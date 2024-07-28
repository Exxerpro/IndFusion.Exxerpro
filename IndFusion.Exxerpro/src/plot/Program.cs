using IndFusion.Exxerpro.Models;
using OxyPlot;
using OxyPlot.Axes;
using OxyPlot.Series;
using static System.IO.FileMode;
using PdfExporter = OxyPlot.SkiaSharp.PdfExporter; // Alias for PdfExporter

namespace plot;

public class Program
{
    public static void Main()
    {

        CreateData();

        const int sampleSize = 1000;
        var random = new Random();

        var sampleWithTendencyToTheRight = new List<double>();
        var sampleWithTendencyToTheLeft = new List<double>();

        for (var i = 0; i < sampleSize; i++)
        {
            sampleWithTendencyToTheRight.Add(MachineOeeExtensions.SampleWithTendencyToTheRight(random));
            sampleWithTendencyToTheLeft.Add(MachineOeeExtensions.SampleWithTendencyToTheLeft(random));
        }
        // Generate the histogram
        var histogramWithTendencyToTheRight = GenerateHistogram(sampleWithTendencyToTheRight, 20);
        var histogramWithTendencyToTheLeft = GenerateHistogram(sampleWithTendencyToTheLeft, 20);


        PlotSamples(sampleWithTendencyToTheRight, "SampleWithTendencyToTheRight", "SampleWithTendencyToTheRight.pdf");
        PlotSamples(histogramWithTendencyToTheRight, "HistogramSampleWithTendencyToTheRight", "HistogramSampleWithTendencyToTheRight.pdf");
        PlotSamples(sampleWithTendencyToTheLeft, "SampleWithTendencyToTheLeft", "SampleWithTendencyToTheLeft.pdf");
        PlotSamples(histogramWithTendencyToTheLeft, "HistogramSampleWithTendencyToTheLeft", "HistogramSampleWithTendencyToTheLeft.pdf");

        Console.WriteLine("Plots generated successfully.");
    }

    private static void CreateData()
    {
        // Create an instance of OeeState
        var dateTimeMachine = new DateTimeMachine(); // Replace with your implementation of IDateTimeMachine
        var oeeState = new OeeState(dateTimeMachine);

        // Generate some data (if not already generated)
        oeeState.GeneratePastData();

        // Export to CSV
        oeeState.ExportHistoricalDataToCsv("historical_data.csv");


    }


    private static void PlotSamples(List<int> samples, string title, string outputFileName)
    {
        var model = new PlotModel { Title = title };

        var barSeries = new BarSeries
        {
            FillColor = OxyColors.SkyBlue,
            StrokeColor = OxyColors.SkyBlue,
            StrokeThickness = 1,
            LabelPlacement = LabelPlacement.Inside,
            LabelFormatString = "{0}"
        };

        foreach (var t in samples)
        {
            barSeries.Items.Add(new BarItem(t));
        }

        var maxY = Math.Max(1, samples.Max());

        var yAxis = new CategoryAxis
        {
            Position = AxisPosition.Left,
            Minimum = 0,
            Maximum = maxY,
            Title = "Values"
        };
        model.Axes.Add(yAxis);

        var xAxis = new CategoryAxis
        {
            Position = AxisPosition.Bottom,
            Title = "Index",
            Key = "IndexAxis"
        };
        xAxis.Labels.AddRange(samples.Select((_, i) => i.ToString()).ToList());
        model.Axes.Add(xAxis);

        model.Series.Add(barSeries);

        using var stream = new FileStream(outputFileName, FileMode.Create);
        var pdfExporter = new PdfExporter { Width = 600, Height = 400 };
        pdfExporter.Export(model, stream);
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

        for (var i = 0; i < samples.Count; i++)
        {
            scatterSeries.Points.Add(new ScatterPoint(i, samples[i]));
        }
        // Configure the y-axis to be fixed between 0 and 1
        // Calculate the maximum value of the sample for y-axis

        var maxY = Math.Max(1, samples.Max());

        var yAxis = new LinearAxis
        {
            Position = AxisPosition.Left,
            Minimum = 0,
            Maximum = maxY,
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

        using var stream = new FileStream(outputFileName, Create);
        var pdfExporter = new PdfExporter { Width = 600, Height = 400 };
        pdfExporter.Export(model, stream);
    }


    private static List<int> GenerateHistogram(List<double> sample, int numberOfSlots)
    {
        var slotSize = 1.0 / numberOfSlots;

        var histogram = Enumerable.Range(0, numberOfSlots)
            .Select(slot => sample.Count(value => value >= slot * slotSize && value < (slot + 1) * slotSize))
            .ToList();

        return histogram;
    }
}