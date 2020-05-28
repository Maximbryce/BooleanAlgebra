using System;
using System.Collections.Generic;
using System.Text;

namespace BooleanAlgebra
{
    /*
     * The variable node class is always a leaf. It never has any children, it inherits those children parameters but sets them to null
     * The class represents a single variable as apposed to a full gate like its bigger brother.
     */
    public class VariableNode : BooleanNode
    {
        public Boolean varValue { get; set; }

        /*
         * Constructor that initializes the node type and variable.
         */
        public VariableNode(String variable)
        {
            this.Expression = variable;
            this.nodeType = "SINGLE";
            this.firstVar = null;
            this.secondVar = null;
        }

        /*
         * Slightly different create children method that represents a base case in the recursive call. There are checks to see if the
         * variable is already present in the function, if it is, it's removed and re-added with a modified list
         */

        public override Dictionary<String, List<VariableNode>> CreateChildren(Dictionary<String, List<VariableNode>> variables)
        {
            if (variables.ContainsKey(this.Expression))
            {
                List<VariableNode> tempList = variables[this.Expression];
                tempList.Add(this);
                variables.Remove(this.Expression);
                variables.Add(this.Expression, tempList);
            }
            else
            {
                List<VariableNode> tempList = new List<VariableNode>();
                tempList.Add(this);
                variables.Add(this.Expression, tempList);
            }
            return variables;
        }

        // Simply returns the value stored in the classes variable.
        public override bool Evaluate()
        {
            return varValue;
        }
    }
}
