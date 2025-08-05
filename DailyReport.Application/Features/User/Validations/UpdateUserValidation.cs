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
    public class UpdateUserValidation : AbstractValidator<UpdateUserCommand>
    {
        IApplicationDbContext _dbContext;
        public UpdateUserValidation(IApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
            RuleFor(x => x.EmployeeNumber)
                .NotEmpty().WithMessage("Employee number is required")
                .MustAsync(async (command, employeeNumber, cancellationToken) =>
                {
                    var requestEmployeeNumber = employeeNumber!.ToLower();
                    return !await _dbContext.Entity<User>().AnyAsync(a => a.Id != command.Id && a.EmployeeNumber.ToLower() == command.EmployeeNumber && a.IsDeleted == false, cancellationToken);
                }).WithMessage("Employee number already exists");
            RuleFor(x => x.FullName).NotEmpty().WithMessage("Full name is required");
            RuleFor(x => x.UserName).NotEmpty().WithMessage("Username is required");
            RuleFor(x => x.Email)
                .NotEmpty().WithMessage("Email is required")
                .MustAsync(async (command, email, cancellationToken) =>
                {
                    var requestEmail = email!.ToLower();
                    return !await _dbContext.Entity<User>().AnyAsync(a => a.Id != command.Id && a.Email.ToLower() == command.Email && a.IsDeleted == false, cancellationToken);
                }).WithMessage("Email already exists");
            RuleFor(x => x.Address).NotEmpty().WithMessage("Address is required");
            RuleFor(x => x.PhoneNumber).NotEmpty().WithMessage("Phone number is required");
            RuleFor(x => x.RoleId).NotEmpty().WithMessage("Role is required");
        }
    }
}
