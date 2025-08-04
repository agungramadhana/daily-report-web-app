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
    public class CreateUserValidation : AbstractValidator<CreateUserCommand>
    {
        IApplicationDbContext _dbContext;

        public CreateUserValidation(IApplicationDbContext dbContext)
        {
            _dbContext = dbContext;

            RuleFor(x => x.EmployeeNumber)
                .NotNull().NotEmpty().WithMessage("Employee number is required")
                .MustAsync(async (employeeNumber, cancellationToken) =>
                {
                    var requestEmployeeNumber = employeeNumber.ToLower();
                    return !await _dbContext.Entity<User>().AnyAsync(a => a.EmployeeNumber.ToLower() == requestEmployeeNumber && a.IsDeleted == false, cancellationToken);
                }).WithMessage("Employee number already exists");
            RuleFor(x => x.FullName).NotNull().NotEmpty().WithMessage("Full name is required");
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
            RuleFor(x => x.IsActive).NotEmpty().WithMessage("Status is required");
        }
    }
}
