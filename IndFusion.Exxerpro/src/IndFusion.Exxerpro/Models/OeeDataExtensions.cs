namespace IndFusion.Exxerpro.Models;

public static class OeeDataExtensions
{
    //public static void ExportToCsv(this List<PerformanceData> data, string filePath)
    //{
    //    var csv = new StringBuilder();

    //    // Add header row
    //    csv.AppendLine("Timestamp,MachineName,Availability,Capacity,DefectiveRate,OEE,Performance,ProducedPieces,Quality,RejectedPieces,RunningTime,StoppingTime");

    //    foreach (var oeeData in data)
    //    {
    //        foreach (var machine in oeeData.Machines)
    //        {
    //            var line = string.Format(CultureInfo.InvariantCulture,
    //                "{0},{1},{2},{3},{4},{5},{6},{7},{8},{9},{10},{11}",
    //                oeeData.Timestamp,
    //                machine.Name,
    //                machine.Availability,
    //                machine.Capacity,
    //                machine.DefectiveRate,
    //                machine.Oee,
    //                machine.Performance,
    //                machine.ProducedPieces,
    //                machine.Quality,
    //                machine.RejectedPieces,
    //                machine.RunningTime,
    //                machine.StoppingTime);

    //            csv.AppendLine(line);
    //        }
    //    }

    //    File.WriteAllText(filePath, csv.ToString());
    //}
}