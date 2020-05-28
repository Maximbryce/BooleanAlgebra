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
            if (input.Equals("new"))
            {
                this.currentQuine = new Quine(getMintermlist());
            }
            else if (input.Equals("print"))
            {
                Console.WriteLine(currentQuine.ToString());
            }
            else if (input.Equals("step"))
            {
                Console.WriteLine("Press enter to step through");
                currentQuine.StepThrough();
            }
            else if (input.Equals("minterms"))
            {
                Console.WriteLine(currentQuine.mintermString());
            }
            else if (input.Equals("expression"))
            {
                Console.WriteLine(currentQuine.ExpressionString());
            }
            else if (input.Equals("column"))
            {
                Console.Write("What column would you like to print?: \n");
                Console.WriteLine(currentQuine.columnString(getNumber()));
            }
        }

    }
}
