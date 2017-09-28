using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TDDCalculator.HelperObjects;
using TDDCalculator.Interfaces;

namespace TDDCalculator
{
    /// <summary>
    /// The concrete receiver which maintains a stack of instructions it has been sent and can be 
    /// commanded by an invoker to run them. 
    /// </summary>
    public class CalcReceiver: IReceiver
    {
        private double _val;
        private Stack<Instruction> _iStack;
       
        /// <summary>
        /// Generic constructor for the receiver class.
        /// </summary>
        public CalcReceiver()
        {
            _val = 0;
            ResetStack();
        }

/******* API Methods ******/

        /// <summary>
        /// Create an instruction for an entered operation and value. Every valid instruction will need to 
        /// have both an operation and value. 
        /// </summary>
        /// <param name="op">Operation to perform</param>
        /// <param name="value">Value to perform against</param>
        public void AddInstruction(char op, double value)
        {
            //TODO: Is this where this should be?
            if (op == '/' && value == 0) {
                throw new InvalidOperationException("Cannot divide by zero!");
            }

            Instruction newInst = new Instruction();
            newInst.operation = op;
            newInst.value = value;

            _iStack.Push(newInst);
        }

        /// <summary>
        /// Solve for the current state of the receiver. Update the internal value to the result.
        /// </summary>
        public void Solve()
        {
            List<Instruction> instructions = Stack2InstructionList();
            OperationNode root = new OperationNode(instructions);
            _val = root.Solve();
            ResetStack();
        }

        /// <summary>
        /// Print the current value held in the receiver.
        /// </summary>
        public void Print()
        {
            Console.Write("\n" + _val + " ");
        }

        /// <summary>
        /// Reset the internal value of the receiver
        /// </summary>
        public void Reset() {
            _val = 0;
            ResetStack();
        }

/****** UTILITY METHODS *******/

        /// <summary>
        /// Utility method to check the last inputted instruction to the stack without popping it.
        /// </summary>
        /// <returns>A copy of the instruction at the top of the stack</returns>
        public Instruction GetLastInstruction() {
            return _iStack.Peek();
        }

        /// <summary>
        /// Utility accessor for internal _val.
        /// </summary>
        /// <returns>Current value in receiver</returns>
        public double GetCurrentValue() {
            return _val;
        }

/****** PRIVATE METHODS ******/

        /// <summary>
        /// Takes a call stack of instructions and turns it into a list of command for the receiver
        /// to execute. Handles special cases of instructions. 
        /// </summary>
        /// <param name="stack">A stack of instructions generated in real time.</param>
        /// <returns>A list of instructions for the receiver to execute.</returns>
        private List<Instruction> Stack2InstructionList() {
            Stack<Instruction> instList = new Stack<Instruction>();
            // Process the stack
            while(_iStack.Count > 0)
            {
                Instruction temp = _iStack.Pop();
                switch (temp.operation) {
                    case '!':
                        Instruction factInst = _iStack.Pop();
                        instList.Push(CreateFactorialInstruction(factInst));
                        break;
                    case 'I':
                        Instruction invertInst = _iStack.Pop();
                        instList.Push(CreateInvertInstruction(invertInst));
                        break;
                    default:
                        instList.Push(temp);
                        break;
                }
            }
            return instList.ToList<Instruction>();
        }

        /// <summary>
        /// Private method which takes an instruction and makes it's value a factorial.
        /// </summary>
        /// <param name="inst"></param>
        /// <returns>The instruction with inst.value = {value}!</returns>
        private Instruction CreateFactorialInstruction(Instruction inst) {
            inst.value = CalculateFactorial(inst.value);
            return inst;
        }

        /// <summary>
        /// Private method to calculate the value of a factorial command
        /// </summary>
        /// <param name="number"></param>
        /// <returns>{number}!</returns>
        private double CalculateFactorial(double number) {
            if (number % 1 != 0 || number < 0) {
                throw new InvalidOperationException("Factorial can only be calculated from a positive integer value!");
            }
            double value = 1;
            for (int i = 1; i <= number; i++) {
                value *= i;
            }
            return value;
        }

        /// <summary>
        /// Takes an instruction and inverts it
        /// </summary>
        /// <param name="inst"></param>
        /// <returns>The instrcution with inst.value = 1 / inst.value</returns>
        private Instruction CreateInvertInstruction(Instruction inst) {
            if (inst.value == 0) {
                throw new InvalidOperationException("Invert command cannot divide by 0!");
            }
            double inverted = 1 / inst.value;
            inst.value = inverted;
            return inst;
        }

        /// <summary>
        /// Sets the instruction stack back to empty then sets the current initial value.
        /// </summary>
        private void ResetStack() {
            _iStack = new Stack<Instruction>();
            Instruction firstInst = new Instruction();
            firstInst.operation = 'N';
            firstInst.value = _val;
            _iStack.Push(firstInst);
        }
    }
}
