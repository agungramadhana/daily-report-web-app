using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DailyReport.Application
{
    public class BaseDatatableResponse
    {
        public BaseDatatableResponse()
        {
            Data = new List<object>();
            RecordsFiltered = 0;
            RecordsTotal = 0;
        }

        public object Data { get; set; }
        public int RecordsFiltered { get; set; }
        public int RecordsTotal { get; set; }
    }
}
