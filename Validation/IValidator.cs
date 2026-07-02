using IntelligencePipeline.Models.Reports;

using System;
namespace IntelligencePipeline.Validation
{
    interface IValidator
    {
        ValidationResult Validate(Report report);
    }
    
}