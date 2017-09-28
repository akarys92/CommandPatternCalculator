using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TDDCalculator.Interfaces
{
    public interface IInvoker
    {
        void AddInstruction(char operation, double value);
        void SolveEquation();
        void Clear();
        void AllClear();
    }
}
