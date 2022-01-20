using System;
using System.Collections.Generic;
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
    public class SmsController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly ISmsService _smsService;

        public SmsController(ISmsService smsService)
        {
            _smsService = smsService;
        }

        [HttpGet("Messages")]
        public async Task<SmsInformationDto> GetAll(string Message, string ToUserId, string FromUserId)
        {
            List<SmsInformationBlo> smsInformationBlo = await _smsService.GetAll(Message, FromUserId, ToUserId);

            return await ConvertToSmsInformationAsync(smsInformationBlo);
        }

        [HttpPost("Update")]
        public async Task<SmsInformationDto> Update(SmsUpdateDto smsUpdateDto)
        {
           SmsInformationBlo smsInformationBlo = await _smsService.Update(smsUpdateDto.Message);

           return await ConvertToSmsInformationAsync(smsInformationBlo);
    }

        private async Task<SmsInformationBlo> ConvertToSmsInformationDtoAsync(SmsInformationDto smsInformationDto)
        {
            if (smsInformationDto == null) throw new ArgumentNullException(nameof(smsInformationDto));

            SmsInformationBlo smsInformationBlo = _mapper.Map<SmsInformationBlo>(smsInformationDto);

            return smsInformationBlo;

        }
        private async Task<SmsInformationDto> ConvertToSmsInformationAsync(SmsInformationBlo smsInformationBlo)
        {
            if (smsInformationBlo == null) throw new ArgumentNullException(nameof(smsInformationBlo));

            SmsInformationDto smsInformationDto = _mapper.Map<SmsInformationDto>(smsInformationBlo);

            return smsInformationDto;

        }
    }
}