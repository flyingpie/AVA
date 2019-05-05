using MUI.DI;
using MUI.Win32;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace AVA.Plugin.Footer
{
    [Service]
    public class SysMonService
    {
        public float CpuUsage { get; private set; }

        public float MemUsage { get; private set; }

        public List<DriveStat> Drives { get; set; } = new List<DriveStat>();

        private PerformanceCounter _cpuCounter;

        private Task _task;

        public SysMonService()
        {
            _cpuCounter = new PerformanceCounter();
            _cpuCounter.CategoryName = "Processor";
            _cpuCounter.CounterName = "% Processor Time";
            _cpuCounter.InstanceName = "_Total";

            _task = StartBackgroundTask();
        }

        private Task StartBackgroundTask() => Task.Run(async () =>
        {
            while (true)
            {
                try
                {
                    var memAvailableMB = PerformanceInfo.GetPhysicalAvailableMemoryInMiB();
                    var memTotalIMB = PerformanceInfo.GetTotalMemoryInMiB();
                    var memUseMB = memTotalIMB - memAvailableMB;

                    CpuUsage = (float)Math.Round(_cpuCounter.NextValue() / 100, 2);
                    MemUsage = (float)Math.Round((float)memUseMB / memTotalIMB, 2);

                    Drives = DriveInfo
                        .GetDrives()
                        .Where(d => d.IsReady)
                        .Select(d => new DriveStat()
                        {
                            Name = d.Name.Substring(0, 2),
                            Usage = (float)Math.Round((d.TotalSize - d.AvailableFreeSpace) / (float)d.TotalSize, 2)
                        })
                        .ToList();
                }
                catch { }

                await Task.Delay(TimeSpan.FromSeconds(1));
            }
        });

        public class DriveStat
        {
            public string Name { get; set; }

            public float Usage { get; set; }
        }
    }
}