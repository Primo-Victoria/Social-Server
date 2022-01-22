using Microsoft.EntityFrameworkCore;
using Social_Server.DataAccess.Core.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Social_Server.DataAccess.Core.Interfaces.DbContext
{
   public  interface IServerContext : IDisposable, IAsyncDisposable
    {
        DbSet<UserRto> Users { get; set; }

        DbSet<UserRoleRto> UserRoles { get; set; }

        DbSet<SmsRto> Sms { get; set; }

        DbSet<FriendsRto> Friends { get; set; }

        DbSet<InvitationFriendRto> FriendInvitations { get; set; }

        Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);


    }
}
