using System;
using IntelligencePipeline.Models.Reports;

namespace IntelligencePipeline.Models.Reports;

class SoldierReport : Report
{
    public string SoldierName { get; protected set; }
    public string SoldierID { get; }
    public string Unit { get; protected set; }
    public int ConfidenceLevel { get; protected set; }
    public SoldierReport(int reportId, DateTime timestamp, double latitude,
                            double longitude, string description,
                            string soldierName, string soldierID, string unit, int confidenceLevel)
                            : base(reportId, timestamp, latitude, longitude, description)
    {
        SoldierName = soldierName;
        SoldierID = soldierID;
        Unit = unit;
        ConfidenceLevel = confidenceLevel;
    }
    public override string GetSourceType() => "Soldier";
    public override int CalculateReliabilityScore()
    {
        int score = 4;
        if (Description.Contains(" weapon ") | Description.Contains(" vehicle ") |
            Description.Contains(" movement ") | Description.Contains(" explosion "))
            { score += 1; }
        return score;

    }



}