using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using System;
using System.Threading;
using System.Threading.Tasks;
using Newtonsoft.Json;
using System.IO;
using Dropbox.Api;
using System.Text;
using Dropbox.Api.Files;

namespace WeatherStation.Server.Service.Backup
{
    public class BackupService : BackgroundService
    {
        private readonly string backupInfoFilePath;
        private readonly string dbFilePath;
        private readonly PeriodicTimer timer = new PeriodicTimer(TimeSpan.FromSeconds(1));
        private readonly IConfiguration configuration;
        private int defaultIntervalInHours = 24;
        private string filename = "BackupInfo.json";
        private BackupInfo backupInfo;
        public BackupService(IConfiguration configuration, IWebHostEnvironment environment)
        {
            this.backupInfoFilePath = Path.Combine(environment.ContentRootPath, "Service", "Backup", filename);
            this.dbFilePath = Path.Combine(environment.ContentRootPath, "DB", "Data.db");
            if (!File.Exists(backupInfoFilePath))
                File.Create(backupInfoFilePath).Close();
            this.configuration = configuration;
            this.backupInfo = JsonConvert.DeserializeObject<BackupInfo>(File.ReadAllText($"{backupInfoFilePath}")) ?? new BackupInfo() { BackupIntervalInHours = defaultIntervalInHours, LastBackupTime = DateTime.MinValue };
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while ((await timer.WaitForNextTickAsync(stoppingToken) && !stoppingToken.IsCancellationRequested))
            {
                if (DateTime.Compare(backupInfo.LastBackupTime.AddHours(backupInfo.BackupIntervalInHours), DateTime.UtcNow) <= 0)
                    await CreateBackupAsync(CreateCopy());
            }
        }
        private string CreateCopy()
        {
            var copy = Path.Combine(Path.GetDirectoryName(dbFilePath), "copy.db");
            if (File.Exists(dbFilePath))
                File.Copy(dbFilePath, copy,true);

            return copy;
        }
        private async Task CreateBackupAsync(string copyFilePath)
        {
            if (File.Exists(copyFilePath))
                await UploadFileAsync(copyFilePath, new DropboxClient(configuration["DropboxToken"]));

            File.Delete(copyFilePath);
        }

        private async Task UploadFileAsync(string copyFilePath, DropboxClient dropboxClient)
        {
            using (var mem = new FileStream(path: copyFilePath, FileMode.Open))
            {
                backupInfo.LastBackupTime = DateTime.UtcNow;
                var updated = await dropboxClient.Files.UploadAsync($"/{backupInfo.LastBackupTime.ToString("yyyyMMddHHmmss")}.db", WriteMode.Overwrite.Instance, body: mem);
                File.WriteAllText(backupInfoFilePath, JsonConvert.SerializeObject(backupInfo));
            }
        }
    }
}
