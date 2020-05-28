using System;
using System.Collections.Generic;
using BooleanAlgebra;

namespace ConsoleInterface
{
    public class Landing
    {
        static void Main(string[] args)
        {
            Landing firstLanding = new Landing();
            firstLanding.landingPage();
        }

        protected void landingPage()
        {
            String userInput = "";
            Console.WriteLine("What would you like to do?");
            do
            {
                Console.Write("> ");
                userInput = Console.ReadLine();
                if (!checkHelp(userInput))
                {
                    interpretInput(userInput);
                }
            } while (userInput != "exit");
        }

        protected virtual void printHelp()
        {
            String print = "";

            print += "Kmap functions: 'kmap'\n";
            print += "QuineMckulskey Algorithm functions: 'quine'\n";
            print += "Boolean Expression functions: 'expression'\n";
            print += "Truth Table functions: 'table' \n";
            //print += "Kmap functions: 'kamp'\n";

            Console.Write(print);
        }

        protected bool checkHelp(String input)
        {
            if (input.Equals("help"))
            {
                printHelp();
                return true;
            }

            return false;
        }
        
        protected virtual void interpretInput(String input)
        {
            if (input.Equals("kmap"))
            {
                new KmapInterface();
            }
            else if (input.Equals("quine"))
            {
                new QuineInterface();
            }
            else if (input.Equals("expression"))
            {
                new ExpressionInterface();
            }
            else if (input.Equals("table"))
            {
                new TruthTableInterface();
            }
            else
            {
                Console.WriteLine("Invalid command, please enter a new command, type help for options");
            }

        }


        protected List<Int32> getMintermlist()
        {
            List<Int32> minterms = new List<int>();
            Console.WriteLine("Please enter in the minterms you want one at a time");
            String userInput = "";
            do
            {
                Console.Write("> ");
                userInput = Console.ReadLine();
                int number;
                bool isNmber = int.TryParse(userInput, out number);
                if (!isNmber)
                {
                    Console.WriteLine("Not a number please enter a different");
                }
                else
                {
                    minterms.Add(number);
                }

            } while (!userInput.Equals("") || userInput.Equals("cancel"));

            if (userInput.Equals("cancel"))
            {
                minterms.Clear();
                return minterms;
            }

            return minterms;
        }


        protected String getExpression()
        {
            Console.Write("Please enter an expression (simply enter an empty string to cancel): \n> ");
            String expression = Console.ReadLine();
            return expression;
        }

        protected int getNumber()
        {
            Console.Write("> ");
            String userInput = Console.ReadLine();
            int num;
            bool isNumber = int.TryParse(userInput, out num);
            while(!isNumber)
            {
                Console.WriteLine("That is not a valid number, please try again \n> ");
                userInput = Console.ReadLine();
                isNumber = int.TryParse(userInput, out num);
            }

            return num;
        }
    }
}
