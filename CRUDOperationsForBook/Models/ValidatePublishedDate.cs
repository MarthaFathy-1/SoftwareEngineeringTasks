using System;
using System.ComponentModel.DataAnnotations;

namespace CRUDOperationsForBook.Models
{
    public class ValidatePublishedDate : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            if (value == null)
                return ValidationResult.Success;

            if (value is DateTime date)
            {
                if (date > DateTime.Today)
                {
                    return new ValidationResult("Published date cannot be in the future.");
                }
                return ValidationResult.Success;
            }

            return new ValidationResult("Invalid date format.");
        }
    }
}
