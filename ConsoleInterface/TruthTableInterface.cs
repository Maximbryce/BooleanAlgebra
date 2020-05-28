using System;
using BooleanAlgebra;
using System.Collections.Generic;
using System.Text;

namespace ConsoleInterface
{
    class TruthTableInterface : Landing
    {
        private TruthTable currentTable;

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
            if (input.Equals("new"))
            {
                String expression = getExpression();
                if (expression.Equals("")){ Console.WriteLine("Canceling");}
                else {this.currentTable = new TruthTable(new BooleanExpression(expression));}
            }
            else if (input.Equals("print"))
            {
                Console.WriteLine(this.currentTable.ToString());
            }
            else if (input.Equals("expresion"))
            {
                Console.WriteLine(this.currentTable.expression);
            }
            else if (input.Equals("minterms"))
            {
                Console.WriteLine(this.currentTable.MintermString());
            }
        }
    }
}
