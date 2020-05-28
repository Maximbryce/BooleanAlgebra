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
            print += "Check if a different expression is equivilent to the current one: 'equal'\n";

            Console.Write(print);
        }

        protected override void interpretInput(String input)
        {
            switch (input)
            {
                case "new":
                    String expression = getExpression();
                    if (!expression.Equals(""))
                    {
                        this.currentExpression = new BooleanExpression(expression);
                    }
                    break;
                case "print":
                    Console.WriteLine(currentExpression.ToString());
                    break;
                case "minterms":
                    Console.WriteLine(currentExpression.getMinterms());
                    break;
                case "table":
                    Console.WriteLine(currentExpression.getTruthTable());
                    break;
                case "equal":
                    String newExpression = getExpression();
                    if (!newExpression.Equals(""))
                    {
                        BooleanExpression tempCompare = new BooleanExpression(newExpression);
                        Boolean equality = this.currentExpression.Equals(tempCompare);
                        if (equality)
                        {
                            Console.WriteLine("The two expressions are equal");
                        }
                        else
                        {
                            Console.WriteLine("The two expressions are not equal");
                        }
                    }
                    break;
            }
        }
    }
}
