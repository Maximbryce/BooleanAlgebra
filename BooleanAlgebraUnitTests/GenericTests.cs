using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using BooleanAlgebra;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace BooleanAlgebraUnitTests
{
    [TestClass]
    public class GenericTests
    {
        [TestMethod]
        public void Bintest()
        {
            int[] imp1 = {1, 0, 1, 0};
            BooleanAlgebra.BinaryArray bin1 = new BooleanAlgebra.BinaryArray(imp1);
            Console.WriteLine(bin1.ToString());

            int[] imp2 = {1, 0, 1, 1};
            BooleanAlgebra.BinaryArray bin2 = new BooleanAlgebra.BinaryArray(imp2);
            Console.WriteLine(bin2.ToString());

            int[] imp3 = {1, 0, 1, 0};
            BooleanAlgebra.BinaryArray bin3 = new BooleanAlgebra.BinaryArray(imp3);
            Console.WriteLine(bin3.ToString());

            int[] imp4 = {0, 0, 1, 1};
            BooleanAlgebra.BinaryArray bin4 = new BooleanAlgebra.BinaryArray(imp4);
            Console.WriteLine(bin4.ToString());


            Assert.AreNotEqual(bin1.ToString(), bin2.ToString());
            Assert.AreEqual(bin1.ToString(), bin3.ToString());
            Assert.IsFalse(bin1.Equals(bin4));
            Assert.IsTrue(bin1.Equals(bin3));
            //System.Console.WriteLine(bin1.valueAt(5)); // should crash
        }

        [TestMethod]
        public void implicantTest()
        {
            BooleanAlgebra.Implicant imp1 = new BooleanAlgebra.Implicant(0, 5);
            BooleanAlgebra.Implicant imp2 = new BooleanAlgebra.Implicant(1, 5);
            BooleanAlgebra.Implicant imp3 = new BooleanAlgebra.Implicant(8, 5);
            BooleanAlgebra.Implicant imp4 = new BooleanAlgebra.Implicant(9, 5);
            System.Console.WriteLine($"{BooleanAlgebra.Implicant.CanCombine(imp1, imp2)}"); // true
            System.Console.WriteLine($"{BooleanAlgebra.Implicant.CanCombine(imp1, imp3)}"); // false
            BooleanAlgebra.Implicant imp1_2 = BooleanAlgebra.Implicant.Combine(imp1, imp2);
            Console.WriteLine($"{imp1_2.numOnes}");
            BooleanAlgebra.Implicant imp3_4 = BooleanAlgebra.Implicant.Combine(imp3, imp4);
            Console.WriteLine(imp1_2.ToString());
            Console.WriteLine(imp1_2.ToExpression());
            Console.WriteLine(imp3_4.ToString());
            Console.WriteLine(imp3_4.ToExpression());
            Console.WriteLine($"{BooleanAlgebra.Implicant.CanCombine(imp1_2, imp3_4)}");
            BooleanAlgebra.Implicant imp5 = BooleanAlgebra.Implicant.Combine(imp1_2, imp3_4);
            Console.WriteLine(imp5.ToString());
            Console.WriteLine(imp5.ToExpression());

        }

        [TestMethod]
        public void quineTest()
        {
            //int[] minterms1 = new int[10] {0, 1, 2, 5, 6, 7, 8, 9, 10, 14};
            int[] minterms1 = new int[] {0,1,2,5,6,7};
            BooleanAlgebra.Quine quine1 = new BooleanAlgebra.Quine(minterms1, 3);
            List<Implicant> essential = quine1.simplify();
            System.Console.WriteLine(quine1.ToString());

            Console.WriteLine("The Expression is");
            Console.WriteLine(Implicant.ConvertToExpression(essential));
        }

    }
}


