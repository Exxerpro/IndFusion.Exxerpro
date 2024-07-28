using System.Globalization;
using System.Text;

namespace IndFusion.Exxerpro.Domain.Models;

public static class OeeDataExtensions
{
    public static void ExportHistoricalDataToCsv(this OeeState oeeState, string filePath)
    {
        var csv = new StringBuilder();

        // Add header row
        csv.AppendLine("StartTime,EndTime,MachineName,Availability,Capacity,DefectiveRate,OEE,Performance,ProducedPieces,Quality,RejectedPieces,RunningTime,StoppingTime");


        foreach (var (machine, data) in oeeState.Machines)
        {
            foreach (var productionData in data.HistoricData)
            {
                var line = string.Format(CultureInfo.InvariantCulture,
                    "{0},{1},{2},{3},{4},{5},{6},{7},{8},{9},{10},{11},{12}",
                    productionData.StartTime,
                    productionData.EndTime,
                    machine,
                    data.Indicator.Availability,
                    data.Capacity,
                    productionData.RejectedPieces / (productionData.ProducedPieces > 0 ? productionData.ProducedPieces : 1), // DefectiveRate
                    data.Indicator.Oee,
                    data.Indicator.Performance,
                    productionData.ProducedPieces,
                    data.Indicator.Quality,
                    productionData.RejectedPieces,
                    productionData.RunningTime,
                    productionData.StoppingTime);

                csv.AppendLine(line);
            }
        }



        File.WriteAllText(filePath, csv.ToString());
    }
}