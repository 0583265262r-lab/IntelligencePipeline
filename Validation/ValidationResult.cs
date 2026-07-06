using System;
namespace IntelligencePipeline.Validation
{
    public class ValidationResult
    {
        public bool _isValid;
        public string? _errorMessage;
        public bool IsValid { get => _isValid; }
        public string? ErrorMessage { get => _errorMessage; }
        public ValidationResult(bool isValid, string? errorMessage)
        {
            _isValid = isValid;
            _errorMessage = errorMessage;
        }
        public static ValidationResult Success()
        {
            return new ValidationResult(true, null);
        }
        public static ValidationResult Failure(string errorMessage)
        {
            return new ValidationResult(false, errorMessage);
        }
    }
}