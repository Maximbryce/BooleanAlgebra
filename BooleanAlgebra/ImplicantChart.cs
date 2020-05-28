using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Reflection.Metadata.Ecma335;

namespace BooleanAlgebra
{
    public class ImplicantChart
    {
        private List<Implicant> primeImplicants;
        private List<Implicant> essentialImplicants;
        private List<Int32> minterms; // previously named implicant
        private List<List<Boolean>> primeImplicantMapping;

        public ImplicantChart(List<Implicant> primeImplicants)
        {
            this.primeImplicants = primeImplicants;
            essentialImplicants = new List<Implicant>();
            createMintermList();
        }

        /**
         * Creates a list of all of the implicant minterms that need to be covered
         */
        private void createMintermList()
        {
            minterms = new List<Int32>();
            List<Int32> tempImp = new List<Int32>();
            for(int i = 0; i < primeImplicants.Count; i++)
            {
                tempImp.AddRange(primeImplicants[i].parents);
            }

            for(int i = 0; i < tempImp.Count; i++)
            {
                if (!minterms.Contains(tempImp[i]))
                {
                    this.minterms.Add(tempImp[i]);
                }
            }
            this.minterms.Sort();
        }

        /**
         * creates a maping of the which minterms cover which minterm values. 
         * this is done with implicant chart[i][j] where changing i changes the implicant that is in focus, and j is minterms of that implicant
         * a False at a j index indicates that the minterm is not contained with that specific implicant, true means is is.
         */
        private List<List<Boolean>> MapImplicantsToMinterm(List<Implicant> implicants, List<Int32> minterms)
        {
            List<List<Boolean>> implicantMapping = new List<List<Boolean>>();
            for (int i = 0; i < this.minterms.Count; i++)
            {
                implicantMapping.Add(new List<Boolean>());
                for (int j = 0; j < primeImplicants.Count; j++)
                {
                    if (this.primeImplicants[j].parents.Contains(minterms[i]))
                    {
                        implicantMapping[i].Add(true);
                    }
                    else
                    {
                        implicantMapping[i].Add(false);
                    }
                }
            }

            return implicantMapping;
        }

        /**
         * Looks through to find implicant minterm values that are only covered by one specific Implicant
         * The returns a list of Prime minterms that could or could not be essential minterms, need to find through patrick method.
         * TODO Implement a Clonable interface for better practice
         */
        private Tuple<List<Implicant>, List<Int32>> initialFind(List<Implicant> primeImplicants, List<Int32> minterms)
        {
            List<Implicant> maybeEssential = new List<Implicant>();
            List<Int32> mintermsLeft = new List<int>();

            List<List<Boolean>> implicantMapping = MapImplicantsToMinterm(primeImplicants, minterms);
            //this.primeImplicantMapping = MapImplicantsToMinterm(primeImplicants, minterms);

            for (int i = 0; i < this.primeImplicants.Count; i++)
            {
                maybeEssential.Add(new Implicant(this.primeImplicants[i]));
            }
            for(int i = 0; i < this.minterms.Count; i++) // For this and the one above consider implementing a Clonable interface in the future
            {
                mintermsLeft.Add(this.minterms[i]);
            }
            for(int i = 0; i < implicantMapping.Count; i++)
            {
                int numMinterms = 0;
                int MintermIndex = 0;
                for(int j = 0; j < implicantMapping[i].Count; j++)
                {
                    if (implicantMapping[i][j])
                    {
                        numMinterms += 1;
                        MintermIndex = j;
                    }
                }
                //This case below represents when a minterm has only one Implicant it is mapped to. If this is the case
                //That implicant is automatically essential
                if(numMinterms == 1) // Less than 1 shouldnt be possible that would mean this minterm is covered by no implicants
                {
                    this.essentialImplicants.Add(new Implicant(this.primeImplicants[MintermIndex]));
                    maybeEssential.Remove(this.primeImplicants[MintermIndex]);
                    foreach (var minterm in this.primeImplicants[MintermIndex].parents)
                    {
                        mintermsLeft.Remove(minterm);
                    }
                }
            }

            return new Tuple<List<Implicant>, List<int>>(maybeEssential, mintermsLeft);
        }

        //TODO add a method of finding the rest of the essential prime minterms, probobly best used with patriks method
        /**
         * Finds all of the essential prime minterms from a list of prime implicant
         */
        public List<Implicant> findEssential()
        {
            Tuple<List<Implicant>, List<int>> maybeEssential = this.initialFind(this.primeImplicants, this.minterms);
            //If there is no pottential essential implicants, no need to do patricks method
            if(maybeEssential.Item1.Count != 0)
            {
                List<Implicant> certainlyEssential = this.patricksMethod(maybeEssential);
                this.essentialImplicants.AddRange(certainlyEssential);
            }
            return this.essentialImplicants;
        }

        /*
         * This uses patricks method or a rough version of it. It is assumed to be demonstoubly slow, and improvement is sure to exist
         */
        private List<Implicant> patricksMethod(Tuple<List<Implicant>, List<int>> maybeEssential)
        {
            String patricksStringExpression = "";

            //Loops over each minterm
            for(int i = 0; i < maybeEssential.Item2.Count; i++)
            {
                // A Sum of Product String
                String SOP = "(";
                //Loops over each Implicant
                for (int j = 0; j < maybeEssential.Item1.Count; j++)
                {
                    //Checks if the specified implicant contains a reference to the targer minterm
                    if (maybeEssential.Item1[j].parents.Contains(maybeEssential.Item2[i]))
                    {
                        //Simply the First implicnt is labeled A, Second B and so on
                        if (SOP.Length != 1)
                        {
                            SOP += "+";
                        }
                        SOP += (char)(j + 'A');
                    }
                }
                patricksStringExpression += SOP + ")";
                if(i != maybeEssential.Item2.Count - 1)
                {
                    patricksStringExpression += "*";
                }
            }

            //Just a quick check to see if the expression doesnt exist. This is the case where there exists no possibly essential implicants
            if (patricksStringExpression.Equals(""))
            {
                return new List<Implicant>();
            }

            //Creates a new BooleanExpression to represent the expression
            BooleanExpression patricksExpression = new BooleanExpression(patricksStringExpression);
            TruthTable patricksTable = new TruthTable(patricksExpression);
            //Console.WriteLine(patricksTable.ToString());

            List<List<Int32>> InputWithLowestImplicantCount = new List<List<int>>();
            for(int i = 0; i < patricksTable.inputValues.Count; i++)
            {
                //Only enter if the table output is true for this input combo (input i coresponds directly to output i)
                if (patricksTable.outputValues[i] == 1)
                {
                    //if master list is empty add first elemtn into it, garunteed to have less ones than null
                    if (!InputWithLowestImplicantCount.Any())
                    {
                        InputWithLowestImplicantCount.Add(patricksTable.inputValues[i]);
                    }
                    //if list has elements
                    else
                    {
                        //Checks idfthe current input has a lower number of ones, and thus implicants than the current lowest
                        if (this.numOnes(patricksTable.inputValues[i]) < this.numOnes(InputWithLowestImplicantCount[0]))
                        {
                            //Clear and add new lowest to list
                            InputWithLowestImplicantCount.Clear();
                            InputWithLowestImplicantCount.Add(patricksTable.inputValues[i]);

                        }
                    }
                }
            }

            //At this point you have a list with the input combinations with the fewest number of implicants

            // Now you need to convert all of those List of ints into Implicant representations again
            List<List<Implicant>> bestCombinations = new List<List<Implicant>>();
            for (int i = 0; i < InputWithLowestImplicantCount.Count; i++)
            {
                bestCombinations.Add(new List<Implicant>());
                //Each list is in the format 001 or 100 coresponding to some number of variables, in this example 3. The Most Significant bit is A, then B
                //And so on. A represents the first Implicant in the maybe essential Item 1 of The input tuple. So find that minterm by below
                for(int j = 0; j < InputWithLowestImplicantCount[i].Count; j++)
                {
                    if (InputWithLowestImplicantCount[i][j] == 1)
                    {
                        bestCombinations[i].Add(maybeEssential.Item1[j]);
                    }
                }
            }

            //If there is more than one best combination than the Truly best one will be the one with the lowest number of total variables
            int LowestNumVariables = 0;
            int LowestNumVariablesIndex = 0;

            //Case where youve found the best combo, so return it
            if (bestCombinations.Count == 1)
            {
                return bestCombinations[0];
            }
            else
            {
                if (bestCombinations.Count != 1)
                {
                    for (int i = 0; i < bestCombinations.Count; i++)
                    {
                        int varCount = 0;
                        for (int j = 0; j < bestCombinations[i].Count; j++)
                        {
                            varCount += bestCombinations[i][j].numVariables();
                        }
                        if (varCount < LowestNumVariables)
                        {
                            LowestNumVariablesIndex = i;
                        }
                    }
                }

                return bestCombinations[LowestNumVariablesIndex];
            }
        }


        private int numOnes(List<int> list)
        {
            int numOnes = 0;
            foreach (var num in list)
            {
                if (num != 0)
                {
                    numOnes += 1;
                }
            }

            return numOnes;
        }

        /**
         * Helper function for creating buffers and white space in the toString funciton
         * The preface is the String that must be inluded in the buffer and the function then returns a string of the input length containing that preface
         * at the begining
         */
        private String createBuffer(String preface, int length)
        {
            String buffer = "";
            buffer += preface;
            while(buffer.Length < length)
            {
                buffer += " ";
            }
            return buffer;
        }

        /**
         * prints the chart coresponding to this created implicant chart,
         */
        public override String ToString()
        {
            this.primeImplicantMapping = this.MapImplicantsToMinterm(this.primeImplicants, this.minterms);
            String chart = "";
            int bufferLength = primeImplicants[0].bitValue.Length; // This is the length of the implicant in binary values
            //int depth = implicantChart[0].Count

            chart += createBuffer("", (bufferLength-3)*2 + 1) + "|"; // uper corner, empty
            for (int i = 0; i < this.minterms.Count; i++)
            {
                chart += minterms[i] + " ";
            }
            chart += "\n";
            for (int i = 0; i < this.primeImplicantMapping[0].Count; i++) // column number
            {
                chart += createBuffer(this.primeImplicants[i].bitValueString(), bufferLength) + "|";
                for (int j = 0; j < this.primeImplicantMapping.Count; j++) // row number
                {
                    if (this.primeImplicantMapping[j][i])
                    {
                        chart += "X";
                    }
                    else
                    {
                        chart += " ";
                    }
                    chart += createBuffer("", $"{minterms[j]}".Length); // hacky way of alligning the graph and finding the length of the digits in a number
                }
                chart += "\n";
            }
            return chart;
        }
    }
}
