using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TDDCalculator.HelperObjects
{
    /// <summary>
    /// Object to keep track of instructions. Has an operator and a value.
    /// </summary>
    public class Instruction
    {
        public char operation { get; set; }
        public double value { get; set; }
        public Instruction() { }
        public Instruction(char op, double val) {
            operation = op;
            value = val;
        }
    }
}
