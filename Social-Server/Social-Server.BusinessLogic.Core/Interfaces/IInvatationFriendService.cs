using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Social_Server.BusinessLogic.Core.Models;

namespace Social_Server.BusinessLogic.Core.Interfaces
{
	public interface IInvatationFriendService
	{
		Task SendFriendInvitation(int sendingUserId, int friendUserId);

		Task AcceptFriendInvitation(int friendUserId, int sendingUserId);

		Task DeleteFriendInvitation(int friendUserId, int sendingUserId);

		Task<int> GetNumberOfFriendInvitationsOfUser(int userId);

		Task<List<UserInformationBlo>> GetUsersWhoHaveSentFriendInvitationsToTheUser(int userId);
	}
}

