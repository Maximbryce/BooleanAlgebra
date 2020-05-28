using System;
using System.Collections.Generic;

namespace BooleanAlgebra
{
    public class KarnaughMap
    {
        private List<String> RowLabels;
        private List<String> ColumnLabels;
        private List<List<int>> kMapInards;
        public BooleanExpression expression { get; private set; }
        private List<int> minterms;
        // Const for creating a kmpa froma particular expression object
        public KarnaughMap(BooleanExpression expression)
        {
            //Simple Copy Constructor call below
            this.expression = new BooleanExpression(expression);
            this.CreateRowsAndColumns();
            this.minterms = new List<int>();
            this.CreateInards();
        }

        // Constructor for creating a kmap object from a list of desired minterms
        public KarnaughMap(List<int> mintermList, int numVariables)
        {
            this.minterms = mintermList;
            this.CreateRowsAndColumns((int)Math.Ceiling(Convert.ToDouble(numVariables) / 2.0), numVariables);
            this.CreateInards(mintermList);
        }

        //just a defualt method for general automatic use based on the specific expression this represents;
        private void CreateRowsAndColumns()
        {
            this.CreateRowsAndColumns((int)Math.Ceiling(Convert.ToDouble(expression.Variables.Count) / 2.0), this.expression.Variables.Count);
        }

        //Creates the rows and column labels, not the actual inards of the kmap
        private void CreateRowsAndColumns(int columnVariableCount, int totalvariableCount)
        {
            // An odd number of expression needs to be a rectangle and not a box

            if (columnVariableCount == 1 && totalvariableCount - columnVariableCount == 1)
            {
                //This is the 1X1 Case for vairable arrangements
                String[] order = new string[] { "0", "1" };
                this.ColumnLabels = new List<String>(order);
                this.RowLabels = new List<string>(order);

            }
            if (columnVariableCount == 2 && totalvariableCount - columnVariableCount == 1)
            {
                //This is the 2X1 case for variable arrangements
                String[] orderOne = new string[] { "0", "1" };
                String[] orderTwo = new string[] { "00", "01", "11", "10" };
                this.ColumnLabels = new List<String>(orderTwo);
                this.RowLabels = new List<string>(orderOne);
            }
            if (columnVariableCount == 2 && totalvariableCount - columnVariableCount == 2)
            {
                //This is the 2X2 case for variable arrangments
                String[] orderTwo = new string[] { "00", "01", "11", "10" };
                this.ColumnLabels = new List<String>(orderTwo);
                this.RowLabels = new List<string>(orderTwo);
            }
            if(columnVariableCount == 3 && totalvariableCount - columnVariableCount == 2)
            {
                String[] orderOne = new string[] {"000","001","011","010","110","111","101", "100" };
                String[] orderTwo = new string[] { "00", "01", "11", "10" };
                this.ColumnLabels = new List<String>(orderOne);
                this.RowLabels = new List<string>(orderTwo);
            }


        }

        /*
         * Helper mothod for the expression driven kmap objct. This method creates the required input list for the expresison
         * object in order to be evaluated at a point
         */
        private List<int> MakeInputSequence(int Row, int Column)
        {
            String LastDigits = this.RowLabels[Row];
            String FirstDigits = this.ColumnLabels[Column];

            List<int> output = new List<int>();
            for (int i = 0; i < FirstDigits.Length; i++)
            {
                output.Add(Int32.Parse($"{FirstDigits[i]}"));
            }
            for (int i = 0; i < LastDigits.Length; i++)
            {
                output.Add(Int32.Parse($"{LastDigits[i]}"));
            }

            return output;
        }

        /*
         * Creates the inards of the kmap from a list of minterms by continously comparing the row and variable combination
         * with the desired minterms to be included (The minterms input variable)
         */

        private void CreateInards(List<int> minterms)
        {
            this.kMapInards = new List<List<int>>();
            for (int i = 0; i < this.RowLabels.Count; i++)
            {
                this.kMapInards.Add(new List<int>());
                for (int j = 0; j < this.ColumnLabels.Count; j++)
                {
                    String inputVal = ColumnLabels[j] + RowLabels[i];
                    //If the specific row and column label combination is one of the minters, add it 1 to Kmap, else add a 0
                    kMapInards[i].Add(minterms.Contains(Convert.ToInt32(inputVal, 2))?1:0);
                }
            }
        }

        /*
         * Creates the inards of the kmap directly from its referenced exression object, uses the makeInputSequece
         * Helper method above
         */
        private void CreateInards()
        {
            /*
             * Loop through Rows and then Columns, The Inards List should be in Column list of Rows style
             * i will Loop over the Rows, j over the Columns
             */
            this.kMapInards = new List<List<int>>();
            for(int i = 0; i < this.RowLabels.Count; i++)
            {
                this.kMapInards.Add(new List<int>());
                for (int j = 0; j < this.ColumnLabels.Count; j++)
                {
                    //Seperate function for no really good reason but cleanliness, could combine later
                    List<int> inputValues = this.MakeInputSequence(i, j);
                    String inputSequence = ColumnLabels[j] + RowLabels[i];

                    bool evaluationResult = this.expression.Evaluate(inputValues);
                    int value = Convert.ToInt32(inputSequence, 2);
                    if (evaluationResult) { this.minterms.Add(value);} 
                    //Simple adds a one if function evaluates to true, 0 if false
                    kMapInards[i].Add(evaluationResult?1:0);
                }
            }
        }


        /*
         * Builds a cell of a specified length including at least a particular number (0 or 1) in this case
         */
        private String BuildCell(int Length, String Contents)
        {
            String retVal = "";
            retVal += Contents;
            while (retVal.Length < Length) 
            {
                retVal += " ";
            }

            return retVal;
        }

        /*
         * generic to string prints a header and than a kmap as would be expected
         */
        public override String ToString()
        {
            String retVal = "";
            //First print out lables for the varibales and there order on the table
            for(int i = 0; i < (int)Math.Log2(this.ColumnLabels.Count); i++)
            {
                if(this.expression == null)
                {
                    retVal += (char)(i + 'A');
                }
                else
                {
                    retVal += this.expression.Variables[i];
                }
            }

            retVal = "\\" + retVal;
            /*
             * The order of these Row and column lables looks a bit confusing but its ordered the way it is so the Column
             * labels have the first letters alphabetically
             */
            String RowLabels = "";
            //Looks like a headach but really it just starts off where the first loop left off and continues onto the other vairbales
            for (int i = (int)Math.Log2(this.ColumnLabels.Count); i < (int)Math.Log2(this.RowLabels.Count) + (int)Math.Log2(this.ColumnLabels.Count); i++)
            {
                if (this.expression == null)
                {
                    RowLabels += (char)(i + 'A');
                }
                else
                {
                    RowLabels += this.expression.Variables[i];
                }
            }

            retVal = RowLabels + retVal;
            retVal += "\n";

            int indentLength = this.ColumnLabels[0].Length * 2;

            //Print all of the Row labels which is at the top of the MAP, This and next loop
            for (int i = 0; i < this.ColumnLabels[0].Length + 2; i++)
            {
                retVal += " ";
            }
                
            for (int i = 0; i < this.ColumnLabels.Count; i++)
            {
                retVal += this.BuildCell(indentLength, this.ColumnLabels[i]); // Extra space for readability
            }

            retVal += "\n";

            for (int i = 0; i < this.RowLabels.Count; i++)
            {
                for(int j = 0; j <this.ColumnLabels.Count; j++)
                {
                    if (j == 0)
                    {
                        retVal += (this.RowLabels[i] + "  ");
                    }

                    retVal += this.BuildCell(indentLength, this.kMapInards[i][j].ToString());
                }

                retVal += "\n";
            }

            return retVal;
        }

        public String MintermString()
        {
            this.minterms.Sort();
            if (minterms == null)
            {
                return null;
            }

            String retval = "(";

            for(int i = 0; i < this.minterms.Count; i++)
            {
                retval += this.minterms[i];
                if (i != this.minterms.Count - 1)
                {
                    retval += ",";
                }
            }

            return retval += ")";
        }

        //Gets the simplest Expression representing this kmap
        public String getSimpleExpression()
        {
            Quine tempQuine = new Quine(this.minterms);
            return Implicant.ConvertToExpression(tempQuine.simplify());
        }
    }
}
