using IntelligencePipeline.Models.Reports;
using System;
namespace IntelligencePipeline.Validation
{
    class DroneValidator:BaseValidator
    {
        protected override ValidationResult ValidateSpecificFields(Report report)
        {
            
            if (report is not DroneReport drone)
            { return ValidationResult.Failure("Invalid Altitude: must be between 100 and 10000"); }
            if (drone.Altitude <100 | drone.Altitude > 1000)
            { return ValidationResult.Failure("Invalid Altitude: must be between 100 and 10000"); }
            if (drone.ImageQuality <1 | drone.ImageQuality > 100)
            { return ValidationResult.Failure("Invalid ImageQuality: must be between 1 and 100"); }
            return ValidationResult.Success();
        }

    }


}