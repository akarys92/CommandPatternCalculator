using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TDDCalculator.Interfaces;
using TDDCalculator.HelperObjects;

namespace TDDTests.Mocks
{
    public class MockInvoker : IInvoker
    {
        public List<Instruction> instructions;
        public bool AllClearCalled = false;
        public bool ClearCalled = false;
        public bool SolveEquationCalled = false;
        public bool AddInstructionCalled = false;
        public int CallsMade = 0;

        public MockInvoker()
        {
            instructions = new List<Instruction>();
        }

        public void AddInstruction(char operation, double value)
        {
            Instruction i = new Instruction(operation, value);
            instructions.Add(i);
            AddInstructionCalled = true;
            CallsMade++;
        }

        public void AllClear()
        {
            AllClearCalled = true;
            CallsMade++;
        }

        public void Clear()
        {
            ClearCalled = true;
            CallsMade++;
        }        

        public void SolveEquation()
        {
            SolveEquationCalled = true;
            CallsMade++;
        }
    }
}

