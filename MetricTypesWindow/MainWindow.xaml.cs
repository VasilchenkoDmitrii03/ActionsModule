using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Media.TextFormatting;
using System.Windows.Navigation;
using System.Windows.Shapes;
using ActionsLib;
using WPFMetricTypeSelectorControl;
namespace MetricTypesWindow
{
   
    public partial class MainWindow : Window
    {
        ViewModel _data;
        MetricTypeList lst { get; set; }
        public MainWindow()
        {
            _data = new ViewModel();
            InitializeComponent();
            SelectedMetricTypes._totalList.Add(IntegerMetricType.createIntegerMetricType("position", "position on the playground", new int[] { 1, 2, 3, 4, 5, 6 }, new string[] { "1", "2", "3", "4", "5", "6" }));
            SelectedMetricTypes._totalList.Add(IntegerMetricType.createIntegerMetricType("quality", "quality of any action from 1-6", new int[] { 1, 2, 3, 4, 5, 6 }, new string[] { "=", "-", "/", "!", "+", "#" }));
            SelectedMetricTypes.StackPanelUpdate();
            lst = SelectedMetricTypes._selectedList;
            this.DataContext = SelectedMetricTypes;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            tmp.ItemsSource = SelectedMetricTypes._selectedList;
            tmp.UpdateLayout();
        }
    }
}