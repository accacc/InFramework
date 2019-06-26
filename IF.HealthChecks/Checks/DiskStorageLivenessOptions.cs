using System;
using System.Collections.Generic;
using System.Text;

namespace IF.HealthChecks.Checks
{
    public class DiskStorageLivenessOptions
    {
        internal Dictionary<string, (string DriveName, long MinimumFreeMegabytes)> ConfiguredDrives { get; } = new Dictionary<string, (string DriveName, long MinimumFreeMegabytes)>();

        public DiskStorageLivenessOptions AddDrive(string driveName, long minimumFreeMegabytes = 1)
        {
            ConfiguredDrives.Add(driveName, (driveName, minimumFreeMegabytes));
            return this;
        }
    }
}
