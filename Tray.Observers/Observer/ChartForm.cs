using System;
using System.Windows.Forms;
using OxyPlot;
using OxyPlot.Series;

namespace Tray.Observers
{
    public partial class ChartForm : Form
    {
        public ChartForm()
        {
            InitializeComponent();
            var myModel = new PlotModel { Title = "Example 1" };
            myModel.Series.Add(new FunctionSeries(Math.Cos, 0, 10, 0.1, "cos(x)"));
            
            this.plot1.Model = myModel;
        }
    }
}
