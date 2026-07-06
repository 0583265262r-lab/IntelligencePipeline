using IntelligencePipeline.Models.Enums;
using IntelligencePipeline.Models.Reports;
using IntelligencePipeline.Pipeline;
using IntelligencePipeline.Storage;
using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection.Metadata;
namespace IntelligencePipeline
{
   class program
   {
        /// <summary>
        /// Entry point of the application.
        /// This is a simple, menu-driven console program: it shows a menu,
        /// reads the user's choice, and repeats until the user chooses to exit.
        /// All the "smart" logic (validation, scoring, storage) lives in the
        /// other classes - this file is only responsible for talking to the user.
        /// </summary>

        static void Main(string[] args)

        {
            var pipeline = new ReportPipline();
            bool keepRunning = true;

            while (keepRunning)
            {
                ShowMenu();
                string? choice = Console.ReadLine();
                switch (choice)
                {
                    case "1":
                        AddNewReport(pipeline);
                        break;

                    case "2":
                        ShowValidatedReports(pipeline);
                        break;

                    case "3":
                        SearchReports(pipeline);
                        break;

                    case "4":
                        ShowRejectedReports(pipeline);
                        break;

                    case "5":
                        pipeline.DisplayStatistics();
                        break;

                    case "6":
                        keepRunning = false;
                        break;
                    default:
                        Console.WriteLine("Invalid input");
                        return;
                }
            }
        }
            
   
        private static void ShowMenu()
        {

            Console.WriteLine("1. Add new report");
            Console.WriteLine("2. Show correct reports");
            Console.WriteLine("3. Search reports by description");
            Console.WriteLine("4. Show reject reports");
            Console.WriteLine("5. Show statics");
            Console.WriteLine("6. Exit");
            
        }
        private static void AddNewReport(ReportPipline pipeline)
        {
            Console.WriteLine("====== Select a reporting source ====== ");
            Console.WriteLine("1. Drone");
            Console.WriteLine("2. Soldier");
            Console.WriteLine("3. Radar");
            Console.WriteLine("4. Signal");

            string? sourceChoice = Console.ReadLine();
            Report? report = null;
            switch (sourceChoice)
            {
                case "1":
                    report = CreateDroneReport(pipeline);
                    break;
                case "2":
                    report = CreateSoldierReport(pipeline);
                    break;
                case "3":
                    report = CreateRadarReport(pipeline);
                    break;
                case "4":
                    report = CreateSignalReport(pipeline);
                    break;
                default:
                    Console.WriteLine("InValid choice");
                    return;
            }
            if (report == null)
            {
                return;
            }
            pipeline.ProcessReport(report);
            Console.WriteLine(report.GetSummary());
            if (report.Status == ReportStatus.Rejected)
            {
                Console.WriteLine($"Rejection reason: {report.RejectionReason}");
            }
        }
        private static (DateTime Timestamp, double Latitude, double Longitude, string Description) createNewReport()
        {
            Console.WriteLine("");
            DateTime timestamp = ReadDateTime("timestamp (press Enter for the current date): ");
            double latitude = ReadDouble("latitude: ");
            double longitude = ReadDouble("longitude: ");
            string description = ReadString("Description: ");

            return (timestamp, latitude, longitude, description);
        }
        private static DroneReport CreateDroneReport(ReportPipline pipeline)
        {
            var (timestamp, latitude, longitude, description) = createNewReport();
            int Altitude = readInt("Altitude: ");
            int ImageQuality = readInt("ImageQuality: ");
            int reportId = pipeline.GetNextReportId();
            return new DroneReport(reportId, timestamp, latitude, longitude, description, Altitude, ImageQuality);
        }
        private static SoldierReport CreateSoldierReport(ReportPipline pipeline)
        {
            var (timestamp, latitude, longitude, description) = createNewReport();
            string SoldierName = ReadString("SoldierName: ");
            string SoldierId = ReadString("SoldierId: ");
            string Unit = ReadString("Unit: ");
            int ConfidenceLevel = readInt("ConfidenceLevel: ");
            int reportId = pipeline.GetNextReportId();
            return new SoldierReport(reportId, timestamp, latitude, longitude, description, SoldierName, SoldierId, Unit, ConfidenceLevel);
        }
        private static RadarReport CreateRadarReport(ReportPipline pipeline)
        {
            var (timestamp, latitude, longitude, description) = createNewReport();
            int Speed = readInt("Speed: ");
            int Direction = readInt("Direction: ");
            int Distance = readInt("Distance: ");

            int reportId = pipeline.GetNextReportId();
            return new RadarReport(reportId, timestamp, latitude, longitude, description, Speed, Direction, Distance);
        }
        private static SignalReport CreateSignalReport(ReportPipline pipeline)
        {
            var (timestamp, latitude, longitude, description) = createNewReport();
            double Frequency = ReadDouble("Frequency: ");
            string Content = ReadString("Content: ");
            Language language = ReadLanguage();
            int SignalStrength = readInt("SignalStrength");
            int reportId = pipeline.GetNextReportId();
            return new SignalReport(reportId, timestamp, latitude, longitude, description,
                Frequency, Content, language, SignalStrength);
        }
        private static void ShowValidatedReports(ReportPipline pipeline)
        {
            List<Report> reports = pipeline.GetValidatedReports().GetByStatus(ReportStatus.Validated);

            Console.WriteLine("=== valid reports ===");
            if (reports.Count == 0)
            {
                Console.WriteLine("There are no valid reports in the system at the moment.");
                return;
            }

            foreach (Report report in reports)
            {
                Console.WriteLine(report.GetSummary());
            }
        }
        private static void SearchReports(ReportPipline pipeline)
        {
            string keyword = ReadString("Enter a search term (to search within the description): ");
            List<Report> results = pipeline.GetValidatedReports().Search(keyword);

            Console.WriteLine($"=== find {results.Count} results for {keyword} ===");
            foreach (Report report in results)
            {
                Console.WriteLine(report.GetSummary());
            }
        }
        private static void ShowRejectedReports(ReportPipline pipeline)
        {
            List<Report> rejected = pipeline.GetRejectedReports().GetAll();

            Console.WriteLine("=== Rejected reports ===");
            if (rejected.Count == 0)
            {
                Console.WriteLine("No rejected reports.");
                return;
            }

            foreach (Report report in rejected)
            {
                Console.WriteLine($"#{report.ReportId} [{report.GetSourceType()}] - {report.RejectionReason}");
            }
        }


        private static string ReadString(string prompt)
        {
            Console.Write(prompt);
            string? input = Console.ReadLine();
            return input ?? string.Empty;
        }
         private static int readInt(string prompt)
        {
            while (true)
            {
                Console.Write(prompt);
                string? input = Console.ReadLine();

                if (int.TryParse(input, out int result))
                {
                    return result;
                }

                Console.WriteLine("Invalid input - please enter a whole number. Try again.");
            }
        }
        private static double ReadDouble(string prompt)
        {
            while (true)
            {
                Console.Write(prompt);
                string? input = Console.ReadLine();

                if (double.TryParse(input, out double result))
                {
                    return result;
                }

                Console.WriteLine("Invalid input - please enter a number. Try again.");
            }
        }
        private static DateTime ReadDateTime(string prompt)
        {
            while (true)
            {
                Console.Write(prompt);
                string? input = Console.ReadLine();

                if (string.IsNullOrWhiteSpace(input))
                {
                    return DateTime.Now;
                }

                if (DateTime.TryParse(input, out DateTime result))
                {
                    return result;
                }

                Console.WriteLine("Invalid date format. Try again (or press Enter for the current date).");
            }
        }
        private static Language ReadLanguage()
        {
            Console.WriteLine(" (Language):");
            Console.WriteLine("1. Hebrew");
            Console.WriteLine("2. Arabic");
            Console.WriteLine("3. English");
            Console.WriteLine("4. Russian");
            Console.WriteLine("5. Other");

            while (true)
            {
                string? input = Console.ReadLine();

                switch (input)
                {
                    case "1": return Language.Hebrew;
                    case "2": return Language.Arabic;
                    case "3": return Language.English;
                    case "4": return Language.Russian;
                    case "5": return Language.Other;
                    default:
                        Console.WriteLine("Invalid choice, try again.");
                        break;
                }
            }
        }
    }
}



