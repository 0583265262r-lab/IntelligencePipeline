using IntelligencePipeline.Models.Enums;
using System;
namespace IntelligencePipeline.Models.Reports
{

    abstract class Report
    {
        
        private int _reportId;
        private DateTime _timestamp;
        private double _latitude;
        private double _longitude;
        private string _description;
        private ReportStatus _status;
        private Priority _priority;
        private Classification _classification;
        private int _reliabilityScore;
        private string _rejectionReason;

        public int ReportId { get; protected set; }
        public DateTime Timestamp { get => _timestamp; protected set { _timestamp = value; } }

        public double Latitude { get => _latitude; protected set { _latitude = value; } }
        public double Longitude { get => _longitude; protected set { _longitude = value; } }
        public string Description { get => _description; protected set { _description = value; } }
        public ReportStatus Status { get => _status; set { _status = value; } }
        public Priority Priority { get => _priority; set { _priority = value; } }
        public Classification Classification { get => _classification; set { _classification = value; } }
        public int ReliabilityScore { get => _reliabilityScore; set { _reliabilityScore = value; } }
        public string? RejectionReason { get ; set; } 
        protected Report(int reportId, DateTime timestamp, double latitude,
                         double longitude, string description)
        {

            ReportId = reportId;
            Timestamp = timestamp;
            Latitude = latitude;
            Longitude = longitude;
            Description = description;

            Status = ReportStatus.New;
            Priority = Priority.Low;
            Classification = Classification.Unclassified;
            ReliabilityScore = 0;
            RejectionReason = null;

        }
        public abstract string GetSourceType();
        public abstract int CalculateReliabilityScore();
        public virtual string GetSummary()
        {
            return $"[{GetSourceType()}] #{ReportId} - {Status} - Priority: {Priority} - " +
                   $"Classification: {Classification} - Reliability: {ReliabilityScore}/10";
        }

        //public override string ToString()

        //{
        //    return
        //        $"Report ID: {_reportId} | Timestamp: {Timestamp} | Latitude: {Latitude} |" +
        //            $" Longitude: {Longitude} | Description: {Description} | Status: {Status}";

        //}
    }
}

