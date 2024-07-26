
namespace IndFusion.Exxerpro.Worker;
public sealed class IndFusionWorker(ILogger<IndFusionWorker> logger) : BackgroundService
{
    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        while (!stoppingToken.IsCancellationRequested)
        {
            logger.LogInformation("Worker running at: {time}", DateTimeOffset.Now);
            await Task.Delay(1_000, stoppingToken);
        }
    }
}