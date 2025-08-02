using DailyReport.Infrastructure.BaseResponses;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System.Text.Json;
using System.Text.Json.Serialization;
using static DailyReport.WebApp.Infrastructures.GlobalExceptionHandling;

namespace DailyReport.WebApp.Infrastructures
{
    public class BaseResponseHandling : IResultFilter
    {
        public void OnResultExecuted(ResultExecutedContext context)
        {
        }

        public void OnResultExecuting(ResultExecutingContext context)
        {
            if (context.Result is ObjectResult result)
            {
                var resultObj = result.Value;

                var actionMetadata = context.ActionDescriptor.EndpointMetadata;
                if (actionMetadata.Any(metadataItem => metadataItem is IgnoreResultManipulatorAttribute))
                {
                    context.Result = new JsonResult(resultObj, new JsonSerializerOptions()
                    {
                        DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull
                    });
                }
                else
                {
                    var resp = new BaseResponse
                    {
                        Path = context.HttpContext.Request.Path.HasValue ? context.HttpContext.Request.Path.Value : "",
                        Method = context.HttpContext.Request.Method
                    };

                    if (resultObj is not null && resultObj is not Unit)
                        resp.Payload = resultObj;

                    context.Result = new JsonResult(resp, new JsonSerializerOptions()
                    {
                        PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
                        DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull,
                        Converters = { new JsonStringEnumConverter() }
                    });
                }
            }
        }
    }
}
