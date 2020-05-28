using System;
using System.Collections;

namespace BooleanAlgebra
{
    public class BinaryArray
    {
        public bool[] bitValue { get;}
        public int Length { get; }

        public BinaryArray(int[] input)
        {
            bitValue = new bool[input.Length];
            for(int i = 0; i < input.Length; i++)
            {
                bitValue[i] = input[i] == 1 ? true : false;
            }
            this.Length = input.Length;
        }
        public BinaryArray(char[] input)
        {
            bitValue = new bool[input.Length];
            for(int i = 0; i < input.Length; i++)
            {
                bitValue[i] = input[i] == '1' ? true : false;
            }
            this.Length = input.Length;
        }
        public BinaryArray(bool[] input)
        {
            bitValue = new bool[input.Length];
            for (int i = 0; i < input.Length; i++)
            {
                bitValue[i] = input[i] ? true : false;
            }
            this.Length = input.Length;
        }
        public BinaryArray(int decimalNumber, int Length)
        {
            bitValue = new bool[Length];
            for(int i = 0; i < Length; i++)
            {
                bitValue[i] = false;
            }
            BitArray bits = new BitArray(new int[] { decimalNumber });
            bool[] b = new bool[bits.Count];
            bits.CopyTo(b, 0);
            for(int i = 0; i < Length; i++)
            {
                bitValue[i] = b[i];
            }
            Array.Reverse(bitValue);
            this.Length = Length;
        }
        public override String ToString()
        {
            string ret = "";
            for(int i = 0; i < this.Length; i++)
            {
                if(bitValue[i] == true) { ret += "1"; }
                else { ret += "0"; }
            }
            return ret;
        }
        public override bool Equals(object obj)
        {
            if(obj == null)
            {
                return false;
            }
            else if(this == obj)
            {
                return true;
            }
            else if (!this.GetType().Equals(obj.GetType()))
            {
                return false;
            }
            else
            {
                BinaryArray bitArray = (BinaryArray)obj;
                return System.Linq.Enumerable.SequenceEqual(this.bitValue, bitArray.bitValue);
            }
        }
    }
}
