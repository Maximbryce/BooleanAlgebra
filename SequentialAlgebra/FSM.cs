using System;
using System.Collections.Generic;
using System.Text;
using BooleanAlgebra;

namespace SequentialAlgebra
{
    class FSM
    {
        private List<StateEquation> StateEquations;
        private List<BooleanExpression> OutputEquations;
        private Dictionary<String, Boolean> CurrentVarValues;
        private List<String> IndependentVariables;
        private List<String> StateVariables;


        public FSM(List<StateEquation> stateEquations, List<BooleanExpression> outputEquations)
        {
            this.StateEquations = stateEquations;
            this.OutputEquations = outputEquations;
            CreateVariableLists();
        }

        private void CreateVariableLists()
        {
            foreach (var variable in StateEquations)
            {
                StateVariables.Add(variable.OutputVariable);
                CurrentVarValues.Add(variable.OutputVariable, false);
                // Also need to add all of the independent variables in the state equations to the independent variables list
            }

            foreach (var variable in OutputEquations)
            {
                
            }

        }




    }
}
