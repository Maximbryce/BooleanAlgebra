using System;
using System.Collections.Generic;
using System.Text;

namespace BooleanAlgebra
{
    
    public class Implicant : BinaryArray
    {
        private bool[] nullValue { get; }
        public int[] parents { get; }
        public int numOnes { get; }
        public Implicant(int[] input, int[] parents) : base(input)
        {
            this.parents = parents;
            nullValue = new bool[input.Length];
            for(int i = 0; i < input.Length; i++)
            {
                this.nullValue[i] = false;
            }
        }
        public Implicant(bool[] input, int[] parents) : base(input)
        {
            this.parents = parents;
            nullValue = new bool[input.Length];
            for (int i = 0; i < input.Length; i++)
            {
                this.nullValue[i] = false;
            }
        }
        public Implicant(bool[] input, int[] parents, bool[] nullValue) : base(input)
        {
            this.parents = parents;
            this.nullValue = nullValue; 
            this.numOnes = 0;
            for (int i = 0; i < Length; i++)
            {
                if (bitValue[i])
                {
                    this.numOnes += 1;
                }
            }
        }
        public Implicant(int minterm, int Length) : base(minterm, Length)
        {
            this.parents = new int[1] { minterm };
            nullValue = new bool[Length];
            for (int i = 0; i < Length; i++)
            {
                this.nullValue[i] = false;
            }
            this.numOnes = 0;
            for(int i = 0; i < Length; i++)
            {
                if (bitValue[i])
                {
                    this.numOnes += 1;
                }
            }
        }
        /*
         * This is a copy constructor, cheifly for use in the implicant chart class
         */
        public Implicant(Implicant imp) : base(imp.bitValue)
        {
            this.nullValue = imp.nullValue;
            this.parents = imp.parents;
            this.numOnes = imp.numOnes;
        }
        public Implicant(BinaryArray bin, int[] parents) : base(bin.bitValue)
        {
            nullValue = new bool[bin.Length];
            for (int i = 0; i < bin.Length; i++)
            {
                this.nullValue[i] = false;
            }
            this.parents = parents;
        }

        internal int parentAt(int index)
        {
            return parents[index];
        }
        /**
         * This static function is used in the QuineColumn function and the general Quine algorithm to simplify boolean functions
         * its general putpose is to combine the parents arrays, not in any particular order, and then find the difference and add a null alue
         */
        public static Implicant Combine(Implicant implicant1, Implicant implicant2)
        {
            bool[] newBitValue = new bool[implicant1.Length];
            int[] newParents = new int[implicant2.parents.Length + implicant1.parents.Length];
            bool[] newNullValue = new bool[implicant1.nullValue.Length];
            for(int i = 0; i < implicant1.Length; i++)
            {
                if(implicant1.bitValue[i] != implicant2.bitValue[i] || implicant1.nullValue[i] == true || implicant2.nullValue[i] == true)
                {
                    newBitValue[i] = false;
                    newNullValue[i] = true;
                }
                else
                {
                    newBitValue[i] = implicant1.bitValue[i];
                }
            }
            for (int i = 0; i < implicant1.parents.Length; i++)
            {
                newParents[i] = implicant1.parentAt(i);
            }
            for (int i = implicant1.parents.Length; i < implicant1.parents.Length + implicant2.parents.Length; i++)
            {
                newParents[i] = implicant2.parentAt(i - implicant1.parents.Length);
            }
            return new Implicant(newBitValue, newParents, newNullValue);
        }
        /**
         * Checks to see if two implicants can be conbimed and returns true if thats the case
         * this is true if there is only one diffence between the implicants from the null values and the bit array
         */
        public static bool CanCombine(Implicant imp1, Implicant imp2)
        {
            if(imp1.Length != imp1.Length)
            {
                return false;
            }
            int differenceCounter = 0;
            for(int i = 0; i < imp1.Length; i++)
            {
                if (imp1.bitValue[i] != imp2.bitValue[i] || imp1.nullValue[i] != imp2.nullValue[i])
                {
                    differenceCounter += 1;
                }
            }
            if(differenceCounter != 1)
            {
                return false;
            }
            else
            {
                return true;
            }
        }

        /**
         * returns the number of null value points in the implicant whcih is represened as the stage
         */
        public int getStage()
        {
            int retval = 0;
            for(int i = 0; i < nullValue.Length; i++)
            {
                if (this.nullValue[i])
                {
                    retval += 1;
                }
            }
            return retval;
        }
      
        public override string ToString()
        {
            string returnString = "";
            for (int i = 0; i < this.Length; i++)
            {
                if (bitValue[i] == true && this.nullValue[i] == false) 
                { 
                    returnString += "1"; 
                }
                else if (bitValue[i] == false && this.nullValue[i] == false)  
                { 
                    returnString += "0"; 
                }
                else if (nullValue[i])
                {
                    returnString += "-";
                }
            }
            returnString += " parents[";
            for(int i = 0; i < parents.Length; i++)
            {
                if(i != parents.Length - 1)
                {
                    returnString += parentAt(i) + ", ";
                }
                else
                {
                    returnString += parentAt(i) + "]";
                }
            }
            return returnString;
        }


        public string bitValueString()
        {
            String returnString = "";
            for (int i = 0; i < this.Length; i++)
            {
                if (bitValue[i] == true && this.nullValue[i] == false)
                {
                    returnString += "1";
                }
                else if (bitValue[i] == false && this.nullValue[i] == false)
                {
                    returnString += "0";
                }
                else if (nullValue[i])
                {
                    returnString += "-";
                }
            }
            return returnString;
        }

        /**
         * A value is the same if the bitarray values and the null values are the same 
         * parents isnt included because the equals function in the remove duplicates function in QUINE column is likely sensative to order
         */
        public override bool Equals(object obj)
        {
            Implicant implicant = (Implicant)obj;
            return base.Equals(obj) && System.Linq.Enumerable.SequenceEqual(this.nullValue, implicant.nullValue); //&& System.Linq.Enumerable.SequenceEqual(implicant.parents, this.parents);
        }


        public String ToExpression()
        {
            String expression = "";

            for(int i = 0; i < this.bitValue.Length; i ++)
            {
                if(this.nullValue[i] == false)
                {
                    expression += (char)('A' + i);
                    if (this.bitValue[i] == false)
                    {
                        expression += "'";
                    }
                }
            }
            return expression;
        }

        public int numVariables()
        {
            int count = 0;
            for(int i =0; i < this.nullValue.Length; i++)
            {
                //If the nullValue is false at this point than a variable exists there so add it
                if (!this.nullValue[i])
                {
                    count += 1;
                }
            }

            return count;
        }


        public static List<Implicant> RemoveDuplicatesInList(List<Implicant> inputList)
        {
            List<Implicant> newList = new List<Implicant>();
            for (int j = 0; j < inputList.Count; j++)
            {
                if (!newList.Contains(inputList[j]))
                {
                    newList.Add(inputList[j]);
                }
            }

            return newList;
        }

        public static String ConvertToExpression(List<Implicant> implicants)
        {
            StringBuilder expression = new StringBuilder();
            for (int i = 0; i < implicants.Count; i++)
            {
                expression.Append(implicants[i].ToExpression());
                if(i != implicants.Count -1)
                {
                    expression.Append("+");
                }
            }

            return expression.ToString();

        }
    }
}
