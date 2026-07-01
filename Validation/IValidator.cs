using IntelligencePipeline.Models.Reports;

using System;
namespace IntelligencePipeline.Validation
{
    public interface IValidator
    {
        ValidationResult Validate(Report report);
    }
    
}