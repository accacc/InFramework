using IF.Core.Common;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;

namespace IF.HealthChecks.Checks
{
    public static partial class HealthCheckBuilderExtensions
    {
        // System checks

        public static HealthCheckBuilder AddPrivateMemorySizeCheck(this HealthCheckBuilder builder, long maxSize)
             => AddMaxValueCheck(builder, $"PrivateMemorySize({maxSize})", maxSize, () => Process.GetCurrentProcess().PrivateMemorySize64);

        public static HealthCheckBuilder AddPrivateMemorySizeCheck(this HealthCheckBuilder builder, long maxSize, TimeSpan cacheDuration)
            => AddMaxValueCheck(builder, $"PrivateMemorySize({maxSize})", maxSize, () => Process.GetCurrentProcess().PrivateMemorySize64, cacheDuration);

        public static HealthCheckBuilder AddVirtualMemorySizeCheck(this HealthCheckBuilder builder, long maxSize)
            => AddMaxValueCheck(builder, $"VirtualMemorySize({maxSize})", maxSize, () => Process.GetCurrentProcess().VirtualMemorySize64);

        public static HealthCheckBuilder AddVirtualMemorySizeCheck(this HealthCheckBuilder builder, long maxSize, TimeSpan cacheDuration)
            => AddMaxValueCheck(builder, $"VirtualMemorySize({maxSize})", maxSize, () => Process.GetCurrentProcess().VirtualMemorySize64, cacheDuration);

        public static HealthCheckBuilder AddWorkingSetCheck(this HealthCheckBuilder builder, long maxSize)
            => AddMaxValueCheck(builder, $"WorkingSet({maxSize})", maxSize, () => Process.GetCurrentProcess().WorkingSet64);

        public static HealthCheckBuilder AddWorkingSetCheck(this HealthCheckBuilder builder, long maxSize, TimeSpan cacheDuration)
            => AddMaxValueCheck(builder, $"WorkingSet({maxSize})", maxSize, () => Process.GetCurrentProcess().WorkingSet64, cacheDuration);


        static double ConvertKilobytesToMegabytes(long kilobytes)
        {
            return kilobytes / 1024f;
        }

        static double ConvertBytesToMegabytes(long bytes)
        {
            return (bytes / 1024f) / 1024f;
        }


        public static HealthCheckBuilder AddSystemStorageCheck(this HealthCheckBuilder builder, string name, Action<DiskStorageLivenessOptions> options)
        {
            Guard.ArgumentNotNull(nameof(builder), builder);

            return AddSystemStorageCheck(builder, name, options, builder.DefaultCacheDuration);
        }


        public static HealthCheckBuilder AddSystemStorageCheck(this HealthCheckBuilder builder, string name, Action<DiskStorageLivenessOptions> options, TimeSpan cacheDuration)
        {
            builder.AddCheck($"SystemStorageCheck({name})", () =>
            {
                try
                {
                    var _options = new DiskStorageLivenessOptions();
                    options?.Invoke(_options);

                    var configuredDrives = _options.ConfiguredDrives.Values;

                    foreach (var item in configuredDrives)
                    {
                        var systemDriveInfo = GetSystemDriveInfo(item.DriveName);

                        if (systemDriveInfo.Exists)
                        {
                            if (systemDriveInfo.ActualFreeMegabytes < item.MinimumFreeMegabytes)
                            {

                                return HealthCheckResult.Unhealthy($"Minimum configured megabytes for disk {item.DriveName} is {item.MinimumFreeMegabytes} but actual free space are {systemDriveInfo.ActualFreeMegabytes} megabytes");
                            }
                        }
                        else
                        {

                            return HealthCheckResult.Unhealthy($"Configured drive {item.DriveName} is not present on system");
                        }
                    }


                    return HealthCheckResult.Healthy($"ElasticSearchLoggerCheck({name}):  Healthy");
                }
                catch (Exception ex)
                {

                    return HealthCheckResult.Unhealthy($"ElasticSearchLoggerCheck({name}): Exception during check: {ex.Message}");
                }

            }, cacheDuration);

            return builder;
        }
        private static (bool Exists, long ActualFreeMegabytes) GetSystemDriveInfo(string driveName)
        {
            var driveInfo = DriveInfo.GetDrives()
                .FirstOrDefault(drive => String.Equals(drive.Name, driveName, StringComparison.InvariantCultureIgnoreCase));

            if (driveInfo != null)
            {
                return (true, driveInfo.AvailableFreeSpace / 1024 / 1024);
            }

            return (false, 0);
        }
    }
}
