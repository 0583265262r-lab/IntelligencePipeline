using IntelligencePipeline.Models.Reports;
using System;


namespace IntelligencePipeline.Validation 
{
    abstract class BaseValidator : IValidator
    {
        public ValidationResult Validate(Report report)
        {
            ValidationResult commonResult = ValidateCommonFields(report);
            if (!commonResult.IsValid)
            {
                return commonResult;
            }

            return ValidateSpecificFields(report);
        }
        protected ValidationResult ValidateCommonFields(Report report)
        {
            if (report.Timestamp > DateTime.Now)
            { return ValidationResult.Failure("The date cannot be future date"); }
            if (report.Timestamp.Year < 2020)
            { return ValidationResult.Failure("The date cannot be before 2020."); }
            if (report.Latitude < 29.5000 || report.Latitude > 33.5000)
            { return ValidationResult.Failure("Latitude must be between 29.5000 and 33.5000"); }
            if (report.Longitude < 34.0000 || report.Longitude > 36.0000)
            { return ValidationResult.Failure("Longitude must be between 34.0000 and 36.0000"); }
            if (report.Description.Length<10 || report.Description.Length> 500 ||
                report.Description == null)
            { return ValidationResult.Failure("Description cannot be null/empty, must be 10-500 characters"); }
            return ValidationResult.Success();
        }
        protected abstract ValidationResult ValidateSpecificFields(Report report);
        


    }
}

