using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DailyReport.Application.Interfaces;
using DailyReport.Infrastructure.Authentications;
using DailyReport.Infrastructure.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace DailyReport.Infrastructure
{
    public static class DependencyInjection 
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddHttpContextAccessor();
            services.AddScoped<ICurrentUserService, CurrentUserService>();
            services.AddScoped<IClaimToken, ClaimToken>();
            services.AddScoped<IJwtProvider, JwtProvider>();
            services.ConfigureOptions<JwtOptionSetup>();
            services.ConfigureOptions<JwtBearerTokenSetup>();

            //services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer();

            //services.AddAuthorization(options =>
            //{
            //    options.DefaultPolicy = new AuthorizationPolicyBuilder()
            //    .AddAuthenticationSchemes(JwtBearerDefaults.AuthenticationScheme)
            //    .RequireAuthenticatedUser()
            //    .Build();
            //});

            return services;
        }
    }
}
