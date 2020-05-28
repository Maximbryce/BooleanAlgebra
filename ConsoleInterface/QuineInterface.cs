using System;
using BooleanAlgebra;
using System.Collections.Generic;
using System.Text;

namespace ConsoleInterface
{
    class QuineInterface:Landing
    {
        private Quine currentQuine;

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
                    this.currentQuine = new Quine(getMintermlist());
                    break;
                case "print":
                    Console.WriteLine(currentQuine.ToString());
                    break;
                case "step":
                    currentQuine.StepThrough();
                    break;
                case "minterms":
                    Console.WriteLine(currentQuine.mintermString());
                    break;
                case "expression":
                    Console.WriteLine(currentQuine.ExpressionString());
                    break;
                case "column":
                    Console.Write("What column would you like to print?: \n");
                    Console.WriteLine(currentQuine.columnString(getNumber()));
                    break;
                default:
                    Console.WriteLine("Invalid command, please enter a new command, type 'help' for options");
                    break;
            }
        }
    }
}
