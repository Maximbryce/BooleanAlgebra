using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


namespace BooleanAlgebra
{
    public class BooleanExpression
    {
        public String StringExpression { get; protected set; }
        public List<String> Variables { get; protected set; }
        public TruthTable truthTable { get; private set; }
        protected Dictionary<String, List<VariableNode>> VariableNodes;
        protected BooleanNode RootExpression;

        // Constructor
        public BooleanExpression(String expression)
        {
            this.StringExpression = expression;
            this.CleanUpExpression();
            this.CreateRootExpression(this.StringExpression);
            this.VariableNodes = RootExpression.CreateChildren();
            this.Variables = new List<string>();
            this.SetUpVariables();
            //this.StringExpression = RootExpression.Expression; //Now that Cleanup is done before creating the root node, not necessary
            //this.GenerateTruthTable(); // Not Strictly necessary to do at this step, but added for the Console. interfaces
        }

        //Copy Constructor
        public BooleanExpression(BooleanExpression expression) : this(expression.StringExpression) { }


        //TODO Make String builder 
        private void CleanUpExpression()
        {
            if (this.StringExpression.Equals(""))
            {
                Console.WriteLine("ERROR");
            }

            this.StringExpression = StringExpression.Trim(); // Trim by removeing extra spaces at end and beginning
            this.StringExpression = this.StringExpression.Replace(" ", ""); // removes any inline spaces 
            //Adds any let out or forgotten multiplication marks ie (A+B)(A+B) -> (A+B)*(A+B)
            for (int i = 0; i < this.StringExpression.Length; i++)
            {
                if (Char.IsLetter(StringExpression[i]))
                {
                    if (Char.IsLower(StringExpression[i]))
                    {
                        StringExpression = StringExpression.Substring(0, i) + Char.ToUpper(StringExpression[i]) +
                                           StringExpression.Substring(i + 1);
                    }

                    //Checks to make sure there are multiplication points where vairbale AND occurs
                    if (i != StringExpression.Length - 1)
                    {
                        if (Char.IsLetter(StringExpression[i + 1]))
                        {
                            StringExpression = StringExpression.Substring(0, i + 1) + "*" +
                                               StringExpression.Substring(i + 1);
                        }
                        //Make sure not out of bounds accessing
                        else if (i != StringExpression.Length - 2 && (StringExpression[i + 1] == '\'' || StringExpression[i + 1] == ')'))
                        {
                            //Where the next char is a inversion or maybe a parenthesis and the AND (*) needs to go after
                            // ie (A+B)'*
                            if (Char.IsLetter(StringExpression[i + 2]))
                            {
                                StringExpression = StringExpression.Substring(0, i + 2) + "*" +
                                                   StringExpression.Substring(i + 2);
                            }
                        }
                    }
                }
            }
        }


        /*
        * Creates the root expression which recursively creates the whole expression
        */
        private void CreateRootExpression(String expression)
        {
            if (BooleanNode.IsSingleVar(expression))
            {
                this.RootExpression = new VariableNode(expression);
            }
            else
            {
                this.RootExpression = new GateNode(expression);
            }
        }

        /*
         * Sets up the variables gained from the dictionary after creating the root expression
         */
        public void SetUpVariables()
        {
            foreach (var variable in VariableNodes.Keys)
            {
                this.Variables.Add(variable);
            }
            this.Variables.Sort();

        }

        /*
         * Method for using with a truth table class
         */
        public bool Evaluate(List<int> varValues)
        {
            List<bool> newVarValues = new List<bool>();
            for(int i = 0; i < varValues.Count; i++)
            {
                if (varValues[i] == 0)
                {
                    newVarValues.Add(false);
                }
                else
                {
                    newVarValues.Add(true);
                }
            }

            return this.Evaluate(newVarValues);
        }

        /*
         * Alternate method of input using arrays instead of lists, nothing special
         */
        public bool Evaluate(bool[] varValues)
        {
            return this.Evaluate(varValues.ToList());
        }

        /*
         * Evaluates an expression by setting each variable to the correct value and then evaluateing the root expression
         */
        public bool Evaluate(List<bool> varValues)
        {
            if(varValues.Count != this.Variables.Count)
            {
                throw null;
            }
            for(int i = 0; i < varValues.Count; i++)
            {
                this.SetVarValue(i, varValues[i]);
            }

            return this.RootExpression.Evaluate();
        }

        /*
         * Simplifies an expression and makes sure the same previous variables are used again in the new expression that is returned
         */
        public BooleanExpression simplify()
        {
            if (!this.hasTruthTable())
            {
                this.GenerateTruthTable();
            }
            Quine simplifiedExpression = new Quine(this.truthTable.minterms.ToArray(), this.Variables.Count);
            List<Implicant> essentialImplicants = simplifiedExpression.simplify();

            StringBuilder newExpression = new StringBuilder(Implicant.ConvertToExpression(essentialImplicants));

            //Console.WriteLine("Intermediate expression: " + newExpression.ToString());
            
            for(int i = 0; i < this.VariableNodes.Keys.Count; i++)
            {
                newExpression.Replace($"{(char)('A' + i)}", this.VariableNodes.Keys.ElementAt(i).ToLower());
                //Console.WriteLine("Replacing " + $"{(char)('A' + i)}" + " with " + this.VariableNodes.Keys.ElementAt(i));
            }

            return new BooleanExpression(newExpression.ToString());
        }


        //Sets a specific variable to a specific value
        private void SetVarValue(int VariableIndex, bool value)
        {
            foreach (var node in this.VariableNodes[this.Variables[VariableIndex]])
            {
                node.varValue = value;
            }

        }
        
        /*
         * Prints the Expression
         */
        public override String ToString()
        {
            return this.StringExpression;
        }

        /*
         * Uses the static method on the BooleanNode class to print all child expressions of the the
         * root node in this case
         */
        public void PrintTree()
        {
            BooleanNode.PrintTree(this.RootExpression);
        }

        //simply checks if the object has a representative truth table
        public bool hasTruthTable()
        {
            return truthTable != null;
        }

        //generates the truth table for this expression object
        private void GenerateTruthTable()
        {
            this.truthTable = new TruthTable(this);
        }

        //returns a string representing a list of all of the minterms covered by this expression
        public String getMintermString()
        {
            if (!this.hasTruthTable())
            {
                this.GenerateTruthTable();
            }

            return this.truthTable.MintermString();
        }

        //returns a list of minterms
        public List<int> getMintermList()
        {
            if (!this.hasTruthTable())
            {
                this.GenerateTruthTable();
            }
            return this.truthTable.minterms;
        }

        //returns the truth table represented by this expression
        public String getTruthTable()
        {
            if (!this.hasTruthTable())
            {
                this.GenerateTruthTable();
            }

            return this.truthTable.ToString();
        }

        /*
         * Checks if this expression is equal to another by comparing the mintersm covered by it and a comparison
         * if the two objects cover the same minters they are by definition equal, else there are not equal
         */
        public override bool Equals(object obj)
        {
            BooleanExpression expression = (BooleanExpression) obj;
            if (expression == this)
            {
                return true;
            }
            else if (expression == null)
            {
                return false;
            }
            else if (expression.StringExpression.Equals(this.StringExpression))
            {
                return true;
            }
            else if (this.simplify().ToString().Equals(expression.simplify().ToString())) // checks to see if the simplest expressions are the same
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
