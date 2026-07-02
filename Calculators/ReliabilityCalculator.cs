using IntelligencePipeline.Models.Reports;
using System;
namespace IntelligencePipeline.Calculators
{
    class ReliabilityCalculator
    {
        public int Calculate(Report report)
        {
            int score = report.CalculateReliabilityScore();
            return Math.Clamp(score, 1, 10);
        }

    }
}