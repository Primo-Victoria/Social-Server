using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Net;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Social_Server.BusinessLogic.Core.Interfaces;
using Social_Server.Core.Models;

namespace RateMeServer.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FriendInvitationsController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly IInvatationFriendService _invitationFriendService;

        public FriendInvitationsController(IMapper mapper, IInvatationFriendService invatationFriendService)
        {
            _mapper = mapper;
            _invitationFriendService = invatationFriendService;
        }

        /// <summary>
        /// Отправляет приглашение в друзья пользователю
        /// </summary>
        /// <param name="sendingUserId">Идентификатор пользователя, который отправляет приглашение</param>
        /// <param name="friendUserId">Идентификатор пользователя, которому отправляется приглашение</param>
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        [ProducesResponseType((int)HttpStatusCode.Conflict)]
        [HttpPost("[action]/{sendingUserId}/{friendUserId}")]
        public async Task<IActionResult> SendFriendInvitation(int sendingUserId, int friendUserId)
        {
            await _invitationFriendService.SendFriendInvitation(sendingUserId, friendUserId);

            return Ok();
        }

        /// <summary>
        /// Принимает приглашение в друзья от пользователя и добавляет его в друзья
        /// </summary>
        /// <param name="friendUserId">Идентификатор пользователя, который принимает приглашение</param>
        /// <param name="sendingUserId">Идентификатор пользователя, который отправил приглашение</param>
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        [HttpPost("[action]/{friendUserId}/{sendingUserId}")]
        public async Task<IActionResult> AcceptFriendInvitation(int friendUserId, int sendingUserId)
        {
            await _invitationFriendService.AcceptFriendInvitation(friendUserId, sendingUserId);

            return Ok();
        }

        /// <summary>
        /// Удаляет приглашение в друзья от пользователя
        /// </summary>
        /// <param name="friendUserId">Идентификатор пользователя, который удаляет отправленное ему приглашение</param>
        /// <param name="sendingUserId">Идентификатор пользователя, который отправил приглашение</param>
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        [HttpDelete("[action]/{friendUserId}/{sendingUserId}")]
        public async Task<IActionResult> DeleteFriendInvitation(int friendUserId, int sendingUserId)
        {
            await _invitationFriendService.DeleteFriendInvitation(friendUserId, sendingUserId);

            return Ok();
        }

        /// <summary>
        /// Возвращает количество приглашений в друзья, отправленные пользователю
        /// </summary>
        /// <param name="userId">Идентификатор пользователя</param>
        [ProducesResponseType(typeof(int), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        [HttpGet("[action]/{userId}")]
        public async Task<ActionResult<int>> GetNumberOfFriendInvitationsOfUser(int userId)
        {
            var numberOfFriendInvitations = await _invitationFriendService.GetNumberOfFriendInvitationsOfUser(userId);

            return numberOfFriendInvitations;
        }

        /// <summary>
        /// Возвращает пользователей, которые отправили приглашение в друзья пользователю
        /// </summary>
        /// <param name="userId">Идентификатор пользователя</param>
        [ProducesResponseType(typeof(List<UserInformationDto>), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        [HttpGet("[action]/{userId}")]
        public async Task<ActionResult<List<UserInformationDto>>> GetUsersWhoHaveSentFriendInvitationsToTheUser(int userId)
        {
            var userInformationBloList = await _invitationFriendService.GetUsersWhoHaveSentFriendInvitationsToTheUser(userId);

            return _mapper.Map<List<UserInformationDto>>(userInformationBloList);
        }
    }
}
