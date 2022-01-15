using System;
using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Social_Server.BusinessLogic.AutoMapperProfile;
using Social_Server.BusinessLogic.Core.Interfaces;
using Social_Server.BusinessLogic.Services;
using Social_Server.Controllers;
using Microsoft.EntityFrameworkCore;
using Social_Server.DataAccess.Core.Interfaces.DbContext;
using Social_Server.DataAccess.DbContext;

namespace Social_Server
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddAutoMapper(typeof(BuisnessLogicProfile), typeof(MicroserviceProfile));
            services.AddDbContext<IServerContext, ServerContext>(o => o.UseSqlite("Data Source=base.db"));

            services.AddScoped<IUserService, UserService>();
            services.AddScoped<IFriendService, FriendService>();
            services.AddScoped<ISmsService, SmsService>();
            services.AddControllers();

            services.AddCors();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseAuthorization();

            using var scope = app.ApplicationServices.CreateScope();
            var mapper = scope.ServiceProvider.GetRequiredService<IMapper>();
            mapper.ConfigurationProvider.AssertConfigurationIsValid();

            var dbContext = scope.ServiceProvider.GetRequiredService<ServerContext>();
            dbContext.Database.Migrate();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
