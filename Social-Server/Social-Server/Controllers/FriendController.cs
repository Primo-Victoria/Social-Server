using System;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Mvc;
using Social_Server.BusinessLogic.Core.Interfaces;
using Social_Server.BusinessLogic.Core.Models;
using Social_Server.DataAccess.Core.Models;
using RouteAttribute = Microsoft.AspNetCore.Components.RouteAttribute;

namespace Social_Server.Controllers
{
        [Route("/api/[controller]")]
        [Controller]
        public class FriendController : ControllerBase
        {
            private readonly IMapper _mapper;
            private readonly IFriendService _friendService;

            public FriendController(IFriendService friendService)
            {
                _friendService = friendService;
            }
        [HttpPost("Friend")]
        public async Task<FriendInformationBlo> AddNewFriend(UserRto userIdOne, UserRto userIdTwo)
            {
            FriendInformationBlo friendInformation = await _friendService.AddNewFriend(userIdOne, userIdTwo);

            return (friendInformation);
        }

        }
}
