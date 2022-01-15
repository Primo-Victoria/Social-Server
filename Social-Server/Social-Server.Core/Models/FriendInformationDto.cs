using System;
using Social_Server.DataAccess.Core.Models;

namespace Social_Server.Core.Models
{
	public class FriendInformationDto
	{
		public UserRto UserIdOne { get; set; }
		public UserRto UserIdTwo { get; set; }
	}
}