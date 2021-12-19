using System;
using System.Collections.Generic;
using System.Text;

namespace Social_Server.BusinessLogic.Core.Models
{
    public class SmsInformationBlo
    {
        public string Message { get; set; }
        public string FromUserId { get; set; }
        public DateTimeOffset Time { get; set; }
        public string ToUserId { get; set; }
    }
}
