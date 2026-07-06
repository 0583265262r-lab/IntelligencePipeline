using IntelligencePipeline.Calculators;
using IntelligencePipeline.Models.Enums;
using IntelligencePipeline.Models.Reports;
using IntelligencePipeline.Storage;
using IntelligencePipeline.Validation;
using System;
namespace IntelligencePipeline.Pipeline
{
    /// <summary>
    /// Orchestrates the complete report-processing workflow: intake ->
    /// validation -> metric calculation -> storage. This is the only class
    /// that knows the full end-to-end flow; every other class only knows its
    /// own narrow responsibility.
    /// </summary>
    class ReportPipline
    {
        private ReportRepository _validatedReports;
        private RejectedReportRepository _rejectedReports;
        private int _nextReportId;

        //private ReliabilityCalculator _reliabilityCalculator;
        //private PriorityCalculator _priorityCalculator;
        //private ClassificationCalculator _classificationCalculator;

        public ReportPipline()
        {
            _validatedReports = new ReportRepository();
            _rejectedReports = new RejectedReportRepository();
            _nextReportId = 0;

        }
        public int GetNextReportId()
        {
            int reportId = _nextReportId++;
            return reportId;
        }
        public void ProcessReport(Report report)
        {

            
            ValidateReport(report);
            if (report.Status == ReportStatus.Rejected)
            { StoreReport(report);
                return;
            }
            report.Status = ReportStatus.Validated;
            CalculateMetrics(report);
            StoreReport(report);
        }
        public ReportRepository GetValidatedReports()
         => _validatedReports;
        public RejectedReportRepository GetRejectedReports()
            => _rejectedReports;
        private IValidator? GetValidator(Report report)
        {

            if (report is DroneReport)
                return new DroneValidator();
            if (report is SoldierReport)
                return new SoldierValidator();
            if (report is RadarReport)
                return new RadarValidator();
            if (report is SignalReport)
                return new SignalValidator();
            return null;
        }
        private void ValidateReport(Report report)
        {
            var validation = GetValidator(report);
            if (validation == null)
            {
                return;
            }
            var result = validation.Validate(report);
            if (!result.IsValid)
            {
                report.Status = ReportStatus.Rejected;
                report.RejectionReason = result._errorMessage;
            }
        }
        private void CalculateMetrics(Report report)
        { 
            report.ReliabilityScore = new ReliabilityCalculator().Calculate(report);
            report.Priority = new PriorityCalculator().Calculate(report);
            report.Classification = new ClassificationCalculator().Calculate(report);}
        private void StoreReport(Report report)
        {
            if (report.Status == ReportStatus.Rejected)
            { _rejectedReports.Add(report); }
            else
            { _validatedReports.Add(report); }
        }
        public void DisplayStatistics()
        {
            int validCount = _validatedReports.GetTotalCount();
            int rejectedCount = _rejectedReports.GetTotalCount();
            int total = validCount + rejectedCount;
            double successRate = total == 0 ? 0 : (validCount / (double)total) * 100;

            Console.WriteLine("=== Pipeline Statistics ===");
            Console.WriteLine($"Total processed : {total}");
            Console.WriteLine($"Validated       : {validCount}");
            Console.WriteLine($"Rejected        : {rejectedCount}");
            Console.WriteLine($"Success rate    : {successRate:F1}%");
            Console.WriteLine();

            Console.WriteLine("By Priority:");
            foreach (Priority priority in Enum.GetValues(typeof(Priority)))
            {
                Console.WriteLine($"  {priority,-10}: {_validatedReports.GetByPriority(priority).Count}");
            }
            Console.WriteLine();

            Console.WriteLine("By Status:");
            foreach (ReportStatus status in Enum.GetValues(typeof(ReportStatus)))
            {
                Console.WriteLine($"  {status,-10}: {_validatedReports.GetCountByStatus(status)}");
            }
        }
    }
}