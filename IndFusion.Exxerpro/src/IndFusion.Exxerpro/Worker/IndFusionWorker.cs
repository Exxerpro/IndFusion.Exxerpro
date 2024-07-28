﻿using Google.Protobuf.WellKnownTypes;
using IndFusion.Exxerpro.Data;
using IndFusion.Exxerpro.Services;
using Microsoft.Extensions.Logging;

namespace IndFusion.Exxerpro.Worker;

public class IndFusionWorker(ILogger<IndFusionWorker> logger, OeeState oeeState) : BackgroundService
{

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        logger.LogInformation("Worker starting at {Time}", DateTime.Now);

        foreach (var machineOee in oeeState.Machines)
        {
            logger.LogInformation(" Machine updated at {Time} {Machine}", DateTime.Now, machineOee.ToString());
        }
        while (!stoppingToken.IsCancellationRequested)
        {
            UpdateOeeData();
            //await Task.Delay(TimeSpan.FromSeconds(5), stoppingToken); // Update every 5 minutes
            await Task.Delay(TimeSpan.FromMinutes(5), stoppingToken); // Update every 5 minutes
        }
    }
    private void UpdateOeeData()
    {
      var oeeData = oeeState.GenerateNewDataPoint( DateTime.Now);
        oeeState.UpdateData(oeeData);
        logger.LogInformation("OEE data updated at {Time}", DateTime.Now);
        foreach (var machineOee in oeeState.Machines)
        {
            logger.LogInformation(" Machine updated at {Time} {Machine}", DateTime.Now, machineOee.ToString());
        }
        
    }
}