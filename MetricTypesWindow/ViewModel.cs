using ActionsLib;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace MetricTypesWindow
{
    public class ViewModel : INotifyPropertyChanged
    {
        public MetricTypeList SelectedMetrics { get; set; }
        public MetricTypeList AvaibleMetrics { get; set; }

        public ViewModel() 
        {
            SelectedMetrics = new MetricTypeList("Selected");
            AvaibleMetrics = new MetricTypeList("Avaible");
            addMetrics(AvaibleMetrics);
        }

        private void addMetrics(MetricTypeList tmp)
        {
            IntegerMetricType position = IntegerMetricType.createIntegerMetricType("position", "position on the playground", new int[] { 1, 2, 3, 4, 5, 6 }, new string[] { "1", "2", "3", "4", "5", "6" });
            IntegerMetricType quality = IntegerMetricType.createIntegerMetricType("quality", "quality of any action from 1-6", new int[] { 1, 2, 3, 4, 5, 6 }, new string[] { "=", "-", "/", "!", "+", "#" });
            tmp.Add(position);
            tmp.Add(quality);
        }

        public void AddSelected(MetricType metric)
        {
            this.SelectedMetrics.Add(metric);
            OnPropertyChanged("SelectedMetrics");
        }

        public void updateAllProps()
        {
            OnPropertyChanged("SelectedMetrics");
            OnPropertyChanged("AvaibleMetrics");
        }

        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName] string prop = "")
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(prop));
        }
    }
}
