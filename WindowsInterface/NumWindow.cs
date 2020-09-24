using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net.NetworkInformation;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;
using NumberConversion;


namespace WindowsInterface
{
    public partial class NumWindow : Form
    {
        public NumWindow()
        {
            InitializeComponent();
            setDefaults();
        }

        private void calculateResults_Click(object sender, EventArgs e)
        {
            statusMessages.Clear(); // status messages should be cleared so nothing sticks around and is confusing
            
            if (!validInput(inputBaseDropDown.Text)) // entered if invalid charectors detected
            {
                statusMessages.Text = "Invalid characters for the selected base!";
                return;
            }

            if (inputBaseDropDown.Text == "Hexadecimal")
            {
                binValOutput.Text =
                    NumberConvert.ConvertToBase(InputBox.Text, 16, 2, 1);
                decValOutput.Text = 
                    NumberConvert.ConvertToBase(InputBox.Text, 16, 10);
                hexValOutput.Text = "NA";
                binTwoComplOutput.Text = "NA";
            }

            if (inputBaseDropDown.Text == "Decimal")
            {
                binValOutput.Text =
                    NumberConvert.ConvertToBase(InputBox.Text, 10, 2, Convert.ToInt32(binForceLength.Text));
                hexValOutput.Text =
                    NumberConvert.ConvertToBase(InputBox.Text, 10, 10, Convert.ToInt32(hexForceLength.Text));
                decValOutput.Text = "NA";
                binTwoComplOutput.Text = "NA";
            }

            if (inputBaseDropDown.Text == "Binary")
            {
                hexValOutput.Text =
                    NumberConvert.ConvertToBase(InputBox.Text, 2, 16, Convert.ToInt32(hexForceLength.Text));
                decValOutput.Text =
                    NumberConvert.ConvertToBase(InputBox.Text, 2, 10);
                binValOutput.Text = "NA";
                binTwoComplOutput.Text =
                    NumberConvert.TwosComplement(InputBox.Text, 2, Convert.ToInt32(binTwoComplForceLength.Text));
            }
        }

        private void inputBaseDropDown_Changed(object sender, EventArgs e)
        {
            inputLabel.Text = $"{inputBaseDropDown.Text} input";
        }

        private void setDefaults()
        {
            // just to prevent dumb users from inputing non numbers into functions
            hexForceLength.Text = "0";
            binForceLength.Text = "0";
            binTwoComplForceLength.Text = "0";
            inputBaseDropDown.SelectedIndex = 0; // set binary conversion to default
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

    }
}
