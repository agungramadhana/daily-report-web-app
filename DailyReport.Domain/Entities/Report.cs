using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DailyReport.Domain.Entities
{
    public class Report : BaseEntity
    {
        public string AreaName { get; set; } = string.Empty;
        public double Latitude { get; set; }
        public double Longitude { get; set; }
        public DateTime Date { get; set; }
        public string ReportNote { get; set; } = string.Empty;
    }
}
