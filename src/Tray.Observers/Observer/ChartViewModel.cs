using System.Collections.Generic;
using OxyPlot;

namespace Tray.Observers
{
    public class ChartViewModel
    {
        public string Title { get; }

        public IList<DataPoint> Points { get; }

        public ChartViewModel()
        {
            Title = "Example 2";
            Points = new List<DataPoint>
            {
                new DataPoint(0, 4),
                new DataPoint(10, 13),
                new DataPoint(20, 15),
                new DataPoint(30, 16),
                new DataPoint(40, 12),
                new DataPoint(50, 12)
            };
        }
    }
}