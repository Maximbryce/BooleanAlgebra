using System;
using System.Collections.Generic;
using System.Linq;
using BooleanAlgebra;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace BooleanAlgebraUnitTests
{
    [TestClass]
    public class BooleanExpressionTests
    {
        [TestMethod]
        public void ExpressionCreateTests()
        {
            BooleanExpression Expression1 = new BooleanExpression("A+B");
            Expression1.PrintTree();

            Assert.IsFalse(Expression1.Evaluate(new bool[] { false, false }));
            Assert.IsTrue(Expression1.Evaluate(new bool[] { true, false }));
            Assert.IsTrue(Expression1.Evaluate(new bool[] { false, true }));
            Assert.IsTrue(Expression1.Evaluate(new bool[] { true, true }));

            BooleanExpression Expression2 = new BooleanExpression("D*(A+C*B)'+B");
            Console.WriteLine("The Variables in order");
            Console.WriteLine(String.Join(',', Expression2.Variables));

            Assert.IsTrue(Expression2.Evaluate(new bool[] { false, false, true, true }));
            Assert.IsFalse(Expression2.Evaluate(new bool[] { false, false, true, false }));

            int[] ValueArray1 = new[] {0, 0, 1, 0};
            List<int> ValueList1 = new List<int>(ValueArray1);
            Assert.IsFalse(Expression2.Evaluate(ValueList1));
        }
        [TestMethod]
        public void ExpressionTrim()
        {
            BooleanExpression expression = new BooleanExpression("A + CD'  ");
            //BooleanExpression expression1 = new BooleanExpression("a'*b'*c*d+a'*b*c'*e+a*b'*c'*e'+a'*b*c*d'+a*b'*c*e+a*c'*d'*e+a*c*d'*e'");
            BooleanExpression expression2 = new BooleanExpression("a'b'cd + a'bc'e + ab'c'e' + a'bcd' + ab'ce + ac'd'e + acd'e'");
            expression2.PrintTree();
            Assert.AreEqual("A+C*D'", expression.StringExpression);
            //Lower is bad test as it capatalizes all of the variables as it should in the Actual part
            //Assert.AreEqual("a'*b'*c*d+a'*b*c'*e+a*b'*c'*e'+a'*b*c*d'+a*b'*c*e+a*c'*d'*e+a*c*d'*e'", expression2.StringExpression);
        }

        [TestMethod]
        public void ExpressionSimplify()
        {
            BooleanExpression expression1 = new BooleanExpression("A+BC'");
            //BooleanExpression expression2 = new BooleanExpression("ac'd'e + acd'e'");
            //BooleanExpression expression3 = new BooleanExpression("a'b'cd + a'bc'e + ab'c'e' + a'bcd' + ab'ce + ac'd'e + acd'e'");

            
            Console.WriteLine(expression1.simplify());
            //Console.WriteLine(expression2.simplify());
            //Console.WriteLine(expression3.simplify());
        }


    }
}