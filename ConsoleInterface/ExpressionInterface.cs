using System;
using BooleanAlgebra;
using System.Collections.Generic;
using System.Text;

namespace ConsoleInterface
{
    class ExpressionInterface:Landing
    {
        private BooleanExpression currentExpression;

        public ExpressionInterface()
        {
            Console.WriteLine("Entering Expression console");
            this.landingPage();
        }

        protected override void printHelp()
        {
            String print = "";
            print += "Create a new expression: 'new'\n";
            print += "Print the current expression: 'print'\n";
            print += "Simplify the current expression : 'equal'\n";
            print += "Get the minterms the current Expression : 'minterms'\n";
            print += "Print a truth table representing the current expression: 'table'\n";
            print += "Check if a different expression is equivilent to the current one: 'simplify'\n";

            Console.Write(print);
        }

        protected override void interpretInput(String input)
        {
            if (input.Equals("new"))
            {
                String expression = getExpression();
                if (expression.Equals("")) { }
                else
                {
                    this.currentExpression= new BooleanExpression(expression);
                }
            }
            else if (input.Equals("print"))
            {
                Console.WriteLine(currentExpression.ToString());
            }
            else if (input.Equals("minterms"))
            {
                Console.WriteLine(currentExpression.getMinterms());
            }
            else if (input.Equals("table"))
            {
                Console.WriteLine(currentExpression.getTruthTable());
            }
            else if (input.Equals("equal"))
            {
                String expression = getExpression();
                if (expression.Equals("")) { }
                else
                {
                    BooleanExpression tempCompare = new BooleanExpression(expression);
                    Boolean equality = this.currentExpression.Equals(tempCompare);
                    if (equality) { Console.WriteLine("The two expressions are equal");}
                    else { Console.WriteLine("The two expressions are not equal"); }
                }
            }
            else if (input.Equals("simplify"))
            {
                this.currentExpression = currentExpression.simplify();
            }
        }
    }
}
