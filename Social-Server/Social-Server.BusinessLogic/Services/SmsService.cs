using AutoMapper;
using Social_Server.DataAccess.Core.Interfaces.DbContext;
using System;
using Social_Server.BusinessLogic.Core.Models;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Social_Server.DataAccess.Core.Models;
using Share.Exceptions;
using Social_Server.BusinessLogic.Core.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace Social_Server.BusinessLogic.Services
{
    public class SmsService : ISmsService
    {
        private readonly IMapper _mapper;
        private readonly IServerContext _context;

        public SmsService(IMapper mapper, IServerContext context)
        {
            _mapper = mapper;
            _context = context;
        }

        public async Task<List<SmsInformationBlo>> GetAll(string message, string toUserId, string fromUserId)
        {
            if (await _context.Sms.AsNoTracking().AnyAsync(e => e.Message == message && e.ToUserId == toUserId && e.FromUserId == fromUserId))
                throw new NotFoundException("Сообщение не найдено");

            var smsRto = await _context.Sms
                .AsNoTracking()
                .Include(sms => sms.FromUserId)
                .Include(sms => sms.Message)
                .Include(sms => sms.ToUserId)
                .OrderByDescending(e => e.Time)
                .Where(sms =>
                    sms.Message == message
                    && (sms.Message.ToUpper().Contains(message.ToUpper()) || sms.Message.ToUpper().Contains(message.ToUpper())))
                .ToListAsync();

            var smsInformationBlo = new List<SmsInformationBlo>();

            foreach (var smsUserRto in smsRto)
                smsInformationBlo.Add(await ConvertToSmsInformationAsync(smsUserRto));

            return smsInformationBlo;
        }

        public async Task<SmsInformationBlo> Update(string message, SmsUpdateBlo smsUpdateBlo)
        {
            SmsRto sms = await _context.Sms.FirstOrDefaultAsync(m => m.Message == message);

            if (sms == null) throw new NotFoundException("Такого сообщения нету");

            sms.Message = smsUpdateBlo.Message;

            SmsInformationBlo smsInfoBlo = await ConvertToSmsInformationAsync(sms);

            return smsInfoBlo;
        }

        private async Task<SmsInformationBlo> ConvertToSmsInformationAsync(SmsRto smsRto)
        {
            if (smsRto == null) throw new ArgumentNullException(nameof(smsRto));

            SmsInformationBlo smsInformationBlo = _mapper.Map<SmsInformationBlo>(smsRto);

            return smsInformationBlo;
        }
    }
}