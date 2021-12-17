using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Social_Server.DataAccess.Core.Models
{
    [Table("Sms")]
    public class SmsRto
    {
        public string Message { get; set; }
        public string From { get; set; }
        public DateTimeOffset Time { get; set; }
        public string To { get; set; }

    }
}
