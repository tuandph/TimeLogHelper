using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Threading;

namespace LogFileHelper
{
    public class StopWatchInfo
    {
        public string FunctionName { get; set; }
        public int NestedLevel { get; set; }
    }

    public static class TimeLogCsvHelper
    {
        private static List<string> memories = new List<string>();
        private static Dictionary<StopWatchInfo, Stopwatch> stopwatches = new Dictionary<StopWatchInfo, Stopwatch>();
        private const string FILE_PATH = @"D:\ExcelLog\Logdata";

        public static void SaveCsvFileFromMemories(string filePath = FILE_PATH,string name = "Log")
        {
            File.WriteAllLines(@$"{filePath}-{name}-{Guid.NewGuid()}.csv", memories);
            stopwatches.Clear();
            memories.Clear();
        }

        public static Stopwatch StartMeasure(string name, int nestedLevel = 1)
        {
            Stopwatch stopwatch = new Stopwatch();
            stopwatch.Start();
            stopwatches.Add(new StopWatchInfo { FunctionName = name, NestedLevel = nestedLevel }, stopwatch);
            return stopwatch;
        }

        public static void StopMeasure(this Stopwatch name)
        {
            var element = stopwatches.FirstOrDefault(p => p.Value == name);
            if (element.Key != null)
            {
                var stw = element.Value as Stopwatch;
                stw.Stop();
                var time = stw.Elapsed.TotalSeconds;
                AddToMemories(element.Key, time);
            }
        }

        public static void AddToMemories(StopWatchInfo info, double time)
        {
            var startNested = "";
            List<string> remainingTimeFormat = new List<string>();
            for (int i = 0; i <= info.NestedLevel; i++)
            {
                remainingTimeFormat.Add(",");
            }
            for (int i = 2; i <= info.NestedLevel; i++)
            {
                startNested = startNested + ",";
                remainingTimeFormat.Remove(remainingTimeFormat.FirstOrDefault());
            }
            memories.Add(startNested + info.FunctionName + string.Join("", remainingTimeFormat) + time);
        }
    }
}
