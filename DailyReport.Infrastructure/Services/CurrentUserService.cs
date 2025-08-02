using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using DailyReport.Application.Interfaces;
using DailyReport.Domain.Enums;
using DailyReport.Infrastructure.ClaimTypeContants;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Http;
using DailyReport.Domain.Entities;

namespace DailyReport.Infrastructure.Services
{
    public class CurrentUserService : ICurrentUserService
    {
        public CurrentUserService(IHttpContextAccessor httpContextAccessor)
        {
            var user = httpContextAccessor.HttpContext?.User;
            if (user == null)
            {
                IsAuthenticated = false;
                return;
            }

            IdUser = user?.FindFirstValue(ClaimConstant.IdUser);
            FullName = user?.FindFirstValue(ClaimConstant.FullName);
            UserName = user?.FindFirstValue(ClaimConstant.UserName);
            Role = user?.FindFirstValue(ClaimTypes.Role);
            IsAuthenticated = true;
        }

        public string IdUser { get; set; }
        public string FullName { get; set; }
        public string UserName { get; set; }
        public string Role { get; set; }
        public bool IsAuthenticated { get; }
    }
}
