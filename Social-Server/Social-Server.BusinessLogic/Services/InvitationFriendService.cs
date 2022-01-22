using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Share.Exceptions;
using Social_Server.BusinessLogic.Core.Interfaces;
using Social_Server.BusinessLogic.Core.Models;
using Social_Server.DataAccess.Core.Interfaces.DbContext;
using Social_Server.DataAccess.Core.Models;

namespace Social_Server.BusinessLogic.Services
{
	public class InvitationFriendService : IInvatationFriendService
	{
		private readonly IMapper _mapper;
		private readonly IServerContext _context;

		public InvitationFriendService(IMapper mapper, IServerContext context)
		{
			_mapper = mapper;
			_context = context;
		}

		public async Task SendFriendInvitation(int sendingUserId, int friendUserId)
		{
			var sendingUserRto = await _context.Users
				.AsNoTracking()
				.Include(e => e.FirstFriends)
				.Include(e => e.SecondFriends)
				.FirstOrDefaultAsync(e => e.Id == sendingUserId);

			if (sendingUserRto == null)
				throw new ConflictException($"The User with id: {sendingUserId} was not found.");

			if (!await _context.Users.AsNoTracking().AnyAsync(e => e.Id == friendUserId))
				throw new NotFoundException($"The User with id: {friendUserId} was not found.");

			if (sendingUserRto.FirstFriends.Any(e => e.UserIdTwoFriend == friendUserId)
			|| sendingUserRto.SecondFriends.Any(e => e.UserIdOneFriend == friendUserId))
				throw new BadRequestException($"The User with id: {sendingUserId} already has the friend with id: {friendUserId}");

			var existingFriendInvitationRto = await _context.FriendInvitations
			.FirstOrDefaultAsync(e => e.SendingUserId == sendingUserId && e.FriendUserId == friendUserId);

			if (existingFriendInvitationRto != null)
				_context.FriendInvitations.Remove(existingFriendInvitationRto);

			var friendInvitationRto = new InvitationFriendRto
			{
				SendingUserId = sendingUserId,
				FriendUserId = friendUserId,
				TimestampCreated = DateTimeOffset.UtcNow
			};

			_context.FriendInvitations.Add(friendInvitationRto);

			await _context.SaveChangesAsync();
		}
		public async Task AcceptFriendInvitation(int friendUserId, int sendingUserId)
		{
			if (!await _context.FriendInvitations
				.AsNoTracking()
				.AnyAsync(e => e.SendingUserId == sendingUserId && e.FriendUserId == friendUserId))
				throw new NotFoundException($"The Friend Invitation from User with id: {sendingUserId} " +
											$"to User with id: {friendUserId} was not found.");

			var friendInvitationsRto = await _context.FriendInvitations
				.Where(e => (e.SendingUserId == sendingUserId && e.FriendUserId == friendUserId)
				  || (e.SendingUserId == friendUserId && e.FriendUserId == sendingUserId))
				.ToListAsync();

			_context.FriendInvitations.RemoveRange(friendInvitationsRto);

			var userFriendRto = new FriendsRto
			{
				UserIdOneFriend = sendingUserId,
				UserIdTwoFriend = friendUserId
			};

			_context.Friends.Add(userFriendRto);

			await _context.SaveChangesAsync();

			var friend = await _context.Users.AsNoTracking().FirstOrDefaultAsync(x => x.Id == friendUserId);

		}

		public async Task DeleteFriendInvitation(int friendUserId, int sendingUserId)
        {
			var friendInvitationRto = await _context.FriendInvitations
		   .FirstOrDefaultAsync(e => e.SendingUserId == sendingUserId && e.FriendUserId == friendUserId);

			if (friendInvitationRto == null) throw new NotFoundException($"The Friend Invitation from User with id: {sendingUserId} " +
											$"to User with id: {friendUserId} was not found.");

			_context.FriendInvitations.Remove(friendInvitationRto);

			await _context.SaveChangesAsync();

		}

		public async Task<int> GetNumberOfFriendInvitationsOfUser(int userId)
		{
			if (!await _context.Users.AsNoTracking().AnyAsync(e => e.Id == userId))
				throw new NotFoundException($"The User with id: {userId} was not found.");

			var numberOfFriendInvitations = await _context.FriendInvitations
				.AsNoTracking()
				.CountAsync(e => e.FriendUserId == userId);

			return numberOfFriendInvitations;
		}

		public async Task<List<UserInformationBlo>> GetUsersWhoHaveSentFriendInvitationsToTheUser(int userId)
		{
			if (!await _context.Users.AsNoTracking().AnyAsync(e => e.Id == userId))
				throw new NotFoundException($"The User with id: {userId} was not found.");

			var usersWhoHaveSentFriendInvitationsRto = await _context.FriendInvitations
				.AsNoTracking()
				.Include(e => e.SendingUser)
				.Where(e => e.FriendUserId == userId)
				.Select(e => e.SendingUser)
				.ToListAsync();

			var userInformationBloList = new List<UserInformationBlo>();

			foreach (var userWhoSentFriendInvitationRto in usersWhoHaveSentFriendInvitationsRto)
				userInformationBloList.Add(await ConvertToUserInformation(userWhoSentFriendInvitationRto));

			return userInformationBloList;
		}


		private async Task<UserInformationBlo> ConvertToUserInformation(UserRto userRto)
		{
			if (userRto == null)
				throw new ArgumentNullException(nameof(userRto));

			UserInformationBlo userInformationBlo = _mapper.Map<UserInformationBlo>(userRto);

			return userInformationBlo;
		}
	}
}
