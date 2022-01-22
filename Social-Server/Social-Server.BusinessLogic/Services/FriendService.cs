using System;
using System.Collections.Generic;
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
	public class FriendService : IFriendService
	{
        private readonly IMapper _mapper;
        private readonly IServerContext _context;

        public FriendService(IMapper mapper, IServerContext context)
        {
            _mapper = mapper;
            _context = context;
        }

        public async Task<FriendInformationBlo> AddNewFriend(UserRto userIdOne, UserRto userIdTwo)
        {
            bool friend = await _context.Friends.AnyAsync(f => f.UserIdOne == userIdOne && f.UserIdTwo == userIdTwo);

            if (friend == true) throw new BadRequestException("Пользователь уже в друзьях");

            FriendsRto newFriend = new FriendsRto
            {
               UserIdOne = userIdOne,
               UserIdTwo = userIdTwo
            };
            _context.Friends.Add(newFriend);

            await _context.SaveChangesAsync();

            FriendInformationBlo friendInformation = await ConvertToFriendInformationAsync(newFriend);

            return friendInformation;

        }

        public async Task<List<UserInformationBlo>> GetUserFriends(int userId)
        {
            var userRto = await _context.Users
                .AsNoTracking()
                .Include(e => e.FirstFriends)
                .ThenInclude(e => e.UserIdTwo)
                .Include(e => e.SecondFriends)
                .ThenInclude(e => e.UserIdOne)
                .FirstOrDefaultAsync(e => e.Id == userId);

            if (userRto == null)
                throw new NotFoundException($"The User with id: {userId} was not found.");

            var userFriendsRto = new List<UserRto>();

            userRto.FirstFriends.ForEach(e => userFriendsRto.Add(e.UserIdTwo));
            userRto.SecondFriends.ForEach(e => userFriendsRto.Add(e.UserIdOne));

            var userInformationBloList = new List<UserInformationBlo>();

            foreach (var Friends in userFriendsRto)
                userInformationBloList.Add(await ConvertToUserInformation(Friends));

            return userInformationBloList;
           
        }


        public async Task DeleteUserFriend(int userId, int friendUserId)
        {
            if (!await _context.Users.AsNoTracking().AnyAsync(e => e.Id == userId))
                throw new ConflictException($"The User with id: {userId} was not found.");

            if (!await _context.Users.AsNoTracking().AnyAsync(e => e.Id == friendUserId))
                throw new NotFoundException($"The User with id: {friendUserId} was not found.");

            var userFriendRto = await _context.Friends.FirstOrDefaultAsync(e => (e.UserIdOneFriend == userId && e.UserIdTwoFriend == friendUserId)
                                          || (e.UserIdOneFriend == friendUserId && e.UserIdTwoFriend == userId));

            if (userFriendRto == null)
                throw new BadRequestException($"The User with id: {friendUserId} is not a friend of the User with id: {userId}.");

            _context.Friends.Remove(userFriendRto);

            await _context.SaveChangesAsync();
        }


        private async Task<FriendInformationBlo> ConvertToFriendInformationAsync(FriendsRto friendRto)
        {
            if (friendRto == null) throw new ArgumentNullException(nameof(friendRto));

           FriendInformationBlo friendInformationBlo = _mapper.Map<FriendInformationBlo>(friendRto);

            return friendInformationBlo;
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

