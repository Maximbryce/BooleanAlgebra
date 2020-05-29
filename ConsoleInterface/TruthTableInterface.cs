using System;
using BooleanAlgebra;
using System.Collections.Generic;
using System.Text;

namespace ConsoleInterface
{
    class TruthTableInterface : Landing
    {
        private TruthTable currentObject;

        public TruthTableInterface()
        {
            Console.WriteLine("Entering truth table console");
            this.landingPage();
        }

        protected override void printHelp()
        {
            String print = "";
            print += "Create a new truth table: 'new'\n";
            print += "Print the current truth table: 'print'\n";
            print += "Get the Boolean function of the current truth table: 'expression'\n";
            print += "Get the minterms the current truth table: 'minterms'\n";
            Console.Write(print);
        }

        protected override void interpretInput(String input)
        {
            switch(input)
            {
                case "new":
                    String expression = getExpression();
                    if (expression.Equals("")) { Console.WriteLine("Canceling:"); }
                    else { this.currentObject = new TruthTable(new BooleanExpression(expression));}
                    break;
                case "print":
                    if (this.hasCurrentObject()) {Console.WriteLine(this.currentObject.ToString());}
                    break;
                case "expression":
                    if (this.hasCurrentObject()) {Console.WriteLine(this.currentObject.expression);}
                    break;
                case "minterms":
                    if (this.hasCurrentObject()) {Console.WriteLine(this.currentObject.MintermString());}
                    break;
                case "exit":
                    break;
                default:
                    Console.WriteLine("Invalid command, please enter a new command, type 'help' for options");
                    break;
            }
        }
        protected override bool hasCurrentObject()
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
