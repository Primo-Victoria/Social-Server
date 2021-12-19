using AutoMapper;
using Social_Server.BusinessLogic.Core.Models;
using Social_Server.DataAccess.Core.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Social_Server.BusinessLogic.AutoMapperProfile
{
    public class BuisnessLogicProfile : Profile
    {
        public BuisnessLogicProfile()
        {
            CreateMap<UserRto, UserUpdateBlo>()
                    .ForMember(x => x.Id, x => x.MapFrom(m => m.Id))
                    .ForMember(x => x.Birthday, x => x.MapFrom(m => m.Birthday))
                    .ForMember(x => x.LastName, x => x.MapFrom(m => m.LastName))
                    .ForMember(x => x.FirstName, x => x.MapFrom(m => m.FirstName))
                    .ForMember(x => x.Patronymic, x => x.MapFrom(m => m.Patronymic))
                    .ForMember(x => x.Password, x => x.MapFrom(m => m.Password))
                    .ForMember(x => x.Email, x => x.MapFrom(m => m.Email))
                    .ForMember(x => x.Login, x => x.MapFrom(m => m.Login))
                    .ForMember(x => x.PhoneNumberPrefix, x => x.MapFrom(m => m.PhoneNumberPrefix))
                    .ForMember(x => x.PhoneNumber, x => x.MapFrom(m => m.PhoneNumber))
                    .ForMember(x => x.IsBoy, x => x.MapFrom(m => m.IsBoy))
                    .ForMember(x => x.AvatarUrl, x => x.MapFrom(m => m.AvatarUrl));

           CreateMap<UserRto, UserInformationBlo>()
                    .ForMember(x => x.Id, x => x.MapFrom(m => m.Id))
                    .ForMember(x => x.Birthday, x => x.MapFrom(m => m.Birthday))
                    .ForMember(x => x.LastName, x => x.MapFrom(m => m.LastName))
                    .ForMember(x => x.FirstName, x => x.MapFrom(m => m.FirstName))
                    .ForMember(x => x.Patronymic, x => x.MapFrom(m => m.Patronymic))
                    .ForMember(x => x.Email, x => x.MapFrom(m => m.Email))
                    .ForMember(x => x.PhoneNumberPrefix, x => x.MapFrom(m => m.PhoneNumberPrefix))
                    .ForMember(x => x.PhoneNumber, x => x.MapFrom(m => m.PhoneNumber))
                    .ForMember(x => x.IsBoy, x => x.MapFrom(m => m.IsBoy))
                    .ForMember(x => x.AvatarUrl, x => x.MapFrom(m => m.AvatarUrl));
            CreateMap<SmsRto, SmsInformationBlo>()
                    .ForMember(x => x.ToUserId, x => x.MapFrom(m => m.ToUserId))
                    .ForMember(x => x.FromUserId, x => x.MapFrom(m => m.FromUserId))
                    .ForMember(x => x.Message, x => x.MapFrom(m => m.Message));
            CreateMap<SmsRto, SmsUpdateBlo>()
                   .ForMember(x => x.Message, x => x.MapFrom(m => m.Message));
            CreateMap<FriendsRto, FriendInformationBlo>()
                     .ForMember(x => x.UserIdOne, x => x.MapFrom(m => m.UserIdOne))
                     .ForMember(x => x.UserIdTwo, x => x.MapFrom(m => m.UserIdTwo));

        }
    }
}