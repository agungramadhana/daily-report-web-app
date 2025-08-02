using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DailyReport.Domain.Entities;
using DailyReport.Domain.Enums;
using DailyReport.Infrastructure.Helpers;

namespace DailyReport.Application.Seeds
{
    public class RoleSeed
    {
        public static List<RoleSeedDTO> GetRoleSeed()
        {
            var listRole = new List<RoleSeedDTO>
            {
                new RoleSeedDTO
                {
                    Id = Guid.Empty,
                    Name = RoleEnum.SuperAdministrator.GetEnumDescription(),
                    Code = RoleEnum.SuperAdministrator,
                },
                new RoleSeedDTO
                {
                    Id = Guid.Parse("cea35578-6e4f-48c0-acd6-fabf9400bcda"),
                    Name = RoleEnum.Administrator.GetEnumDescription(),
                    Code = RoleEnum.Administrator,
                },
                new RoleSeedDTO
                {
                    Id = Guid.Parse("9aad9f43-7909-4aa3-a534-08ef32bbc0a3"),
                    Name = RoleEnum.User.GetEnumDescription(),
                    Code = RoleEnum.User,
                }
            };

            return listRole;
        }
    }

    public class RoleSeedDTO
    {
        public Guid Id { get; set; }
        public string? Name { get; set; }
        public RoleEnum Code { get; set; }
    }
}
