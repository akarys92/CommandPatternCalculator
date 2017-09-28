using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TDDCalculator.Interfaces;

namespace TDDCalculator.Commands
{
    /// <summary>
    /// Concrete command that issues a solve command to the receiver
    /// </summary>
    public class SolveCommand: ICommand
    {
        private IReceiver _receiver;

        public SolveCommand(IReceiver receiver) {
            _receiver = receiver;
        }

        public void Execute() {
            _receiver.Solve();
        }
    }
}
