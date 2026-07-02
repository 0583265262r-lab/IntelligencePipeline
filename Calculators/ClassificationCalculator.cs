using IntelligencePipeline.Models.Enums;
using IntelligencePipeline.Models.Reports;
using System;
namespace IntelligencePipeline.Calculators
{
    class ClassificationCalculator
    {
        string[] TopSecretKeywords = { "target", "attack", "missile" };
        string[] SecretKeywords = { "border ", "weapon" };
        public Classification Calculate(Report report)
        {
            if (IsTopSecret(report))
                return Classification.TopSecret;
            if (IsSecret(report))
                return Classification.Secret;
            if (IsRestricted(report))
                return Classification.Restricted;
            return Classification.Unclassified;
        }
        public bool IsTopSecret(Report report)
        {
            if (report.Priority == Priority.Critical)
                return true;
            foreach (string kayword in TopSecretKeywords)
            {
                if (report is SignalReport signal)
                {
                    if (signal.Content.Contains(kayword))
                        return true;
                }
            }
            return false;
        }
        public bool IsSecret(Report report)
        {
            if (report.Priority == Priority.High)
                return true;
            if (report is SignalReport)
                return true;
            foreach (string kayword in SecretKeywords)
            {
                if (report.Description.Contains(kayword))
                    return true;
            }
            return false;
        }
        public bool IsRestricted (Report report)
        {
            if (report.Priority == Priority.Medium)
                return true;
            if (report is SoldierReport)
                return true;
            return false;
        }

    }
}