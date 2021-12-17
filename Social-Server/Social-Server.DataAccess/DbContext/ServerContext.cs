using Microsoft.EntityFrameworkCore;
using Social_Server.DataAccess.Core.Interfaces.DbContext;
using Social_Server.DataAccess.Core.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Social_Server.DataAccess.DbContext
{
    public class ServerContext : Microsoft.EntityFrameworkCore.DbContext, IServerContext
    {
        public ServerContext(DbContextOptions<ServerContext> options) : base(options)
        {

        }
        public DbSet<UserRto> Users { get; set; }
        public DbSet<UserRoleRto> UserRoles { get; set; }
        public DbSet<SmsRto> Sms { get; set; }
        public DbSet<SmsRoleRto> SmsRoles { get; set; }

        public Task<int> SaveChangeAsync(CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }
    }
}
