using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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
using NumberConversion;
using System.Text.RegularExpressions;
using NumberConversion;

namespace Desktop
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class NumConvWindow : Page
    {
        public NumConvWindow()
        {
            InitializeComponent();
            FillEmpty();
        }

        private void FillEmpty()
        {
            if (BinOutLength.Text == "")
            {
                BinOutLength.Text = "0";
            }
            if (HexOutLength.Text == "")
            {
                HexOutLength.Text = "0";
            }
            if (Bin2CompOutLength.Text == "")
            {
                Bin2CompOutLength.Text = "0";
            }
        }

        private void SubmitButton_OnClick(object sender, RoutedEventArgs e)
        {
            BaseErrorMessage.Visibility = Visibility.Hidden; // status messages should be cleared so nothing sticks around and is confusing
            FillEmpty(); // make sure users cant mess everything up by input null values to functions
            if (!validInput(BaseSelector.Text)) // entered if invalid characters detected
            {
                BaseErrorMessage.Visibility = Visibility.Visible;
                return;
            }

            if (BaseSelector.Text == "Hexadecimal")
            {
                BinOutBox.Text =
                    NumberConvert.ConvertToBase(InputBox.Text, 16, 2, 1);
                DecOutBox.Text =
                    NumberConvert.ConvertToBase(InputBox.Text, 16, 10);
                HexOutBox.Text = "NA";
                Bin2CompOutBox.Text = "NA";
            }

            if (BaseSelector.Text == "Decimal")
            {
                BinOutBox.Text =
                    NumberConvert.ConvertToBase(InputBox.Text, 10, 2, Convert.ToInt32(BinOutLength.Text));
                HexOutBox.Text =
                    NumberConvert.ConvertToBase(InputBox.Text, 10, 10, Convert.ToInt32(HexOutLength.Text));
                DecOutBox.Text = "NA";
                Bin2CompOutBox.Text = "NA";
            }

            if (BaseSelector.Text == "Binary")
            {
                HexOutBox.Text =
                    NumberConvert.ConvertToBase(InputBox.Text, 2, 16, Convert.ToInt32(HexOutLength.Text));
                DecOutBox.Text =
                    NumberConvert.ConvertToBase(InputBox.Text, 2, 10);
                BinOutBox.Text = "NA";
                Bin2CompOutBox.Text =
                    NumberConvert.TwosComplement(InputBox.Text, 2, Convert.ToInt32(Bin2CompOutLength.Text));
            }
        }

        private bool validInput(String stringBase)
        {
            if (stringBase == "Hexadecimal")
            {
                return Regex.IsMatch(InputBox.Text, "^[0123456789ABCDEF]*$", RegexOptions.IgnoreCase);
            }
            if (stringBase == "Decimal")
            {
                return Regex.IsMatch(InputBox.Text, "^[0123456789]*$", RegexOptions.IgnoreCase);
            }
            if (stringBase == "Binary")
            {
                return Regex.IsMatch(InputBox.Text, "^[01]*$", RegexOptions.IgnoreCase);
            }
            else
            {
                return true;
            }
        }

        private void ReturnToMain(object sender, RoutedEventArgs e)
        {
            //TODO Make sure to use this method in the future otherwise memory leaks like a firehose
            NavigationService.GoBack();
        }
    }
}
