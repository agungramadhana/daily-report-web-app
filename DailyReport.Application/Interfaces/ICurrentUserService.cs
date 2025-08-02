using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using DailyReport.Domain.Enums;

namespace DailyReport.Application.Interfaces
{
    public interface ICurrentUserService
    {
        string IdUser { get; set; }
        string FullName { get; set; }
        string UserName { get; set; }
        string Role { get; set; }
        bool IsAuthenticated { get; }
    }
}
