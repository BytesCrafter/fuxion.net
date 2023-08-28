using Microsoft.Extensions.Hosting.Systemd;
using Microsoft.VisualBasic;

namespace Fuxion.Service
{
    public class Worker : BackgroundService
    {
        private readonly ILogger<Worker> _logger;

        public Worker(ILogger<Worker> logger, IHostLifetime lifetime)
        {
            _logger = logger;
            _logger.LogInformation("IsSystemd: {isSystemd}", lifetime.GetType() == typeof(SystemdLifetime));
            _logger.LogInformation("IHostLifetime: {hostLifetime}", lifetime.GetType());

            Thread webapp = new Thread(Program.StartHost);
            webapp.IsBackground = false;
            webapp.Start();
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                _logger.LogInformation("Fuxion NET - Web Service running at: {time}", DateTimeOffset.Now);
                //_logger.LogWarning("A warning from Fuxion NET - Web Service at: {time}", DateTimeOffset.Now);
                //_logger.LogError("An error from Fuxion NET - Web Service at: {time}", DateTimeOffset.Now);
                await Task.Delay(1000, stoppingToken);
            }
        }
    }
}