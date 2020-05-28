using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;


namespace BooleanAlgebra
{
    public class Quine
    {
        private List<Implicant> implicants;
        private List<QuineColumn> columns;
        private List<Implicant> primeImplicants;
        private List<Implicant> essentialImplicants;
        private ImplicantChart primeImplicantMapping;
        private int[] minterms;
        private Boolean foundPrimes;
        private Boolean fullySimplified;
        private int implicantLength;
        private int currentColumnNum;


        public Quine(int[] minterms, int impLength)
        {
            this.implicants = new List<Implicant>();
            this.primeImplicants = new List<Implicant>();
            this.essentialImplicants = new List<Implicant>();
            this.columns = new List<QuineColumn>();
            this.foundPrimes = false;
            this.implicantLength = impLength;
            this.minterms = minterms;

            for (int i = 0; i < minterms.Length; i++)
            {
                this.implicants.Add(new Implicant(minterms[i], this.implicantLength));
            }
            
            this.columns.Add(new QuineColumn(this.implicantLength, this.implicants));
        }
        
        // Other constructors just to allow flexability
        public Quine(List<int> minterms, int impLength) : this(minterms.ToArray(), impLength) { }
        public Quine(List<int> minterms) : this(minterms.ToArray(), (int)Math.Ceiling(Math.Log2(minterms.Max()))) { }
        public Quine(int[] minterms) : this(minterms, (int)Math.Ceiling(Math.Log2(minterms.Max()))) { }

        /**
            * Simplifies the function first by finding the prime minterms and them the essential prime imlicants
            */
        public List<Implicant> simplify()
        {
            this.findPrimeImplicants();
            //System.Console.WriteLine(this.primeImplicantString());
            //this.getImplicantChartString();
            this.primeImplicantMapping = new ImplicantChart(this.primeImplicants);
            this.essentialImplicants.AddRange(this.primeImplicantMapping.findEssential());
            this.fullySimplified = true;
            this.essentialImplicants = Implicant.RemoveDuplicatesInList(this.essentialImplicants);
            return this.essentialImplicants;
        }

        /**
         * Finds the prime Implicants by repeatedly simplify the resulting columns
         */
        private void findPrimeImplicants()
        {
            do
            {
                //System.Console.WriteLine(this.columns[currentColumnNum].ToString());
                QuineColumn currentColumn = this.columns[this.currentColumnNum];
                Tuple<QuineColumn, List<Implicant>> tempTuple = QuineColumn.ColumnSimplify(currentColumn);
                //this.primeImplicants.AddRange(tempTuple.Item2);
                this.primeImplicants.AddRange(tempTuple.Item2);
                this.columns.Add(tempTuple.Item1);
                this.currentColumnNum += 1;
            } while (!this.columns[currentColumnNum].Equals(columns[currentColumnNum - 1])); // continue as long as simplification still occurs
            this.foundPrimes = true;
        }
        
        /**
         * Prints the chart for the prime minterms, and runs the find primes function if necessary
         */
        public String getImplicantChartString()
        {
            if (!this.foundPrimes)
            {
                this.findPrimeImplicants();
            }

            if (this.primeImplicantMapping == null)
            {
                this.primeImplicantMapping = new ImplicantChart(this.primeImplicants);
                this.primeImplicantMapping.findEssential();
            }

            return this.primeImplicantMapping.ToString();
        }


        /**
         * Creates a String for printing the prime minterms in the algorithm
         */
        private String primeImplicantString()
        {
            String implicantString = "Prime minterms: \n";
            for(int i = 0; i < this. primeImplicants.Count; i++)
            {
                implicantString += this.primeImplicants[i].ToString();
                implicantString += "\n";
            }
            return implicantString;
        }

        public String mintermString()
        {
            StringBuilder retString = new StringBuilder();
            retString.Append("(");
            for (int i = 0; i < minterms.Length; i++)
            {
                retString.Append(this.minterms[i]);
                if (i != minterms.Length - 1)
                {
                    retString.Append(", ");
                }
            }

            retString.Append(")");
            return retString.ToString();
        }

        public String ExpressionString()
        {
            if (!this.fullySimplified)
            {
                this.simplify();
            }

            return Implicant.ConvertToExpression(this.essentialImplicants);
        }
        
        public String columnString(int i)
        {
            if(i > this.columns.Count - 1)
            {
                return "Invalid index";
            }
            else
            {
                return this.columns[i].ToString();
            }
        }

        // builds the String that represents all of the stages of this algorithm
        public List<String> stepThoughStringList()
        {
            //If the algorith mhas full run, do so
            if (!this.fullySimplified)
            {
                this.simplify();
            }

            List<String> retString = new List<String>();
            
            StringBuilder currentStringBuilder = new StringBuilder();
            retString.Add("The implicants to simplify are: \n");

            for (int i = 0; i < this.implicants.Count; i++)
            {
                currentStringBuilder.Append(this.implicants[i].ToString());
                if (i != this.implicants.Count - 1)
                {
                    currentStringBuilder.Append(", ");
                }
            }
            retString.Add(currentStringBuilder.ToString() + "\n \n");

            for(int i = 0; i < this.columns.Count; i++)
            {
                retString.Add(this.columns[i].ToString() + "\n");
            }

            currentStringBuilder.Clear();
            currentStringBuilder.Append("The prime implicants are: \n \n");

            for(int i = 0; i < this.primeImplicants.Count; i++)
            {
                currentStringBuilder.Append(this.primeImplicants[i].ToString()) ;
                if (i != this.primeImplicants.Count - 1)
                {
                    currentStringBuilder.Append(", ");
                }
            }

            retString.Add(currentStringBuilder.ToString() + "\n");

            currentStringBuilder.Clear();

            currentStringBuilder.Append("The essential implicant chart is: \n \n");
            currentStringBuilder.Append(this.getImplicantChartString());
            retString.Add(currentStringBuilder.ToString());

            currentStringBuilder.Clear();
            currentStringBuilder.Append("The essential prime implicants are: \n \n");

            for (int i = 0; i < this.essentialImplicants.Count; i++)
            {
                currentStringBuilder.Append(this.essentialImplicants[i].ToString());
                if (i != this.essentialImplicants.Count - 1)
                {
                    currentStringBuilder.Append(", ");
                }
            }

            retString.Add(currentStringBuilder.ToString());
            return retString;
        }

        public void StepThrough()
        {
            List<String> stepThroughString = this.stepThoughStringList();

            for(int i = 0; i < stepThroughString.Count; i++)
            {

                Console.Write(stepThroughString[i]);
                Console.ReadKey();
            }
        }

        public override string ToString()
        {
            StringBuilder retString = new StringBuilder();
            List<String> stepThroughString = this.stepThoughStringList();

            for (int i = 0; i < stepThroughString.Count; i++)
            {
                retString.Append(stepThroughString[i]);
            }

            return retString.ToString();
        }
    }
}
