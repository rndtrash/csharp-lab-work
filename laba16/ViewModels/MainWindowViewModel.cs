using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Reactive;
using Laba16.Models;
using OxyPlot;
using ReactiveUI;
using ReactiveUI.Fody.Helpers;

namespace Laba16.ViewModels;

public partial class MainWindowViewModel : ViewModelBase
{
    [Reactive] public double From { get; set; } = -5;
    [Reactive] public double To { get; set; } = 3;
    [Reactive] public double Minimum { get; set; } = 0;
    [Reactive] public double Maximum { get; set; } = 0;
    
    public ObservableCollection<DataPoint> GraphPointSeries { get; } = new();
    public MainWindowModel Model { get; }

    ReactiveCommand<string, Unit> RenderPlotCommand { get; }
    ReactiveCommand<string, Unit> ClearPlotCommand { get; }

    public MainWindowViewModel(MainWindowModel model)
    {
        Model = model;

        RenderPlotCommand = ReactiveCommand.Create((string _) => RenderPlot());
        ClearPlotCommand = ReactiveCommand.Create((string _) => GraphPointSeries.Clear());

        Model.PropertyChanged += (_, _) => RenderPlot();
    }

    public MainWindowViewModel() : this(new MainWindowModel())
    {
    }

    private void RenderPlot()
    {
        GraphPointSeries.Clear();

        var steps = (To - From) / 0.5;
        var minY = double.MaxValue;
        var minX = double.MaxValue;
        var maxX = double.MinValue;
        var maxY = double.MinValue;
        for (var step = 0; step <= steps; step++)
        {
            var x = From + (To - From) * (double)step / steps;
            var y = F(x);
            GraphPointSeries.Add(new DataPoint(x, y));

            if (y < minY)
            {
                minY = y;
                minX = x;
            }

            if (y > maxY)
            {
                maxY = y;
                maxX = x;
            }
        }

        Minimum = minX;
        Maximum = maxX;
    }

    private double F(double x) => x * Math.Cbrt(Model.A + Model.B * x);
}