using System;
using System.Collections.Generic;
using System.Text;

namespace BooleanAlgebra
{
    /*
     * The big boy class that is for any Gate operations,
     * Currently Handles inversion, AND, OR.
     */
    public class GateNode : BooleanNode
    {
        
        public GateNode(String expression)
        {
            this.Expression = expression;
            this.CleanUpExpression();
        }

        /*
         * Pre-Processing of the expression obtained from the constructor to make it valid
         */
        public void CleanUpExpression()
        {
            if (Expression[0] == '(' && Expression[Expression.Length - 1] == ')' && this.findSplitPoint() == -1) // will hopefully remove any unecessary or unhelpful parenthesis
            {
                this.Expression = this.Expression.Substring(1, Expression.Length - 2);
            }

        }

        /*
         * Simply checks to see if the whole expression represents an inverted function. Not parts, but only if the whole is inverted
         * returns a boolean representing this.
         */
        public bool CheckIfInverted()
        {
            if (this.Expression.Length == 2) // Case where it is a single variable and inverted
            {
                if (Expression[1] == '\'')
                {
                    return true;
                }
            }
            if (this.Expression[0] == '(' && this.Expression[this.Expression.Length - 2] == ')' && this.Expression[this.Expression.Length - 1] == '\'')
            {
                return true;
            }

            return false;
        }

        /**
         * Checks an expression to see if there is a point where the expression can be split, ie an AND or OR (+,*) that is present in the
         * Expression outside of any parenthesis and part of the larger expression. If one exists it will return the first point that exists.
         * If non exist this will return a -1 meaning that there
         * is no point to split. This means either there is an error, the function is inverted.
         */
        public int findSplitPoint() // Finds the Split point and type
        {
            int splitPoint = -1;
            int parenthLevel = 0;

            for (int i = 0; i < Expression.Length; i++)
            {
                if (Expression[i].Equals('+') && parenthLevel == 0)
                {
                    splitPoint = i;
                    this.nodeType = "OR";
                    return splitPoint;
                }
                else if (Expression[i].Equals('('))
                {
                    parenthLevel += 1;
                }
                else if (Expression[i].Equals(')'))
                {
                    parenthLevel -= 1;
                }
            }

            if (splitPoint == -1) // find the first AND expression
            {
                for (int i = 0; i < Expression.Length; i++)
                {
                    if (Expression[i].Equals('*') && parenthLevel == 0)
                    {
                        splitPoint = i;
                        this.nodeType = "AND";
                        return splitPoint;
                    }
                    else if (Expression[i].Equals('('))
                    {
                        parenthLevel += 1;
                    }
                    else if (Expression[i].Equals(')'))
                    {
                        parenthLevel -= 1;
                    }
                }
            }

            return splitPoint;
        }

        /*
         * Splits the expression at the first AND or OR, creating and returning two separate expressions
         */
        public Tuple<String, String> CreateChildrenExpressions()
        {
            int splitPoint = this.findSplitPoint();
            String expressionOne = "";
            String expressionTwo = "";

            if (splitPoint == -1) //It is not a AND or OR gate, Need to check if single variable or if it is inverted
            {
                if (this.CheckIfInverted())
                {
                    this.nodeType = "INV";
                    if (this.Expression[this.Expression.Length - 1] == '\'' &&
                        this.Expression[this.Expression.Length - 2] == ')') // Case where it is not a single inversion
                    {
                        expressionOne = this.Expression.Substring(1, this.Expression.Length - 3);
                        expressionTwo = "MultiInv";
                    }
                    else // single variable inversion
                    {
                        expressionOne = this.Expression.Substring(0, Expression.Length - 1);
                        expressionTwo = "SingleInv";
                    }
                }
            }

            else
            {
                expressionOne = this.Expression.Substring(0, splitPoint);
                expressionTwo = this.Expression.Substring(splitPoint + 1);
            }
            return new Tuple<string, string>(expressionOne, expressionTwo);
        }


        /*
         * The actually important function, creates children expressions, and then determines the types of nodes that need to be created as
         * children
         */
        public override Dictionary<String, List<VariableNode>> CreateChildren(Dictionary<String, List<VariableNode>> variables)
        {
            Tuple<String, String> expressions = this.CreateChildrenExpressions();
            if (expressions.Item1.Equals("") && expressions.Item2.Equals(""))
            {
                Console.WriteLine("ERROR");
            }
            if (expressions.Item2.Equals("SingleInv")) // single inverter case
            {
                this.firstVar = new VariableNode(expressions.Item1);
                this.firstVar.CreateChildren(variables);
                this.secondVar = null;
            }
            else if (expressions.Item2.Equals("MultiInv")) // single inverter case
            {
                this.firstVar = new GateNode(expressions.Item1);
                this.firstVar.CreateChildren(variables);
                this.secondVar = null;
            }
            else
            {
                if (BooleanNode.IsSingleVar(expressions.Item1))
                {
                    this.firstVar = new VariableNode(expressions.Item1);
                }
                else
                {
                    this.firstVar = new GateNode(expressions.Item1);
                }

                this.firstVar.CreateChildren(variables);

                if (BooleanNode.IsSingleVar(expressions.Item2))
                {
                    this.secondVar = new VariableNode(expressions.Item2);
                }
                else
                {
                    this.secondVar = new GateNode(expressions.Item2);
                }

                this.secondVar.CreateChildren(variables);
            }

            return variables;
        }

        /*
         * evaluates the function by recursivly calling the evaluate function until the base case which is present in the variable node class.
         * All leaves in these trees are variables.
         */
        public override Boolean Evaluate()
        {
            switch (nodeType)
            {
                case "AND":
                    return this.firstVar.Evaluate() && this.secondVar.Evaluate();
                case "OR":
                    return this.firstVar.Evaluate() || this.secondVar.Evaluate();
                case "INV":
                    return !this.firstVar.Evaluate();
                default:
                    return this.secondVar.Evaluate(); // this will purposely crash sometimes
            }
        }
    }
}
