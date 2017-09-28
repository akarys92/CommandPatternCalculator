using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TDDCalculator.Interfaces;

namespace TDDTests.Mocks
{
    public class MockReceiver : IReceiver
    {
        public char op { get; private set; }
        public double value { get; private set; }
        public string lastCommand { get; private set; }

        public void AddInstruction(char inOP, double inValue)
        {
            op = inOP;
            value = inValue;
            lastCommand = "Add";
        }

        public void Print()
        {
            lastCommand = "Print";
        }

        public void Reset()
        {
            lastCommand = "Reset";
        }

        public void Solve()
        {
            lastCommand = "Solve";
        }
    }
}
