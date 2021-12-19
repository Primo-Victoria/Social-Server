using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Social_Server.DataAccess.Core.Models
{
	[Table("Friends")]
	public class FriendsRto
	{
		[Key] public UserRto UserIdOne { get; set; }
		[Key] public UserRto UserIdTwo { get; set; }
	}
}

