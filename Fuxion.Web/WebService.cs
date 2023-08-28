
public sealed class WebService : IHostedService
{
    private readonly Task _completedTask = Task.CompletedTask;
    private readonly ILogger<WebService> _logger;
    private int _executionCount = 0;

    public WebService(ILogger<WebService> logger) => _logger = logger;

    public Task StartAsync(CancellationToken stoppingToken)
    {
        _logger.LogInformation("{Service} is running.", nameof(WebService));

        return _completedTask;
    }

    private void DoWork(object? state)
    {
        int count = Interlocked.Increment(ref _executionCount);

        _logger.LogInformation(
            "{Service} is working, execution count: {Count:#,0}",
            nameof(WebService),
            count);
    }

    public Task StopAsync(CancellationToken stoppingToken)
    {
        _logger.LogInformation(
            "{Service} is stopping.", nameof(WebService));

        //Revive

        return _completedTask;
    }
}