using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using BooleanAlgebra;
using Microsoft.VisualStudio.TestPlatform.ObjectModel;

namespace BooleanAlgebraUnitTests
{
    [TestClass]
    public class BooleanNodeTests
    {
        [TestMethod]
        public void FindSplitTest()
        {
            GateNode node1 = new GateNode("(A*B)");
            GateNode node2 = new GateNode("A+BC");
            GateNode node3 = new GateNode("(A+B)*C");
            GateNode node4 = new GateNode("(A+C)'+B");
            GateNode node5 = new GateNode("(A+C)'+B+C");

            Assert.AreEqual(1, node1.findSplitPoint()); // 1 is expected becuase it should trim down the parenthesis
            Assert.AreEqual(1, node2.findSplitPoint());
            Assert.AreEqual(5, node3.findSplitPoint());
            Assert.AreEqual(6, node4.findSplitPoint());
            Assert.AreEqual(6, node5.findSplitPoint());
        }

        [TestMethod]
        public void IsInvertedTest()
        {
            GateNode node1 = new GateNode("A'");
            GateNode node2 = new GateNode("(A+B)'");
            GateNode node3 = new GateNode("(A+B)");
            GateNode node4 = new GateNode("(A+C)'+B"); // should be false as the whole expression isn't inverted, just a sub piece


            Assert.IsTrue(node1.CheckIfInverted());
            Assert.IsTrue(node2.CheckIfInverted());
            Assert.IsFalse(node3.CheckIfInverted());
            Assert.IsFalse(node4.CheckIfInverted());

        }

        [TestMethod]
        public void SplitTest()
        {
            GateNode node1 = new GateNode("A'");
            GateNode node2 = new GateNode("(A+B)'");
            GateNode node3 = new GateNode("A+B");
            GateNode node4 = new GateNode("(A+C)'+B");
            GateNode node5 = new GateNode("(A+C)'+B+C");

            Tuple<String, String> expressions1 = node1.CreateChildrenExpressions();
            Assert.AreEqual("A", expressions1.Item1);
            Assert.AreEqual("SingleInv", expressions1.Item2);

            Tuple<String, String> expressions2 = node2.CreateChildrenExpressions();
            Assert.AreEqual("A+B", expressions2.Item1);
            Assert.AreEqual("MultiInv", expressions2.Item2);

            Tuple<String, String> expressions3 = node3.CreateChildrenExpressions();
            Assert.AreEqual("A", expressions3.Item1);
            Assert.AreEqual("B", expressions3.Item2);

            Tuple<String, String> expressions4 = node4.CreateChildrenExpressions();
            Assert.AreEqual("(A+C)'", expressions4.Item1);
            Assert.AreEqual("B", expressions4.Item2);

            Tuple<String, String> expressions5 = node5.CreateChildrenExpressions();
            Assert.AreEqual("(A+C)'", expressions5.Item1);
            Assert.AreEqual("B+C", expressions5.Item2);
        }

        [TestMethod]
        public void CreateChildrenTest()
        {
            //GateNode node1 = new GateNode("A'");
            //GateNode node2 = new GateNode("(A+B)'");
            //GateNode node3 = new GateNode("(A+B) + C'");
            //GateNode node4 = new GateNode("(A+C)'+B");
            GateNode node5 = new GateNode("D*(A+C*B)'+B+C");

            Dictionary<String, List<VariableNode>> variables = new Dictionary<string, List<VariableNode>>();
            //variables = node1.CreateChildren(variables);
            //BooleanNode.PrintTree(node1);
            //foreach (var variable in variables.Keys)
            //{
            //    Console.WriteLine(variable);
            //}

            variables = new Dictionary<string, List<VariableNode>>();
            variables = node5.CreateChildren(variables);
            BooleanNode.PrintTree(node5);
            foreach (var variable in variables.Keys)
            {
                Console.WriteLine(variable);
            }


        }
    }
}
