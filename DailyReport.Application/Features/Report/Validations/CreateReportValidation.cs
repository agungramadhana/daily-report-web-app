using FluentValidation;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DailyReport.Application.Features.Report.Validations
{
    public class CreateReportValidation : AbstractValidator<CreateReportCommand>
    {
        public CreateReportValidation()
        {
            RuleFor(x => x.AreaName).NotNull().NotEmpty();
            RuleFor(x => x.Latitude).NotNull().NotEmpty();
            RuleFor(x => x.Longitude).NotNull().NotEmpty();
            RuleFor(x => x.Date).NotNull().NotEmpty();
        }
    }
}
