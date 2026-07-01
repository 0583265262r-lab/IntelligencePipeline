using System;
using IntelligencePipeline.Models.Enums;
using IntelligencePipeline.Models.Reports;

namespace IntelligencePipeline.Validation
{
    class SignalValidator : BaseValidator
    {
        protected override ValidationResult ValidateSpecificFields(Report report)
        {
            if ( report is not SignalReport signal)
            { return ValidationResult.Failure(""); }
            if (signal.Frequency<1.0 | signal.Frequency>3000.0)
            { return ValidationResult.Failure(""); }
            if (signal.Content.Length<5 | signal.Content.Length >1000)
            { return ValidationResult.Failure(""); }
            if (!Enum.IsDefined(typeof(Language),signal.Language))
            { return ValidationResult.Failure(""); }
            if (signal.SignalStrength < -120 | signal.SignalStrength >0)
            { return ValidationResult.Failure(""); }
            return ValidationResult.Success();
        }
    }
}