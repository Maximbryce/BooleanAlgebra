using System;
using BooleanAlgebra;
using System.Collections.Generic;
using System.Text;

namespace ConsoleInterface
{
    class ExpressionInterface:Landing
    {
        private BooleanExpression currentObject;

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
                        this.currentObject = new BooleanExpression(expression);
                    }
                    break;
                case "print":
                    if (this.hasCurrentObject()) {Console.WriteLine(currentObject.ToString());}
                    break;
                case "minterms":
                    if (this.hasCurrentObject()) {Console.WriteLine(currentObject.getMinterms());}
                    break;
                case "table":
                    if (this.hasCurrentObject()) {Console.WriteLine(currentObject.getTruthTable());}
                    break;
                case "equal":
                    if (this.currentObject == null)
                    {
                        Console.WriteLine("There is no current object to compare to, please create one first");
                    }
                    else
                    {
                        String newExpression = getExpression();
                        if (!newExpression.Equals(""))
                        {
                            BooleanExpression tempCompare = new BooleanExpression(newExpression);
                            Boolean equality = this.currentObject.Equals(tempCompare);
                            if (equality)
                            {
                                Console.WriteLine("The two expressions are equal");
                            }
                            else
                            {
                                Console.WriteLine("The two expressions are not equal");
                            }
                        }
                    }
                    break;
                case "exit":
                    break;
                default:
                    Console.WriteLine("Invalid command, please enter a new command, type 'help' for options");
                    break;
            }
        }
        protected virtual bool hasCurrentObject()
        {
            if (currentObject == null)
            {
                Console.WriteLine("There is no current object, create one first");
                return false;
            }
            return true;
        }
    }
}
