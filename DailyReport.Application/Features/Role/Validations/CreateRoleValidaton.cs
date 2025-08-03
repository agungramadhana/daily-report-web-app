using DailyReport.Application;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DailyReport.Application.Features.Role.Validations
{
    public class CreateRoleValidaton : AbstractValidator<CreateRoleCommand>
    {
        public CreateRoleValidaton()
        {
            RuleFor(x => x.RoleName).NotEmpty().NotNull();
            RuleFor(x => x.IsActive).NotEmpty().NotNull();
        }
    }
}
