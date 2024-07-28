using IndFusion.Exxerpro.Models;
using OxyPlot.Series;
using OxyPlot;
using OxyPlot.Axes;

public class Program2
{
    public static void Main2()
    {
        const int sampleSize = 1000;
        var random = new Random();

        var sampleWithTendencyToTheRight = new List<double>();
        var sampleWithTendencyToTheLeft = new List<double>();

        for (var i = 0; i < sampleSize; i++)
        {
            sampleWithTendencyToTheRight.Add(MachineOeeExtensions.SampleWithTendencyToTheRight(random));
            sampleWithTendencyToTheLeft.Add(MachineOeeExtensions.SampleWithTendencyToTheLeft(random));
        }

        PlotSamples(sampleWithTendencyToTheRight, "SampleWithTendencyToTheRight", "SampleWithTendencyToTheRight.pdf");
        PlotSamples(sampleWithTendencyToTheLeft, "SampleWithTendencyToTheLeft", "SampleWithTendencyToTheLeft.pdf");

        Console.WriteLine("Plots generated successfully.");
    }

    private static void PlotSamples(List<double> samples, string title, string outputFileName)
    {
        var model = new PlotModel { Title = title };

        var scatterSeries = new ScatterSeries
        {
            MarkerType = MarkerType.Circle,
            MarkerStroke = OxyColors.SkyBlue,
            MarkerFill = OxyColors.SkyBlue,
            MarkerSize = 2
        };

        for (int i = 0; i < samples.Count; i++)
        {
            scatterSeries.Points.Add(new ScatterPoint(i, samples[i]));
        }
        // Configure the y-axis to be fixed between 0 and 1
        var yAxis = new LinearAxis
        {
            Position = AxisPosition.Left,
            Minimum = 0,
            Maximum = 1,
            Title = "Values"
        };
        model.Axes.Add(yAxis);

        // Configure the x-axis
        var xAxis = new LinearAxis
        {
            Position = AxisPosition.Bottom,
            Title = "Index"
        };
        model.Axes.Add(xAxis);
        model.Series.Add(scatterSeries);

        using var stream = new System.IO.FileStream(outputFileName, System.IO.FileMode.Create);
        var pdfExporter = new PdfExporter { Width = 600, Height = 400 };
        pdfExporter.Export(model, stream);
    }
}