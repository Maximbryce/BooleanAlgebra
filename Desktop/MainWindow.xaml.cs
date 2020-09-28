using System;
using System.Collections.Generic;
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

namespace Desktop
{
    /// <summary>
    /// Interaction logic for MainPage.xaml
    /// </summary>
    public partial class MainWindow : Page
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void Simplification_OnClick(object sender, RoutedEventArgs e)
        {
            BoolExpPage booleanPage = new BoolExpPage();
            NavigationService.Navigate(booleanPage);
            
        }

        private void Conversion_OnClick(object sender, RoutedEventArgs e)
        {
            NumConvWindow numConvWindow = new NumConvWindow();
            NavigationService.Navigate(numConvWindow);
        }
    }
}
