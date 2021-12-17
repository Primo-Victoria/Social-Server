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
        public async Task<SmsInformationBlo> List(string message, string to, string from)
        {
            SmsRto sms = await _context.Sms.FindAsync(message, to, from);

            if (sms == null) throw new NotFoundException("Пустое сообщение");

            return await ConvertToSmsInformation(sms);
        }
        private async Task<SmsInformationBlo> ConvertToSmsInformation(SmsRto smsRto)
        {
            if (smsRto == null) throw new ArgumentNullException(nameof(smsRto));

            SmsInformationBlo smsInformationBlo = _mapper.Map<SmsInformationBlo>(smsRto);

            return smsInformationBlo;
        }
    }
}
