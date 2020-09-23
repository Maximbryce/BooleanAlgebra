using System;
using System.Collections.Generic;
using System.Xml;
using BooleanAlgebra;

namespace SequentialAlgebra
{
    class StateEquation : BooleanExpression
    {
        public String OutputVariable {get; private set; }
        //private Dictionary<String, StateEquation> EquationVariables;
        //private Dictionary<String, >

        StateEquation(String equation) : base(getExpression(equation))
        {
            this.OutputVariable = getVariable(equation);
        }

        //TODO Add some kind of error checking if this is not an equation ie no =. This would return a -1
        private static String getExpression(String equation)
        {
            int splitPoint = equation.IndexOf('=');
            return equation.Substring(splitPoint + 1);
        }

        private static String getVariable(String equation)
        {
            int splitPoint = equation.IndexOf('=');
            return equation.Substring(0, splitPoint).Remove('+');
        }

    }
}
