using Social_Server.BusinessLogic.Core.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Social_Server.BusinessLogic.Core.Interfaces
{
    public interface ISmsService
    {
        Task<List<SmsInformationBlo>> GetAll(string message, string fromUserId, string toUserId);
        Task<SmsInformationBlo> Update(string message, SmsUpdateBlo smsUpdateBlo);
    }
}
