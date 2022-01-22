using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Social_Server.DataAccess.Core.Models
{
	[Table("Friends")]
	public class FriendsRto
	{
		public int UserIdOneFriend { get; set; }
		public int UserIdTwoFriend { get; set; }
		public UserRto UserIdOne { get; set; }
		public UserRto UserIdTwo { get; set; }
	}
}

