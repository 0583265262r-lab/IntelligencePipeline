using Enumerations;
using System;
using IntelligencePipeline.Models.Reports;

namespace IntelligencePipeline.Models.Reports;
class SignalReport : Report
{
    public double Frequency { get; protected set; }
    public string Content { get; protected set; }
    public Language Language { get; protected set; }
    public int SignalStrength { get; protected set; }
    public SignalReport(int reportId, DateTime timestamp, double latitude,
                        double longitude, string description,
                        double frequency,string content, Language language,
                        int signalStrength)
                        : base(reportId, timestamp, latitude, longitude, description)
    {
        Frequency = frequency;
        Content = content;
        Language = language;
        SignalStrength = signalStrength;
        ReliabilityScore = CalculateReliabilityScore();


    }
    public override string GetSourceType() => "Signal";
    public override int CalculateReliabilityScore()
    {
        int score = 5;
        if (SignalStrength >= -40)
        { score += 3; }
        else if (SignalStrength >= -70)
        { score += 2; }
        if (Content.Contains(" attack ") || Content.Contains(" vehicle ") ||
            Content.Contains(" border ") || Content.Contains(" target "))
        { score += 1; }
        if (SignalStrength < -100)
        { score -= 2; }
        return score; 
    }
}
