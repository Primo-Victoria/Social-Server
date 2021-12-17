using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Social_Server.DataAccess.Core.Models
{
    [Table("UserRole")]
    public class SmsRoleRto
    {
        public string Message { get; set; }
    }
}
