using System;
namespace Social_Server.Core.Models
{
	public class SmsInformationDto
	{
		public string Message { get; set; }
		public string FromUserId { get; set; }
		public DateTimeOffset Time { get; set; }
		public string ToUserId { get; set; }
	}
}

