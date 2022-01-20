using System;
using AutoMapper;
using Social_Server.BusinessLogic.Core.Models;
using Social_Server.Core.Models;

namespace Social_Server.Controllers
{
    public class MicroserviceProfile : Profile
    {
        public MicroserviceProfile()
        {
            CreateMap<UserInformationBlo, UserInformationDto>();
            CreateMap<UserUpdateBlo, UserUpdateDto>();
            CreateMap<UserInformationDto, UserInformationBlo>();
            CreateMap<UserUpdateDto, UserUpdateBlo>();
            CreateMap<SmsUpdateDto, SmsUpdateBlo>();
            CreateMap<SmsInformationDto, SmsInformationBlo>();
            CreateMap<SmsInformationBlo, SmsInformationDto>();
        }
    }
}