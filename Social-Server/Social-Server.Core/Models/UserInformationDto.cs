using System;
namespace Social_Server.Core.Models
{
	public class UserInformationDto
	{
        public int Id { get; set; }
        public string Email { get; set; }
        public bool IsBoy { get; set; }
        public string PhoneNumberPrefix { get; set; }
        public string PhoneNumber { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Patronymic { get; set; }
        public DateTimeOffset Birthday { get; set; }
        public string AvatarUrl { get; set; }
    }
}

