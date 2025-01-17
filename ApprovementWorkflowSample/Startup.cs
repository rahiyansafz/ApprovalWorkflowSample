using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using ApprovementWorkflowSample.Applications;
using ApprovementWorkflowSample.Models;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components.Server;
using ApprovementWorkflowSample.Approvements;
using Microsoft.EntityFrameworkCore;

namespace ApprovementWorkflowSample
{
    public class Startup
    {
        private readonly IConfiguration configuration;
        public Startup(IConfiguration configuration)
        {
            this.configuration = configuration;
        }
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddRazorPages();
            services.AddServerSideBlazor();
            services.AddHttpClient();
            
            services.AddControllers()
                .AddNewtonsoftJson();
            services.AddDbContext<ApprovementWorkflowContext>(options =>
                options.UseSqlServer(configuration["DbConnection"]));
            services.AddIdentity<ApplicationUser, IdentityRole<int>>()
                .AddUserStore<ApplicationUserStore>()
                .AddEntityFrameworkStores<ApprovementWorkflowContext>()
                .AddDefaultTokenProviders();
            
            services.AddScoped<IHostEnvironmentAuthenticationStateProvider>(sp =>
                (ServerAuthenticationStateProvider) sp.GetRequiredService<AuthenticationStateProvider>()
            );
            services.AddScoped<IApplicationUsers, ApplicationUsers>();
            services.AddScoped<IWorkflows, Workflows>();
            services.AddScoped<IApplicationUserService, ApplicationUserService>();
            services.AddScoped<IApprovementService, ApprovementService>();
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            app.UseStaticFiles();
            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapBlazorHub();
                endpoints.MapControllers();
            });
        }
    }
}
