using Social_Server.BusinessLogic.Core.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Social_Server.BusinessLogic.Core.Interfaces
{
    public interface ISmsService
    {
        Task<SmsInformationBlo> List(string message, string from, string to); 
    }
}
