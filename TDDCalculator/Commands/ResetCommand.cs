using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TDDCalculator.Interfaces;

namespace TDDCalculator.Commands
{
    /// <summary>
    /// Concrete command class that on execute pushes an all clear command to a receiver.
    /// </summary>
    public class ResetCommand: ICommand
    {
        private IReceiver _receiver;

        public ResetCommand(IReceiver receiver) {
            _receiver = receiver;
        }

        public void Execute() {
            _receiver.Reset();
        }
    }
}
