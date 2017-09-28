using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TDDCalculator.Interfaces;

namespace TDDCalculator.Commands
{
    /// <summary>
    /// Concrete command that issues a print command to the receiver
    /// </summary>
    public class PrintCommand : ICommand
    {
        private IReceiver _receiver;

        public PrintCommand(IReceiver receiver) {
            _receiver = receiver;
        }

        public void Execute() {
            _receiver.Print();
        }
    }
}
