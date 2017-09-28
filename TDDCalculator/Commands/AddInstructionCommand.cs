using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TDDCalculator.Interfaces;

namespace TDDCalculator.Commands
{
    /// <summary>
    /// Concrete command to take an operator and value and send a command to the receiver to
    /// perform the operation using that value.
    /// </summary>
    public class AddInstructionCommand: ICommand
    {
            private IReceiver _receiver;
            private char _op;
            private double _value;
            /// <summary>
            /// Take in and set the operator and value on the instance of the command.
            /// </summary>
            /// <param name="newReceiver">The receiver to act on</param>
            /// <param name="op">The operator to use</param>
            /// <param name="value">The value to use</param>
            public AddInstructionCommand(IReceiver newReceiver, char op, double value)
            {
                _receiver = newReceiver;
                _op = op;
                _value = value;
            }

            public void Execute()
            {
                _receiver.AddInstruction(_op, _value);
            }
        }
}
