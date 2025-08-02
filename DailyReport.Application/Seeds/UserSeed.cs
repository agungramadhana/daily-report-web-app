using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DailyReport;
using DailyReport.Domain.Entities;
using DailyReport.Domain.Enums;

namespace DailyReport.Application.Seeds
{
    public static class UserSeed
    {
        public static List<User> GetUserSeed()
        {
            var listUser = new List<User>
            {
                new User
                {
                    Id = Guid.Empty,
                    EmployeeNumber = "000-000-000",
                    FullName = "Super Admin",
                    UserName = "superadmin",
                    Email = "superadmin@mail.com",
                    Address = "Bandung, West Java, Indonesia",
                    PhoneNumber = "1234567890",
                    RoleId = Guid.Empty
                }
            };

            return listUser;
        }
    }
}
