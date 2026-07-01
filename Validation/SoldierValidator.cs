using System.Linq;
using IntelligencePipeline.Models.Reports;

namespace IntelligencePipeline.Validation
{
    class SoldierValidator :BaseValidator
    {
        protected override ValidationResult ValidateSpecificFields(Report report)
        {
            if (report is not SoldierReport soldier)
            { return ValidationResult.Failure("Invalid report type: expected SoldierReport"); }
            if (soldier.SoldierName.Length < 2 | soldier.SoldierName.Length > 50)
            { return ValidationResult.Failure("Name must be between 2 and 50 characters"); }
            if (soldier.SoldierID.Length !=7)
            { return ValidationResult.Failure("Personal ID must be exactly 7 digits long"); }
            if (soldier.Unit.Length <2 | soldier.Unit.Length > 50)
            { return ValidationResult.Failure("Unit name must be between 2 and 50 characters"); }
            if (soldier.ConfidenceLevel <1 |soldier.ConfidenceLevel >5)
            { return ValidationResult.Failure("Please enter a confidence level between 1 and 5"); }

            return ValidationResult.Success();
        }
    }
}