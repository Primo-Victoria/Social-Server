using System;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Mvc;
using Social_Server.BusinessLogic.Core.Interfaces;
using Social_Server.BusinessLogic.Core.Models;
using Social_Server.Core.Models;
using RouteAttribute = Microsoft.AspNetCore.Components.RouteAttribute;

namespace Social_Server.Controllers
{
    [Route("/api/[controller]")]
    [Controller]
    public class UserController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly IUserService _userService;


        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpPost("registration")]
        public async Task<ActionResult<UserInformationDto>> Register(UserIdentityDto userIdentityDto)
        {
            UserInformationBlo userInformationBlo = await _userService.RegisterWithPhone(userIdentityDto.NumberPrefix, userIdentityDto.Number, userIdentityDto.Password);

            return await ConvertToUserInformationAsync(userInformationBlo);
        }

        [HttpPost("autorization")]
        public async Task<UserInformationDto> AutorizationWithEmail(UserIdentityDto userIdentityDto)
        {
            UserInformationBlo userInformationBlo = await _userService.AuthWithEmail(userIdentityDto.Email, userIdentityDto.Password);

            return await ConvertToUserInformationAsync(userInformationBlo);
        }

        [HttpPost("autorizatitionwithphone")]
        public async Task<UserInformationDto> AutorizationWithPhone(UserIdentityDto userIdentityDto)
        {
            UserInformationBlo userInformationBlo = await _userService.AuthWithPhone(userIdentityDto.NumberPrefix, userIdentityDto.Number, userIdentityDto.Password);

            return await ConvertToUserInformationAsync(userInformationBlo);
        }

        [HttpPost("autorizationwithlogin")]
        public async Task<UserInformationDto> AutorizationWithPLogin(UserIdentityDto userIdentityDto)
        {
            UserInformationBlo userInformationBlo = await _userService.AuthWithLogin(userIdentityDto.Login, userIdentityDto.Password);

            return await ConvertToUserInformationAsync(userInformationBlo);
        }

        [HttpPost("update")]
        public async Task<UserInformationDto> Update(UserUpdateDto userUpdateDto)
        {
            UserUpdateBlo userUpdateBlo = await ConvertToUserInformationDtoAsync(userUpdateDto);

            UserInformationBlo userInformationBlo = await _userService.Update(userUpdateDto.PhoneNumber, userUpdateDto.PhoneNumberPrefix, userUpdateDto.Password, userUpdateBlo);

            return await ConvertToUserInformationAsync(userInformationBlo);
        }

        [HttpGet("get")]
        public async Task<UserInformationDto> Get(int Id)
        {

            UserInformationBlo userInformation = await _userService.Get(Id);

            return await ConvertToUserInformationAsync(userInformation);
        }

        [HttpPost("exist")]
        public async Task<bool> DoesExist(UserIdentityDto userIdentityDto)
        {
            bool doesExist = await _userService.DoesExist(userIdentityDto.NumberPrefix, userIdentityDto.Number);

            return doesExist;
        }

        private async Task<UserInformationDto> ConvertToUserInformationAsync(UserInformationBlo userInformationBlo)
            {
                if (userInformationBlo == null) throw new ArgumentNullException(nameof(userInformationBlo));

                UserInformationDto userInformationDto = _mapper.Map<UserInformationDto>(userInformationBlo);

                return userInformationDto;

            }
        private async Task<UserUpdateBlo> ConvertToUserInformationDtoAsync(UserUpdateDto userUpdateDto)
        {
            if (userUpdateDto == null) throw new ArgumentNullException(nameof(userUpdateDto));

            UserUpdateBlo userUpdateBlo = _mapper.Map<UserUpdateBlo>(userUpdateDto);

            return userUpdateBlo;

        }
    }
    }

