using IntelligencePipeline.Models.Reports;
using System;

namespace IntelligencePipeline.Validation
{
    class RadarValidator:BaseValidator
    {
        protected override ValidationResult ValidateSpecificFields(Report report)
        {
            if (report is not RadarReport radar)
            { return ValidationResult.Failure("Invalid report type: expected RadarReport"); }
            if (radar.Speed<0 | radar.Speed >2000)
            { return ValidationResult.Failure("Invalid Speed: must be between 0 and 2000"); }
            if(radar.Direction <0 |radar.Direction > 360)
            { return ValidationResult.Failure("Invalid Direction: must be between 0 and 360"); }
            if(radar.Distance <100 | radar.Distance >1000000)
            { return ValidationResult.Failure("Invalid Distance: must be between 100 and 100000"); }
            return ValidationResult.Success();
        }
    }
}