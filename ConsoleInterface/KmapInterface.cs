﻿using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using System.Transactions;
using BooleanAlgebra;

namespace ConsoleInterface
{
    public class KmapInterface:Landing
    {
        private KarnaughMap currentObject;

        public KmapInterface()
        {
            Console.WriteLine("Entering Kmap console");
            this.landingPage();
        }

        protected override void printHelp()
        {
            String print = "";

            print += "Create a new Kmap: 'new'\n";
            print += "Print the current Kmap: 'print'\n";
            print += "Get the Boolean function of the current Kmap: 'expression'\n";
            print += "Get the minterms the current Kmap : 'minterms'\n";

            Console.Write(print);
        }

        protected override void interpretInput(String input)
        {
            switch (input)
            {
                case "new":
                    newKmap();
                    break;
                case "print":
                    if (this.hasCurrentObject()) {Console.WriteLine(currentObject.ToString());}
                    break;
                case "minterms":
                    if (this.hasCurrentObject()) {Console.WriteLine(currentObject.MintermString());}
                    break;
                case "expression":
                    if (this.hasCurrentObject()) {Console.WriteLine(currentObject.getSimpleExpression());}
                    break;
                case "exit":
                    break;
                default:
                    Console.WriteLine("Invalid command, please enter a new command, type 'help' for options");
                    break;
            }
        }

        private void newKmap()
        {
            Console.Write("From an expression or from minterms \n> ");
            String userInput = "";
            do
            {
                userInput = Console.ReadLine();
                if (userInput.Equals("expression"))
                {
                    String expression = getExpression();
                    if (expression.Equals(""))
                    {
                        break;
                    }
                    else
                    {
                        BooleanExpression tempExpression = new BooleanExpression(expression);
                        currentObject = new KarnaughMap(tempExpression);
                        break;
                    }
                }
                else if (userInput.Equals("minterms"))
                {
                    Console.WriteLine("Please enter how many variables are in this kmap (2-5)");
                    int numVariables = getNumber();
                    currentObject = new KarnaughMap(getMintermlist(), numVariables);
                    break;
                }
                else
                {
                    Console.WriteLine("Invalid Input, type 'expression', 'minterm' or 'cancel'");
                }
            } while (!userInput.Equals("cancel"));
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
