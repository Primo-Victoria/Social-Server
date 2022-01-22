using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Social_Server.BusinessLogic.Core.Models;
using Social_Server.DataAccess.Core.Models;

namespace Social_Server.BusinessLogic.Core.Interfaces
{
	public interface IFriendService
	{
		Task<FriendInformationBlo> AddNewFriend(UserRto userIdOne, UserRto userIdTwo);

		Task<List<UserInformationBlo>> GetUserFriends(int userId);

		Task DeleteUserFriend(int userId, int friendUserId);
	}
}

