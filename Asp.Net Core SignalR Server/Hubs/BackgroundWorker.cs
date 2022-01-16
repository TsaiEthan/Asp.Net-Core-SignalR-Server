using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.SignalR;
using Microsoft.Extensions.Hosting;

namespace Asp.Net_Core_SignalR_Server.Hubs
{
    public class DataHubWorker : BackgroundService
    {
        private readonly IHubContext<DataHub> heartbeat;
        public DataHubWorker(IHubContext<DataHub> heartbeat)
        {
            this.heartbeat = heartbeat;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            /*
            var timeZoneInfo = TimeZoneInfo.FindSystemTimeZoneById("Taipei Standard Time");
            while (!stoppingToken.IsCancellationRequested)
            {
                DateTimeOffset taipeiStandardTimeOffset = DateTimeOffset.Now.ToOffset(timeZoneInfo.BaseUtcOffset);
                Console.WriteLine("heartbeat " + taipeiStandardTimeOffset.ToString("yyyy-MM-dd HH:mm:ss:fff"));
                await this.heartbeat.Clients.All.SendAsync("Heartbeat", taipeiStandardTimeOffset.ToString("yyyy-MM-dd HH:mm:ss:fff"));
                await Task.Delay(300000);//延遲5分鐘(1000MS*60秒*5分鐘)
            }
            
            int count = 0;
            while (!stoppingToken.IsCancellationRequested)
            {
                if (count > 10000)
                    count = 0;
                Console.WriteLine("heartbeat：HB "+count);
                await this.heartbeat.Clients.All.SendAsync("Heartbeat", "HB "+ count, stoppingToken);
                await Task.Delay(3000, stoppingToken);//延遲3秒
                count++;
            }
            */
        }
    }
}
