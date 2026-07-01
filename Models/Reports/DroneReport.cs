using System;
namespace IntelligencePipeline.Models.Reports;

abstract class DroneReport : Report
{
    private int _altitude;
    private int _imageQuality;
    public int Altitude { get => _altitude; set { _altitude = value; } }
    public int ImageQuality { get => _imageQuality; set { _imageQuality = value;  } }
    public DroneReport(int reportId, DateTime timestamp, double latitude,
                        double longitude, string description,
                        int altitude, int imageQuality)
                        : base(reportId, timestamp, latitude, longitude, description)
    {
    Altitude = altitude;
    ImageQuality = imageQuality;
    }
    public override string GetSourceType() => "Drone";
    public override int CalculateReliabilityScore()
    {
        int score = 5;
        if (ImageQuality >= 80)
        { score += 3; }
        else if (ImageQuality >= 50)
        { score += 2; }
        if (Altitude >= 500 & Altitude <= 3000)
        { score += 2; }
        if ( Altitude >7000)
        { score -= 2; }
        return score;
    }

}