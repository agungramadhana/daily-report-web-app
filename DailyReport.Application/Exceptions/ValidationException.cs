using FluentValidation.Results;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DailyReport.Application.Exceptions
{
    public class ValidationException : Exception
    {
        public ValidationException() : base("One or more validation failures have occurred.")
        {
            Failures = new Dictionary<string, ValidationExceptionVm>();
        }

        public class ValidationExceptionVm
        {
            public object AttemptedValue { get; set; }

            public string[] ErrorMessage { get; set; }
        }

        public ValidationException(List<ValidationFailure> failures)
            : this()
        {
            var propertyNames = failures
                .Select(e => e.PropertyName)
                .Distinct();

            foreach (var item in propertyNames)
            {
                var vm = new ValidationExceptionVm();
                vm.AttemptedValue = failures.Where(x => x.PropertyName == item)
                    .Select(x => x.AttemptedValue)
                    .First();

                vm.ErrorMessage = failures.Where(e => e.PropertyName == item)
                    .Select(e => e.ErrorMessage)
                    .ToArray();

                Failures.Add(item, vm);
            }
        }

        public IDictionary<string, ValidationExceptionVm> Failures { get; }
    }
}
