using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DailyReport.Application.Features.Report.Validations
{
    public class UpdateReportValidator : AbstractValidator<UpdateReportCommand>
    {
        public UpdateReportValidator()
        {
            RuleFor(x => x.AreaName).NotNull().NotEmpty();
            RuleFor(x => x.Latitude).NotNull().NotEmpty();
            RuleFor(x => x.Longitude).NotNull().NotEmpty();
            RuleFor(x => x.Date).NotNull().NotEmpty();
        }
    }
}
