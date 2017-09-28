using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using TDDCalculator.Commands;
using TDDCalculator.Interfaces;

namespace TDDCalculator
{
    /// <summary>
    /// Invoker class that acts as an interface between the client and receiver. Responsible for creating and 
    /// sending commands from the client to the receiver.
    /// </summary>
    public class CalcInvoker : IInvoker
    {
        private IReceiver _receiver;
        private Queue<ICommand> _commandQ;

/***** API METHODS *****/

        /// <summary>
        /// Construct and create the CalcReceiver that will receive commands.
        /// </summary>
        public CalcInvoker()
        {
            _receiver = new CalcReceiver();
            _commandQ = new Queue<ICommand>();
        }

        /// <summary>
        /// Construct and take a new receiver that will receive commands.
        /// </summary>
        public CalcInvoker(IReceiver receiver)
        {
            _receiver = receiver;
            _commandQ = new Queue<ICommand>();
        }

        /// <summary>
        /// Add a new Instruction to the queue to be executed later
        /// </summary>
        /// <param name="operation">Operation to perform.</param>
        /// <param name="value">Value to use.</param>
        public void AddInstruction(char operation, double value)
        {
            AddInstructionCommand instCommand = new AddInstructionCommand(_receiver, operation, value);
            _commandQ.Enqueue(instCommand);   
        }

        /// <summary>
        /// Adds a solve and print command to the queue and executes all commands 
        /// </summary>
        public void SolveEquation()
        {
            SolveCommand sCommand = new SolveCommand(_receiver);
            _commandQ.Enqueue(sCommand);

            PrintCommand prCommand = new PrintCommand(_receiver);
            _commandQ.Enqueue(prCommand);

            ExecuteAll();
        }

        /// <summary>
        /// Clear the last command from the queue
        /// </summary>
        public void Clear() {
            Queue<ICommand> tempQ = new Queue<ICommand>();
            while (_commandQ.Count > 0) {
                if (_commandQ.Count > 1)
                {
                    ICommand tempCommand = _commandQ.Dequeue();
                    tempQ.Enqueue(tempCommand);
                }
                else {
                    _commandQ.Dequeue();
                }
            }

            while (tempQ.Count > 0) {
                ICommand tempCommand = tempQ.Dequeue();
                _commandQ.Enqueue(tempCommand);
            }
        }

        /// <summary>
        /// Reset the system. 
        /// </summary>
        public void AllClear() {
            _commandQ = new Queue<ICommand>();
            ResetCommand rCommand = new ResetCommand(_receiver);
            rCommand.Execute();
        }

/***** UTILITY METHODS *****/
        /// <summary>
        /// Get the current number of instructions in the queue
        /// </summary>
        /// <returns></returns>
        public int GetQueueLength() {
            return _commandQ.Count;
        }

/***** PRIVATE METHODS *****/
        /// <summary>
        /// Execute all commands in the queue
        /// </summary>
        private void ExecuteAll() {
            while (_commandQ.Count > 0)
            {
                ICommand temp = _commandQ.Dequeue();
                temp.Execute();
            }
        }
    }
}
