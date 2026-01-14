using Syncfusion.UI.Xaml.Charts;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Diagnostics;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Collections;

namespace Sample1
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public double Visiblebar { get; set; }

        double  minimum, maximum;
        string[] months;
        public MainWindow()
        {
            ViewModel viewModel = new ViewModel();

            months = new string[] { "JAN", "FEB", "MAR", "APR", "MAY", "JUN", "JUL", "AUG", "SEP", "OCT", "NOV", "DEC" };
            
            DataContext = viewModel;

            InitializeComponent();

            Visiblebar = 2f;

            double value = Math.Round(Visiblebar / viewModel.Data.Count, 4);

            minimum = 0;//X-Axis initial minimum
            maximum = 11;//X-Axis initial maximum

            xAxis.Minimum = minimum;
            xAxis.Maximum = maximum;
        }
        private void zoomFactor_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            UpdateRange();
        }

        private void zoomPosition_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            UpdateRange();
        }

        // Update LabelContent property of Position of AxisLabel
        private void xAxis_LabelCreated(object sender, LabelCreatedEventArgs e)
        {
            if (e.AxisLabel.Position - (int)e.AxisLabel.Position < 0.5)
            {
                int position = (int)Math.Floor(e.AxisLabel.Position);

                if (position < months.Count() && position >= 0)
                    e.AxisLabel.LabelContent = months[position].ToString();
                else
                    e.AxisLabel.LabelContent = "";
            }
            else
            {
                int position = (int)Math.Ceiling(e.AxisLabel.Position);

                if (position < months.Count() && position >= 0)
                    e.AxisLabel.LabelContent = months[position].ToString();
                else
                    e.AxisLabel.LabelContent = "";
            }
        }

        //Calculating the Maximum and Minimum properties of Axis.
        private void UpdateRange()
        {
            if (zoomPosition != null && zoomFactor != null)
            {
                double start = minimum + zoomPosition.Value * maximum;
                double end = start + zoomFactor.Value * maximum;

                if (end > maximum)
                {
                    start = start - (end - maximum);
                    end = maximum;
                }

                if (start < minimum)
                    start = minimum;

                xAxis.Minimum = start;
                xAxis.Maximum = end;
            }
        }
    }

    public class Model
    {
        public string XValue { get; set; }
        public double YValue { get; set; }
    }

    public class ViewModel
    {
        string[] months;
        public ViewModel()
        {
            months = new string[] { "JAN", "FEB", "MAR", "APR", "MAY", "JUN", "JUL", "AUG", "SEP", "OCT", "NOV", "DEC" };

            GenerateData();
        }

        public void GenerateData()
        {
            Data = new ObservableCollection<Model>();
            Random rd = new Random();
            for (int i = 0; i < 12; i++)
            {
                Data.Add(new Model()
                {
                    XValue = months[i],
                    YValue = rd.Next(0, 50)
                });
            }
        }

        private ObservableCollection<Model> data;

        public ObservableCollection<Model> Data
        {
            get { return data; }
            set { data = value;  }
        }
    }
    
}
