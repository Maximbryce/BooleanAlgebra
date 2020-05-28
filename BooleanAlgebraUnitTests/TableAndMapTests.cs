using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using BooleanAlgebra;

namespace BooleanAlgebraUnitTests
{
    [TestClass]
    public class TableAndMapTests
    {
        [TestMethod]
        public void CreateTruthTableTest()
        {
            BooleanExpression expression1 = new BooleanExpression("a'b'cd + a'bc'd");
            TruthTable table1 = new TruthTable(expression1);
            Console.WriteLine(table1.ToString());
            Console.WriteLine(table1.MintermString());
        }


        [TestMethod]
        public void CreateKMap()
        {
            //BooleanExpression expression1 = new BooleanExpression("a'*b'*c*d+a'*b*c'*e+a*b'*c'*e'+a'*b*c*d'+a*b'*c*e+a*c'*d'*e+a*c*d'*e'");
            BooleanExpression expression1 = new BooleanExpression("b'*d' +a'*d + c'*d");
            BooleanExpression expression2 = new BooleanExpression("a'b'cd + a'bc'e + ab'c'e' + a'bcd' + ab'ce + ac'd'e + acd'e'");
            KarnaughMap kmap1 = new KarnaughMap(expression2);
            Console.WriteLine(kmap1.ToString());
        }

        [TestMethod]
        public void KmapFromMinterms()
        {
            int[] mintermArray = new[] {1, 2, 3, 4, 8, 9, 0, 4, 5, 10};
            List<int> mintermList = new List<int>(mintermArray);
            KarnaughMap kmap1 = new KarnaughMap(mintermList, 4);
            Console.WriteLine(kmap1.ToString());
            Console.WriteLine(mintermList.ToString());
        }

        [TestMethod]
        public void MintermsFromKmap()
        {
            BooleanExpression expression1 = new BooleanExpression("a'b'cd + a'bc'd");
            var kmap1 = new KarnaughMap(expression1);
            Console.WriteLine(kmap1.MintermString());

        }


    }
}
