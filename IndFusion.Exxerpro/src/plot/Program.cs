using IndFusion.Exxerpro.Models;
using OxyPlot.Series;
using OxyPlot;
using OxyPlot.Axes;
using System;
using System.Collections.Generic;

public class Program
{
    public static void Main()
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

        // Create scatter plot
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

        // Create histogram plot
        var histogramSeries = CreateHistogramSeries(samples, 20);

        // Configure the y-axis to be fixed between 0 and 1
        var yAxis = new LinearAxis
        {
            Position = AxisPosition.Left,
            Minimum = 0,
            Maximum = 1,
            Title = "Values"
        };
        model.Axes.Add(yAxis);

        // Configure the x-axis for the scatter plot
        var xAxis = new LinearAxis
        {
            Position = AxisPosition.Bottom,
            Title = "Index"
        };
        model.Axes.Add(xAxis);

        // Configure the categorical axis for the histogram
        var categoryAxis = new CategoryAxis
        {
            Position = AxisPosition.Bottom,
            Key = "CategoryAxis",
            Title = "Bins"
        };
        model.Axes.Add(categoryAxis);

        model.Series.Add(scatterSeries);
        model.Series.Add(histogramSeries);

        using var stream = new System.IO.FileStream(outputFileName, System.IO.FileMode.Create);
        var pdfExporter = new PdfExporter { Width = 600, Height = 400 };
        pdfExporter.Export(model, stream);
    }

    private static BarSeries CreateHistogramSeries(List<double> samples, int binCount)
    {
        var histogramSeries = new BarSeries
        {
            FillColor = OxyColors.SkyBlue,
            StrokeColor = OxyColors.DarkBlue,
            StrokeThickness = 1,
            XAxisKey = "CategoryAxis",
            YAxisKey = "CategoryAxis"
        };

        double min = 0.0;
        double max = 1.0;
        double binWidth = (max - min) / binCount;
        var bins = new int[binCount];

        foreach (var sample in samples)
        {
            int binIndex = (int)((sample - min) / binWidth);
            if (binIndex >= 0 && binIndex < binCount)
            {
                bins[binIndex]++;
            }
        }

        for (int i = 0; i < binCount; i++)
        {
            histogramSeries.Items.Add(new BarItem { Value = bins[i] });
        }

        return histogramSeries;
    }
}
