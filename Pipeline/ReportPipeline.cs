using IntelligencePipeline.Calculators;
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
        

        private DroneValidator _droneValidator;
        private SoldierValidator _soldierValidator;
        private RadarValidator _radarValidator;
        private SignalValidator _signalValidator;

        private ReliabilityCalculator _reliabilityCalculator;
        private PriorityCalculator _priorityCalculator;
        private ClassificationCalculator _classificationCalculator;

        public ReportPipeline()
        {
            _validatedReports = new ReportRepository();
            _rejectedReports = new RejectedReportRepository();
            
        }

    }
}