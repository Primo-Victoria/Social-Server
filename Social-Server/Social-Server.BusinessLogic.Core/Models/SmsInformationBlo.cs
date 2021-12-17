using System;
using System.Collections.Generic;
using System.Text;

namespace Social_Server.BusinessLogic.Core.Models
{
    public class SmsInformationBlo
    {
        public string Message { get; set; }
        public string From { get; set; }
        public DateTimeOffset Time { get; set; }
        public string To { get; set; }
    }
}
