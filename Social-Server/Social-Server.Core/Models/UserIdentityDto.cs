using System;
namespace Social_Server.Core.Models
{
	public class UserIdentityDto
	{
			public string NumberPrefix { get; set; }
			public string Number { get; set; }
			public string Password { get; set; }
			public string Email { get; set; }
			public string Login { get; set; }
	}
}

