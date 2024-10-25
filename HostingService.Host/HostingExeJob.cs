using HostingService.APIProxy;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using WindowsService1.Web.Server.Api;


namespace HostingService
{
    public class HostingExeJob : BackgroundService
    {
        private readonly ILogger<HostingExeJob> _logger;


        public HostingExeJob(ILogger<HostingExeJob> logger)
        {
            _logger = logger;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                try
                {
                    KeepAlive();
                }
                catch (Exception e)
                {
                    _logger.LogError($"error:{e.Message}");
                }
                _logger.LogInformation("Worker running at: {time}", DateTimeOffset.Now);
                await Task.Delay(5 * 1000, stoppingToken);
            }
        }

        public void KeepAlive()
        {
            if (HostInfoSingleTon.Instance.Exe == null || HostInfoSingleTon.Instance.Exe.Count <= 0)
            {
                return;
            }
            foreach (var item in HostInfoSingleTon.Instance.Exe)
            {
                if (string.IsNullOrEmpty(item.ProcessName))
                {
                    _logger.LogInformation($"processname is null skip item.");
                    return;
                }

                var all = Process.GetProcesses().Select(f => f.ProcessName).ToList();
                var json = JsonConvert.SerializeObject(all);

                if (Process.GetProcessesByName(item.ProcessName).ToList().Count > 0)
                {
                    _logger.LogInformation($"{item.ProcessName} is alive.");
                }
                else
                {
                    Exec(item);
                }
            }

        }

        private void Exec(ExeParameter item)
        {
            if (item == null)
            {
                return;
            }


            if (item.NeedBypassUAC)
            {
                switch (item.BypassUACType)
                {
                    case 0:
                        UserProcess.PROCESS_INFORMATION pInfo = new UserProcess.PROCESS_INFORMATION();
                        UserProcess.StartProcessAndBypassUAC(item.ExePath, string.Empty, out pInfo);
                        break;

                    case 1:
                        WinAPI_Interop.CreateProcess(item.ExePath);
                        break;

                    case 2:
                        Interops.CreateProcess(item.ExePath, @"C:\Windows\System32\");
                        break;

                    case 3:
                        SessionUtility.CreateProcess(@"C:\Windows\System32\", item.ExePath, 1);//string path=@"C:\Users\Administrator\Test.exe";
                        break;

                    case 4:
                        ApplicationLoader.PROCESS_INFORMATION procInfo;
                        ApplicationLoader.StartProcessAndBypassUAC(item.ExePath, out procInfo);//string path=@"C:\Users\Administrator\Test.exe";
                        break;

                    default:
                        break;
                }
            }
            else
            {
                Process process = new Process();
                process.StartInfo = new ProcessStartInfo
                {
                    FileName = item.ExePath,
                };
                if (!string.IsNullOrEmpty(item.Parameter))
                {
                    process.StartInfo.Arguments = item.Parameter;
                }
                process.Start();
            }

            _logger.LogInformation($"start {item}.");
        }
    }
}
