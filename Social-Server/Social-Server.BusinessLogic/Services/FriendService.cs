using System;
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
        private async Task<FriendInformationBlo> ConvertToFriendInformationAsync(FriendsRto friendRto)
        {
            if (friendRto == null) throw new ArgumentNullException(nameof(friendRto));

           FriendInformationBlo friendInformationBlo = _mapper.Map<FriendInformationBlo>(friendRto);

            return friendInformationBlo;
        }
    }
}

