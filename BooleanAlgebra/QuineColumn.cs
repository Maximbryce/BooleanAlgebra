using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;



namespace BooleanAlgebra
{

    public class QuineColumn
    {
        List<List<Implicant>> implicants { get; }
        int stage;
        int implicantLength { get; }

        public QuineColumn(int implicantLength, List<List<Implicant>> inputList)
        {
            this.implicants = inputList;
            this.stage = findstage(inputList);
            this.implicantLength = implicantLength;
        }

        public QuineColumn(int implicantLength, List<Implicant> inputList)
        {
            this.implicants = new List<List<Implicant>>();
            for (int i = 0; i <= implicantLength; i++)
            {
                implicants.Add(new List<Implicant>());
                for (int j = 0; j < inputList.Count; j++)
                {
                    //Console.WriteLine($"{this.implicants[j]} compared to {i}");
                    if (inputList[j].numOnes == i)
                    {
                        //Console.WriteLine($"Appending {implicants[j]}");
                        implicants[i].Add(inputList[j]);
                    }
                }
            }
            this.implicantLength = implicantLength;
            this.stage = findstage(this.implicants);
        }

        /**
         * Simplifies a list and removes any dublicates
         */
        public static Tuple<QuineColumn, List<Implicant>> ColumnSimplify(QuineColumn column)
        {
            List<List<Implicant>> newColumn = new List<List<Implicant>>();
            List<Implicant> unusedImplcants = buildUnusedList(column.implicants);
            for (int i = 0; i <= column.implicantLength; i++)
            {
                newColumn.Add(new List<Implicant>());
            }
            for (int i = 0; i < column.implicants.Count - 1; i++) // Loops through the super list which sorts sublists based on the number of ones
            {
                for (int j = 0; j < column.implicants[i].Count; j++) // one column looping through
                {
                    for (int m = 0; m < column.implicants[i + 1].Count; m++) // Lopp through next column looking for a match
                    {
                        if (Implicant.CanCombine(column.implicants[i][j], column.implicants[i + 1][m]))
                        {
                            //Console.WriteLine($"Trying to combine: {column[i][j].ToString()} {column[i + 1][m].ToString()}");
                            //Console.WriteLine($"These can combine: {Implicant.CanCombine(column[i][j], column[i+1][m])}");
                            Implicant newImp = Implicant.Combine(column.implicants[i][j], column.implicants[i + 1][m]);
                            //Console.WriteLine($"Num Ones: {newImp.numOnes}");
                            //Console.WriteLine($"{newImp.ToString()}");
                            newColumn[newImp.numOnes].Add(newImp);
                            if (unusedImplcants.Contains(column.implicants[i][j])) // Checks if either implucant is in the unused column, if unused removes and updates unused list
                            {
                                unusedImplcants.Remove(column.implicants[i][j]);
                            }
                            if (unusedImplcants.Contains(column.implicants[i + 1][m]))
                            {
                                unusedImplcants.Remove(column.implicants[i + 1][m]);
                            }
                        }
                    }
                }
            }

            newColumn = QuineColumn.removeDublicates(newColumn);

            QuineColumn nextColumn = new QuineColumn(column.implicantLength, newColumn);

            return Tuple.Create(nextColumn, unusedImplcants);
        }

        /**
         * builds a linear list of all of the elements in a 2d List, to be used with prime implicants and the simplif functions
         */
        private static List<Implicant> buildUnusedList(List<List<Implicant>> inputList)
        {
            List<Implicant> reList = new List<Implicant>();
            for (int i = 0; i < inputList.Count; i++)
            {
                for (int j = 0; j < inputList[i].Count; j++)
                {
                    reList.Add(inputList[i][j]);
                }
            }
            return reList;
        }

        /**
         * removes any dublicate simplified implicants from a simplified List
         */
        private static List<List<Implicant>> removeDublicates(List<List<Implicant>> inputList)
        {
            List<List<Implicant>> newList = new List<List<Implicant>>();
            for(int i = 0; i < inputList.Count; i++)
            {
                newList.Add(new List<Implicant>());
            }
            for (int i = 0; i < inputList.Count; i++)
            {
                for(int j = 0; j < inputList[i].Count; j++)
                {
                    if (!newList[i].Contains(inputList[i][j]))
                    {
                        newList[i].Add(inputList[i][j]);
                    }
                }
            }
            return newList;
        }

        /**
         * The Stage is defined as the number of null values in a Implicant
         */
        private int findstage(List<List<Implicant>> inputList)
        {
            int stage = -1;
            for (int i = 0; i < inputList.Count; i++)
            {
                for (int j = 0; j < inputList[i].Count; j++)
                {
                    if (this.stage != -1)
                    {
                        return stage;
                    }
                    else
                    {
                        stage = inputList[i][j].getStage();
                    }
                }
            }
            return stage;
        }

        public override string ToString()
        {
            string retval = "";
            for (int i = 0; i < this.implicants.Count; i++)
            {
                for (int j = 0; j < this.implicants[i].Count; j++)
                {
                    retval += this.implicants[i][j].ToString();
                    retval += "\n";
                }
                //retval += "~~~~~~~~~\n";
            }
            return retval;
        }

        /**
         * Equals only iff every implicant is in the same order and same subarea in both
         */
        public override bool Equals(object obj)
        {
            QuineColumn column = (QuineColumn)obj;

            if(column.implicantLength != this.implicantLength)
            {
                return false;
            }
            if(column.implicants.Count != this.implicants.Count)
            {
                return false;
            }
            for(int i = 0; i < implicants.Count; i++)
            {
                if(implicants[i].Count != this.implicants[i].Count)
                {
                    return false;
                }
            }
            for (int i = 0; i < this.implicants.Count; i++)
            {
                for(int j = 0; j < this.implicants[i].Count; j++)
                {
                    if (!this.implicants[i][j].Equals(column.implicants[i][j]))
                    {
                        return false;
                    }
                }
            }
            return true;
        }
    }
}
