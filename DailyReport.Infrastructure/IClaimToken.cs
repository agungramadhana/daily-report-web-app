using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace DailyReport.Infrastructure
{
    public interface IClaimToken
    {
        Task<ClaimsIdentity> SetClaimFromToken(string token);
    }
}
