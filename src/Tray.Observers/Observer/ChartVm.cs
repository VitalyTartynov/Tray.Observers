using System;
using System.Windows.Controls;
using OxyPlot;
using OxyPlot.Axes;
using LineSeries = OxyPlot.Series.LineSeries;

namespace Tray.Observers
{
    class ChartVm
    {
        private readonly LineSeries _data;
        private readonly Cache _cache;

        public ChartVm(Cache cache)
        {
            PlotData = new PlotModel { Title = "График" };

            _cache = cache;
            _data = new LineSeries();

            foreach (var element in cache.GetAll())
            {
                var time = DateTimeAxis.ToDouble(element.Stamp);
                _data.Points.Add(new DataPoint(time, element.Value));
            }

            PlotData.Axes.Add(new DateTimeAxis{ StringFormat = "mm:ss"});
            PlotData.Axes.Add(new LinearAxis {Maximum = 100, Minimum = 0});
            PlotData.Series.Add(_data);

            _cache.OnValuesChanged += OnValuesChanged;
        }

        private void OnValuesChanged(object sender, AddingNewItemEventArgs addingNewItemEventArgs)
        {
            if (_data.Points.Count > ((Cache) sender).MaxCount)
            {
                _data.Points.RemoveAt(0);
            }

            var newPoint = addingNewItemEventArgs.NewItem as CacheItem;

            _data.Points.Add(new DataPoint(DateTimeAxis.ToDouble(newPoint.Stamp), newPoint.Value));
            PlotData.InvalidatePlot(true);
        }

        public PlotModel PlotData { get; set; }

        public void Unsubscribe()
        {
            if (_cache != null)
            _cache.OnValuesChanged -= OnValuesChanged;
        }
    }
}