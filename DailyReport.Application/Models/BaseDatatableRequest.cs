using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DailyReport.Application.Models
{
    public class BaseDatatableRequest
    {
        public BaseDatatableRequest()
        {
            if (string.IsNullOrEmpty(OrderType))
                OrderType = "asc";

            Length = 10;
            Start = 0;
        }

        public string? Keyword { get; set; }
        public int Length { get; set; }
        public int Start { get; set; }
        public string? OrderCol { get; set; }
        public string OrderType { get; set; }
    }
}
