﻿using System.Text;
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
using ActionsLib.ActionTypes;
using Microsoft.Win32;
using WPFMetricTypeSelectorControl;
namespace MetricTypesWindow
{
   
    public partial class MainWindow : Window
    {
        ActionsMetricTypes _data;
        public MainWindow()
        {
            InitializeComponent();
            _data = new ActionsMetricTypes("tmp");
            this.ActionTypeCombobox.ItemsSource = _data.Keys;
            this.AvaibleMetricsSelector.OnCheckBoxUpdated += CheckBoxUpdateEventHadler;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
        }

        private void ListNew_Click(object sender, RoutedEventArgs e)
        {

        }
        private void ListOpen_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                OpenFileDialog ofd = new OpenFileDialog();
                ofd.Filter = @"Metric type list|*.metl";
                if (ofd.ShowDialog() == true)
                {
                    MetricTypeList tmp = MetricTypeList.Load(ofd.FileName);
                    this.AvaibleMetricsSelector.updateTotalList( tmp);
                    this.ListName.Content = tmp.Name;
                }
            }
            catch (Exception exc)
            {
                MessageBox.Show(exc.Message);
            }
        }
        private void CheckBoxUpdateEventHadler()
        {
            CurrentSelectedValues.ItemsSource = this.AvaibleMetricsSelector._selectedList;
            CurrentSelectedValues.ItemsSource = null;
            CurrentSelectedValues.ItemsSource = this.AvaibleMetricsSelector._selectedList;
            updateData();
        }
        private void updateData()
        {
            _data.updateList((VolleyActionType)ActionTypeCombobox.SelectedItem, AvaibleMetricsSelector._selectedList);
        }

        private void ActionTypeCombobox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            AvaibleMetricsSelector.updateSelected(_data[(VolleyActionType)ActionTypeCombobox.SelectedItem]);
        }
    }
}