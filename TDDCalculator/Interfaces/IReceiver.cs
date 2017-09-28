using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TDDCalculator.Interfaces
{
    /// <summary>
    /// Defines the receiver API surface.
    /// </summary>
    public interface IReceiver
    {
        void AddInstruction(char op, double value);
        void Solve();
        void Print();
        void Reset();
    }
}
