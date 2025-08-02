using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DailyReport.Infrastructure.BaseResponses
{
    public class BaseResponse
    {
        public BaseResponse()
        {
            IsSuccess = true;
            StatusCode = 200;
            Message = "Ok";
            InnerMessage = null;
            Path = null;
            Payload = null;
            Method = null;
        }

        public bool IsSuccess { get; set; }

        public int StatusCode { get; set; }

        public string Message { get; set; }

        public string? InnerMessage { get; set; }

        public string? Path { get; set; }

        public string? Method { get; set; }

        public object? Payload { get; set; }
        public object? ExceptionMessage { get; set; }
    }
}
