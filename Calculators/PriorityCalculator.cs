using IntelligencePipeline.Models.Enums;
using IntelligencePipeline.Models.Reports;
using System;
namespace IntelligencePipeline.Calculators
{
    class PriorityCalculator
    {
        string[] CriticalKaywords = { "missile", "explosion", "attack", "fire" };
        string[] HighKaywords = { "weapon", "suspicious", "border" };
        string[] MediumKaywords = { "movement", "vehicle", "activity" };
        public Priority Calculate(Report report)
        {
            if (IsCritical(report))
                return Priority.Critical;
            if (IsHigh(report))
                return Priority.High;
            if (IsMedium(report))
                return Priority.Medium;
            return Priority.Low;
        }
        public bool IsCritical(Report report)
        {
            foreach(string kayword in CriticalKaywords)
            {
                if (report.Description.Contains(kayword))
                    return true;
                if (report is SignalReport signal)
                {
                    if (signal.Content.Contains(kayword))
                        return true;
                }
                
            }
            if (report is RadarReport radar)
            {
                if (radar.Speed >= 800)
                    return true;
            }
            if (report is SignalReport signalreport)
            {
                if (signalreport.Content.Contains("attack") & signalreport.Content.Contains("target"))
                    return true;
            }
            return false;
        }
        public bool IsHigh(Report report)
        {
            foreach (string kayword in HighKaywords)
            {
                if (report.Description.Contains(kayword))
                    return true;
            }
            if (report is DroneReport drone)
            {
                if (drone.Altitude < 500)
                    return true;
            }
            if (report is RadarReport radar)
            {
                if (radar.Speed >= 400)
                    return true;
            }
            if(report is SoldierReport soldier)
            {
                if (soldier.ConfidenceLevel >= 4 & soldier.Description.Contains("movement"))
                    return true;
            }
            return false;
        }
        public bool IsMedium(Report report)
        {
            foreach (string kayword in MediumKaywords)
            {
                if (report.Description.Contains(kayword))
                    return true;
            }
            if (report is RadarReport radar)
            {
                if (radar.Speed >= 120)
                    return true;
            }
            if (report.ReliabilityScore >= 7)
                return true;
            return false;
        }

    }
}