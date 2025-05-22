using System.ComponentModel.DataAnnotations;
using CareNet_System.Models;

namespace CareNet_System.Models
{
    public class UniqueAttribute : ValidationAttribute
    {
        protected override ValidationResult? IsValid(object value, ValidationContext validationContext)
        {
            Department deptFromReq = validationContext.ObjectInstance as Department;

            // Use dependency injection to resolve the DbContext instead of creating a new instance
            var context = (HosPitalContext)validationContext.GetService(typeof(HosPitalContext));
            if (context == null)
            {
                throw new InvalidOperationException("HosPitalContext is not available in the validation context.");
            }

            Department deptFromDB = context.Departments.FirstOrDefault(d => d.name == deptFromReq.name);

            if (deptFromDB == null)
            {
                return ValidationResult.Success;
            }
            else
            {
                return new ValidationResult($"The course {deptFromDB.name} already exists in this department.");
            }
        }
    }
}
