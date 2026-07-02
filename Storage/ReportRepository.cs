using IntelligencePipeline.Models.Enums;
using IntelligencePipeline.Models.Reports;
using System;
using System.Net.NetworkInformation;
namespace IntelligencePipeline.Storage
{
    /// <summary>
    /// Stores and manages reports that have passed validation.
    /// Backed by a List&lt;Report&gt; - a List was chosen over a fixed-size
    /// Array because the number of incoming reports is unknown ahead of time
    /// and grows continuously while the pipeline runs; List&lt;T&gt; gives us
    /// dynamic resizing plus convenient LINQ-based search/filter operations.
    /// </summary>
    class ReportRepository
    {
        private List<Report> _reports;
        public ReportRepository()
        {
            _reports = new List<Report>();
        }
        public void Add(Report report)
        {
            _reports.Add(report);
        }
        public List<Report> GetAll()
        {
            return _reports;
        }
        public List<Report> GetByStatus(ReportStatus status)
        {
            return _reports.Where(reportByStatus => reportByStatus.Status == status).ToList();
        }
        public List<Report> GetByPriority(Priority priority)
        {
            return _reports.Where(reportByPriority => reportByPriority.Priority == priority).ToList();
        }
        public List<Report> Search(string keyword)
        {
            if (string.IsNullOrWhiteSpace(keyword))
            { return _reports; }
            return _reports.Where(text => text.Description.Contains(keyword)).ToList();
        }

        public Report? GetById(int reportId)
        {

            foreach(Report id in GetAll())
            {
                if (reportId == id.ReportId)
                    return id;
            }
            return null;
        }
        public void UpdateStatus(int reportId, ReportStatus newStatus)
        {
            Report currentId = GetById(reportId);
            currentId.Status = newStatus;
        }
        public int GetTotalCount()
        {
            return _reports.Count();
        }
        public int GetCountByStatus(ReportStatus status)
        {
            return _reports.Count(reportByStatus => reportByStatus.Status == status);
        }
    }
}