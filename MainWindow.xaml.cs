using LiveChartsCore;
using LiveChartsCore.SkiaSharpView;
using LiveChartsCore.SkiaSharpView.Painting;
using SkiaSharp;
using System;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Threading;

namespace LiveChart
{
    public partial class MainWindow : Window
    {
        public ISeries[] Series { get; set; }

        private DispatcherTimer timer;
        private readonly Random random = new Random();
        private double nextValue = 45;

        private ObservableCollection<double> values =
            new ObservableCollection<double>
            {
                10, 25, 18, 35, 28, 45
            };

        public MainWindow()
        {
            InitializeComponent();

            Series = new ISeries[]
            {
                new LineSeries<double>
                {
                    Values = values,
                    LineSmoothness = 0.5,
                    Stroke = new SolidColorPaint(SKColors.Cyan, 3),
                    GeometrySize = 0,
                    Fill = null
                }
            };

            DataContext = this;

            timer = new DispatcherTimer();
            timer.Interval = TimeSpan.FromMilliseconds(500);
            timer.Tick += Timer_Tick;
            timer.Start();
        }

        private void Timer_Tick(object? sender, EventArgs e)
        {
            nextValue += random.Next(-8, 9);

            if (nextValue < 5)
                nextValue = 5;

            values.Add(nextValue);

            if (values.Count > 20)
                values.RemoveAt(0);
        }
    }
}