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

            return await ConvertToUserInformationAsync(user);
        }

        public async Task<UserInformationBlo> AuthWithLogin(string login, string password)
        {
            UserRto user = await _context.Users.FirstOrDefaultAsync(l => l.Login == login && l.Password == password);

            if (user == null) throw new Exception($"Пользователь с почтой {login} не найден");

            UserInformationBlo userInformationBlo = await ConvertToUserInformationAsync(user);

            return userInformationBlo;
        }

        public async Task<UserInformationBlo> AuthWithPhone(string numberPrefix, string number, string password)
        {
            UserRto user = await _context.Users.FirstOrDefaultAsync(n => n.PhoneNumber == number && n.Password == password && n.PhoneNumberPrefix == numberPrefix);

            if (user == null) throw new Exception("Пользователь не найден");

            UserInformationBlo userInformationBlo = await ConvertToUserInformationAsync(user);

            return userInformationBlo;
        }

        public async Task<bool> DoesExist(string numberPrefix, string number)
        {
            bool user = await _context.Users.AnyAsync(y => y.PhoneNumberPrefix == numberPrefix && y.PhoneNumber == number);

            return user;
        }

        public async Task<UserInformationBlo> Get(int userId)
        {
            UserRto user = await _context.Users.FirstOrDefaultAsync(x => x.Id == userId);

            if (user == null) throw new NotFoundException("Пользователь не найден");

            UserInformationBlo userInfoBlo = await ConvertToUserInformationAsync(user);

            return userInfoBlo;
        }

    public async Task<UserInformationBlo> RegisterWithPhone(string numberPrefix, string number, string password)
        {
            bool user = await _context.Users.AnyAsync(y => y.PhoneNumberPrefix == numberPrefix && y.PhoneNumber == number);

            if (user == true) throw new BadRequestException("Вы не ввели данные для регистрации");

            UserRto newUser = new UserRto()
            {
                PhoneNumber = number,
                Password = password,
                PhoneNumberPrefix = numberPrefix


            };

            _context.Users.Add(newUser);

            await _context.SaveChangesAsync();

            UserInformationBlo userInformation = await ConvertToUserInformationAsync(newUser);

            return userInformation;
        }

        public async Task<UserInformationBlo> Update(string numberPrefix, string number, string password, UserUpdateBlo userUpdateBlo)
        {
            UserRto user = await _context.Users.FirstOrDefaultAsync(y => y.PhoneNumber == number && y.PhoneNumberPrefix == numberPrefix && y.Password == password);

            if (user == null) throw new NotFoundException("Такого пользователя нету");

            user.Password = userUpdateBlo.Password;
            user.FirstName = userUpdateBlo.FirstName;
            user.Email = userUpdateBlo.Email;
            user.Login = userUpdateBlo.Login;
            user.LastName = userUpdateBlo.LastName;
            user.Patronymic = userUpdateBlo.Patronymic;
            user.Birthday = userUpdateBlo.Birthday;
            user.IsBoy = userUpdateBlo.IsBoy;
            user.AvatarUrl = userUpdateBlo.AvatarUrl;
            user.PhoneNumber = userUpdateBlo.PhoneNumber;
            user.PhoneNumberPrefix = userUpdateBlo.PhoneNumberPrefix;

            UserInformationBlo userInfoBlo = await ConvertToUserInformationAsync(user);
            return userInfoBlo;
        }

        private async Task<UserInformationBlo> ConvertToUserInformationAsync(UserRto userRto)
        {
            if (userRto == null) throw new ArgumentNullException(nameof(userRto));

            UserInformationBlo userInformationBlo = _mapper.Map<UserInformationBlo>(userRto);

            return userInformationBlo;
        }
    }
}
