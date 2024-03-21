using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using ActionsLib;

namespace WPFMetricTypeSelectorControl
{
    /// <summary>
    /// Interaction logic for UserControl1.xaml
    /// </summary>
    public partial class MetricTypeSelector : UserControl, INotifyPropertyChanged
    {
        public MetricTypeList _totalList { get; set; }
        public MetricTypeList _displayedList { get; set; }
        public MetricTypeList _selectedList { get; set; }
        public List<CheckBox> _checkBoxes { get; set; }
        public MetricTypeSelector()
        {
            _checkBoxes = new List<CheckBox>();
            _totalList = new MetricTypeList("total");
            _displayedList = _totalList;
            _selectedList = new MetricTypeList("seleted");
            this.DataContext = this;
            InitializeComponent();
        }

        public void StackPanelUpdate()
        {
            _checkBoxes.Clear();
            Items.Children.Clear();
            int ind = 0;
            foreach (MetricType tmp in _displayedList)
            {
                CheckBox chbox = new CheckBox() { Content = tmp.ToString(), IsChecked = false };
                chbox.Checked += (o, e) => { 
                    if(((CheckBox)o).IsChecked == true)
                    {
                        _selectedList.Add(tmp);
                    }
                    else
                    {
                        _selectedList.Remove(tmp);
                    }
                    OnPropertyChanged("_selectedList");
                };
                _checkBoxes.Add(chbox);
                Items.Children.Add(chbox);

            }
        }

        private void SearchTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            string txt = ((TextBox)sender).Text;
            createDisplayList(txt);
            StackPanelUpdate();
        }
        private void createDisplayList(string txt = "")
        {
            if (txt == "") _displayedList = _totalList;
            else
            {
                _displayedList = new MetricTypeList("displayed");
                foreach(MetricType tmp in _totalList)
                {
                    if (tmp.ToString().Contains(txt))
                    {
                        _displayedList.Add(tmp);
                    }
                }
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName] string prop = "")
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(prop));
        }
    }

}
