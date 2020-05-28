using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text;

namespace BooleanAlgebra
{
    /*
     * The base node that is inherited from from all of the other BooleanNodes
     */
    public abstract class BooleanNode
    {
        public String Expression { get; set;}
        protected String nodeType;
        public BooleanNode firstVar { get; protected set; }
        public BooleanNode secondVar { get; protected set; }
        
        /*
         * Helper function that makes it easy to call without inputing a premade dictionary when calling from a root node.
         */
        public Dictionary<String, List<VariableNode>> CreateChildren()
        {
            Dictionary<String, List<VariableNode>> Variables = new Dictionary<string, List<VariableNode>>();
            return this.CreateChildren(Variables);
        }

        public abstract Dictionary<String, List<VariableNode>> CreateChildren(Dictionary<String, List<VariableNode>> variables);

        public abstract bool Evaluate();

        /*
         * Recursively prints All children nodes of the tree
         */
        public static void PrintTree(BooleanNode tree)
        {
            if (tree != null)
            {
                Console.WriteLine(tree.ToString());
                PrintTree(tree.firstVar);
                PrintTree(tree.secondVar);
            }
        }

        /*
         * simply checks to see if a function is a single variable, for use in the create children function. Can add more conditions as needed
         * kept as a static on the parent class becuase it is used in the BooleanExpression class in the constructor.
         */
        public static bool IsSingleVar(String expression)
        {
            if (expression.Length == 1) // Only one case so far, add more later.
            {
                return true;
            }

            return false;
        }

        /*
         * prints a Single node
         */
        public override String ToString()
        {
            return this.Expression + " " + this.nodeType;
        }
    }
}
