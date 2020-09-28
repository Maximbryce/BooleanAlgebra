using System;


namespace NumberConversion
{
    public class NumberConvert
    {
        public static String ConvertToBase(String value, int fromBase, int toBase, int length = 0)
        {
            if (toBase == 16)
            {
                //TODO Converting from decimal to hex doesn't seem to force the correct number of digits
                return Convert.ToInt64(value, fromBase).ToString($"X{length}");
            }

            else if(toBase == 10)
            {
                return Convert.ToInt64(value, fromBase).ToString("D");
            }

            else if (toBase == 2)
            {
                String bitVal = Convert.ToString(Convert.ToInt64(value, fromBase), 2);
                int num0 = length - bitVal.Length; // the number of zeros that need to be prepended to reach desired length
                num0 = Math.Max(0, num0); // dont want to append negative values
                string prepend = new string('0', num0);
                return prepend + bitVal;
            }
            else
            {
                throw new ArgumentException("Bases must be 10, 2, or 16");
            }
        }

        public static String TwosComplement(string value, int numBase, int length = 0)
        {
            if (numBase == 2)
            {
                //twos compliment can be written as maxvalue - value
                int maxValue = (int) Math.Pow(2, value.Length);
                long twoCompDecimalNum = maxValue - Convert.ToInt64(value,2);
                String bitVal= ConvertToBase(twoCompDecimalNum.ToString(), 10, 2);
                int num0 = length - bitVal.Length; // the number of zeros that need to be prepended to reach desired length
                num0 = Math.Max(0, num0); // dont want to append negative values
                string prepend = new string('0', num0);
                return prepend + bitVal;
            }
            else
            {
                throw new ArgumentException("base must be 2");
            }

        }
    }
}
