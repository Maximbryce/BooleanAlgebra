using System;
using BooleanAlgebra;
using System.Collections.Generic;
using System.Text;

namespace ConsoleInterface
{
    class QuineInterface:Landing
    {
        private Quine currentObject;

        public QuineInterface()
        {
            Console.WriteLine("Entering Quine console");
            this.landingPage();
        }

        protected override void printHelp()
        {
            String print = "";

            print += "Create a new quineObject: 'new'\n";
            print += "Print the current quine object in full: 'print'\n";
            print += "Step though the current quine object: 'step'\n";
            print += "Get the minterms the current quine object : 'minterms'\n";
            print += "Get the simplest expression of the current quine object : 'expression'\n";
            print += "Print a specific column of the algorithm : 'column'\n";
            
            Console.Write(print);
        }

        protected override void interpretInput(String input)
        {
            switch (input)
            {
                case "new":
                    this.currentObject = new Quine(getMintermlist());
                    break;
                case "print":
                    if (this.hasCurrentObject()) {Console.WriteLine(currentObject.ToString());}
                    break;
                case "step":
                    if (this.hasCurrentObject()) {currentObject.StepThrough();}
                    break;
                case "minterms":
                    if (this.hasCurrentObject()) {Console.WriteLine(currentObject.mintermString());}
                    break;
                case "expression":
                    if (this.hasCurrentObject()) {Console.WriteLine(currentObject.ExpressionString()); }
                    break;
                case "column":
                    if (this.hasCurrentObject())
                    {
                        Console.Write("What column would you like to print?: \n");
                        Console.WriteLine(currentObject.columnString(getNumber()));
                    }
                    break;
                default:
                    Console.WriteLine("Invalid command, please enter a new command, type 'help' for options");
                    break;
            }
        }
    }
}
