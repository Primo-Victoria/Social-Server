using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Share.Exceptions;
using Social_Server.BusinessLogic.Core.Interfaces;
using Social_Server.BusinessLogic.Core.Models;
using Social_Server.DataAccess.Core.Interfaces.DbContext;
using Social_Server.DataAccess.Core.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Social_Server.BusinessLogic.Services
{
    public class UserService : IUserService
    {
        private readonly IMapper _mapper;
        private readonly IServerContext _context;

        public UserService(IMapper mapper, IServerContext context)
        {
            _mapper = mapper;
            _context = context;
        }

        public async Task<UserInformationBlo> AuthWithEmail(string email, string password)
        {
            UserRto user = await _context.Users.FirstOrDefaultAsync(p => p.Email == email && p.Password == password);

            if (user == null)
                throw new NotFoundException($"Пользователь с почтой {email} не найден");

            return await ConvertToUserInformation(user);
        }

        public async Task<UserInformationBlo> AuthWithLogin(string login, string password)
        {
            UserRto user = await _context.Users.FirstOrDefaultAsync(l => l.Login == login && l.Password == password);

            if (user == null) throw new Exception($"Пользователь с почтой {login} не найден");

            UserInformationBlo userInformationBlo = await ConvertToUserInformation(user);

            return userInformationBlo;
        }

        public async Task<UserInformationBlo> AuthWithPhone(string numberPrefix, string number, string password)
        {
            UserRto user = await _context.Users.FirstOrDefaultAsync(n => n.PhoneNumber == number && n.Password == password && n.PhoneNumberPrefix == numberPrefix);

            if (user == null) throw new Exception("Пользователь не найден");

            UserInformationBlo userInformationBlo = await ConvertToUserInformation(user);

            return userInformationBlo;
        }

        public async Task<bool> DoesExist(string numberPrefix, string number)
        {
            UserRto user = await _context.Users.FirstOrDefaultAsync(y => y.PhoneNumberPrefix == numberPrefix && y.PhoneNumber == number);

            if (user == null) throw new Exception("Номер не существует");

            return true;
        }

        public async Task<UserInformationBlo> Get(int userId)
        {
            UserRto user = await _context.Users.FirstOrDefaultAsync(x => x.Id == userId);

            if (user == null) throw new NotFoundException("Пользователь не найден");

            UserInformationBlo userInfoBlo = await ConvertToUserInformation(user);

            return userInfoBlo;
        }

    public async Task<UserInformationBlo> RegisterWithPhone(string numberPrefix, string number, string password)
        {
            UserRto user = await _context.Users.FirstOrDefaultAsync(y => y.PhoneNumberPrefix == numberPrefix && y.PhoneNumber == number);

            if (user == null) throw new NotFoundException("Вы не ввели данные для регистрации");

            if (user == _context) throw new NotFoundException("Такой пользователь уже есть");

            UserInformationBlo userInformation = await ConvertToUserInformation(user);

            return userInformation;
        }

        public Task<UserInformationBlo> Update(string numberPrefix, string number, string password, UserUpdateBlo userUpdateBlo)
        {
            throw new NotImplementedException();
        }
        private async Task<UserInformationBlo> ConvertToUserInformation(UserRto userRto)
        {
            if (userRto == null) throw new ArgumentNullException(nameof(userRto));

            UserInformationBlo userInformationBlo = _mapper.Map<UserInformationBlo>(userRto);

            return userInformationBlo;
        }
    }
}
