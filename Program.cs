using System;
using IntelligencePipeline.Models.Enums;
using IntelligencePipeline.Models.Reports;
namespace IntelligencePipeline
{
   class programm
    {
        static void Main()

        {

            SoldierReport soldier = new SoldierReport(12, DateTime.Now, 33.0000, 34.0000, "hfeko hdsfklh dsfao ", "hello", "1234567", "8200", 234);
            Console.WriteLine($"Soldier object: {soldier.ToString()}");
            SignalReport s1 = new SignalReport(13,DateTime.Now, 33.0000, 34.0000, "hfeko hdsfklh dsfao ", -40.7, "hvhffvbhvhgv", Language.Arabic, 123);
            Console.WriteLine($"signal object: {s1.CalculateReliabilityScore()}");
            RadarReport r1 = new RadarReport(10, DateTime.Now, 33.0000, 34.0000, "hfeko hdsfklh dsfao ", 234, 231, 678);
            Console.WriteLine($"Radar object: {r1.Classification}");





        }
    }
}


