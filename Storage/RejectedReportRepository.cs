using IntelligencePipeline.Models.Reports;
using System;
namespace IntelligencePipeline.Storage
{
    /// <summary>
    /// Stores and manages reports that failed validation, keeping them
    /// separate from the "clean" ReportRepository so analysts can review
    /// rejections (and the reasons behind them) without them polluting the
    /// working set of valid intelligence.
    /// </summary>
    class RejectedReportRepository
    {
        private List<Report> _rejectedReports;
        public RejectedReportRepository()
        {
            _rejectedReports = new List<Report>();
        }
        public void Add(Report report)
        {
            _rejectedReports.Add(report);
        }
        public List<Report> GetAll()
        { return _rejectedReports; }
        public int GetTotalCount()
        {
            return _rejectedReports.Count();
        }
        public List<Report> GetByReason(string reasonKeyword)
        {
            if (string.IsNullOrWhiteSpace(reasonKeyword))
            {
                return _rejectedReports;
            }
            return _rejectedReports.Where(rejectedReason => rejectedReason.RejectionReason !=null &&
            rejectedReason.RejectionReason.Contains(reasonKeyword)).ToList();
        }
    }
}