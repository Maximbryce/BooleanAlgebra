using System;
using System.Collections.Generic;
using System.Net.Http.Headers;


namespace BooleanAlgebra
{
    public class TruthTable
    {
        public List<List<int>> inputValues { get; private set; }
        public List<int> outputValues { get; private set; }
        public List<int> minterms { get; private set; }
        public String expression { get; private set; }
        private List<String> variableNames;

        public TruthTable(BooleanExpression expression)
        {
            this.CreateInputList(expression.Variables.Count);
            this.minterms = new List<int>();
            this.CreateOutputList(expression);
            this.variableNames = expression.Variables;
            this.expression = expression.StringExpression;
        }


        private void CreateInputList(int inputVariableCount)
        {
            List < List < int >> invertedInputList = new List<List<int>>();
            this.inputValues = new List<List<int>>();
            for(int i = inputVariableCount - 1; i >= 0; i--)
            {
                invertedInputList.Add(new List<int>());
                for (int j = 0; j < Math.Pow(2, inputVariableCount); j++)
                {
                    if (j % Math.Pow(2, i + 1) >= Math.Pow(2, i))
                    {
                        invertedInputList[inputVariableCount - i - 1].Add(1);
                    }
                    else
                    {
                        invertedInputList[inputVariableCount - i - 1].Add(0);
                    }
                }
            }

            for(int i = 0; i < Math.Pow(2, inputVariableCount); i++)
            {
                this.inputValues.Add(new List<int>());
            }

            for (int i = 0; i < inputVariableCount; i++)
            {
                for (int j = 0; j < Math.Pow(2, inputVariableCount); j++)
                {
                    this.inputValues[j].Add(invertedInputList[i][j]);
                }
            }
        }

        private String inputString(List<int> inputSequence)
        {
            String retval = "";
            foreach (var num in inputSequence)
            {
                retval += num;
            }

            return retval;
        }


        private void CreateOutputList(BooleanExpression expression)
        {
            this.outputValues = new List<int>();
            for (int i = 0; i < this.inputValues.Count; i++)
            {
                bool result = expression.Evaluate(this.inputValues[i]);
                this.outputValues.Add(result?1:0);
                if (result)
                {
                    //Adds the numerical number coresponding to the specific input sequence
                    minterms.Add(Convert.ToInt32(inputString(inputValues[i]), 2));
                }

            }
        }


        public override String ToString()
        {
            String retVal = "";
            for (int i = 0; i < this.variableNames.Count; i++)
            {
                retVal += this.variableNames[i];
            }
            retVal += "| F\n";
            for(int i = 0; i < this.inputValues.Count; i++)
            {
                for(int j = 0; j < this.inputValues[i].Count; j++)
                {
                    retVal += this.inputValues[i][j];
                }
                retVal += ("| " + this.outputValues[i] + "\n");
            }

            return retVal;
        }

        public override bool Equals(object obj)
        {
            TruthTable table = (TruthTable) obj;
            if (table == this)
            {
                return true;
            }
            else if (table == null)
            {
                return false;
            }
            else if(this.outputValues.Count != table.outputValues.Count)
            {
                return false;
            }
            else
            {
                for (int i = 0; i < this.outputValues.Count; i++)
                {
                    if(this.outputValues[i] != table.outputValues[i])
                    {
                        return false;
                    }
                }

                return true;
            }
        }


        public String MintermString()
        {
            this.minterms.Sort();
            if (minterms == null)
            {
                return null;
            }

            String retval = "(";

            for (int i = 0; i < this.minterms.Count; i++)
            {
                retval += this.minterms[i];
                if (i != this.minterms.Count - 1)
                {
                    retval += ",";
                }
            }

            return retval += ")";
        }


    }
}
