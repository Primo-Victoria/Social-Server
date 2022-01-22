using System;
namespace Social_Server.DataAccess.Core.Models
{
	public class InvitationFriendRto
	{
			public int SendingUserId { get; set; }

			public int FriendUserId { get; set; }

			public UserRto SendingUser { get; set; }

			public UserRto FriendUser { get; set; }

			public DateTimeOffset TimestampCreated { get; set; }

	}
}

