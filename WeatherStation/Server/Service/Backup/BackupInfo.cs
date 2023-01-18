using System;

namespace WeatherStation.Server.Service.Backup
{
    public class BackupInfo
    {
        public DateTime LastBackupTime { get; set; }
        public double BackupIntervalInHours { get; set; }

    }
}
