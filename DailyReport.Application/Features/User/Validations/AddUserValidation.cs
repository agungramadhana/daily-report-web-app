using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DailyReport.Domain.Entities;
using FluentValidation;
using Microsoft.EntityFrameworkCore;

namespace DailyReport.Application.Features
{
    public class AddUserValidation : AbstractValidator<AddUserCommand>
    {
        IApplicationDbContext _dbContext;

        public AddUserValidation(IApplicationDbContext dbContext)
        {
            _dbContext = dbContext;

            RuleFor(x => x.UserName).NotEmpty().WithMessage("Username is required");
            RuleFor(x => x.Email)
                .NotEmpty().WithMessage("Email is required")
                .MustAsync(async (email, cancellationToken) =>
                {
                    var requestEmail = email.ToLower();
                    return !await _dbContext.Entity<User>().AnyAsync(a => a.Email.ToLower() == requestEmail && a.IsDeleted == false, cancellationToken);
                }).WithMessage("Email already exists");
            RuleFor(x => x.Password).NotEmpty().WithMessage("Password is required");
            RuleFor(x => x.Address).NotEmpty().WithMessage("Address is required");
            RuleFor(x => x.PhoneNumber).NotEmpty().WithMessage("Phone number is required");
            RuleFor(x => x.RoleId).NotEmpty().WithMessage("Role is required");
        }
    }
}
