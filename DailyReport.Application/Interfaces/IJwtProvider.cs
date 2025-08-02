using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DailyReport.Application;

namespace DailyReport.Application.Interfaces
{
    public interface IJwtProvider
    {
        Task<string> GenerateToken(UserModel user);
        string Hash(string password);
        bool Verify(string userPassword, string password);
    }
}
