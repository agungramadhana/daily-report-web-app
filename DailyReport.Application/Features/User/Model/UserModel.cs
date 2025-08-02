using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DailyReport.Application
{
    public class UserModel
    {
        public Guid IdUser { get; set; }
        public string? FullName { get; set; }
        public string? UserName { get; set; }
        public string? Role { get; set; }
    }
}
