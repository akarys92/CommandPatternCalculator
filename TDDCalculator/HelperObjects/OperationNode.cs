using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TDDCalculator.HelperObjects
{
    /// <summary>
    /// A class that defines a node in an expression tree. A node has left and right children,
    /// an operation to perform and a resultant value.
    /// </summary>
    public class OperationNode
    {
        public OperationNode leftNode { get; set; }
        public char operation { get; set; }
        public OperationNode rightNode { get; set; }

        public double result { get; private set; }
        
/***** API METHODS *****/

        /// <summary>
        /// Constructor recursively creates an expression tree from a list of instructions.
        /// </summary>
        /// <param name="instructions">List of expressions to be converted into an expression tree.</param>
        public OperationNode(List<Instruction> instructions)
        {
            int length = instructions.Count;
            // Try to find the first, second tier operation.
            int index = FindSecondTier(instructions);
            // If no second tier operations are found, find the first first tier operation.
            if (index == -1)
            {
                index = FindFirstTier(instructions);
            }
            // If any operation was found, save the operation and repeat recursively.
            if (index >= 0)
            {
                operation = instructions[index].operation;
                // Mark the operation as added
                instructions[index].operation = 'N';

                leftNode = new OperationNode(SliceArray(instructions, 0, index - 1));

                rightNode = new OperationNode(SliceArray(instructions, index, length - 1));
            }
            // IF no operator was found you're at a leaf! Save the value and mark it with an L for leaf
            else
            {
                operation = 'L';
                if (instructions.Count > 0) { result = instructions[0].value; }
                else { result = 0; }
            }
        }
        /// <summary>
        /// Traverse the tree recursively to solve the expression.
        /// </summary>
        /// <returns>The value of the expression.</returns>
        public double Solve() {
            switch (operation) {
                case '+':
                    result = leftNode.Solve() + rightNode.Solve();
                    break;
                case '-':
                    result = leftNode.Solve() - rightNode.Solve();
                    break;
                case '*':
                    result = leftNode.Solve() * rightNode.Solve();
                    break;
                case '/':
                    result = leftNode.Solve() / rightNode.Solve();
                    break;
            }
            return result;
        }

/***** PRIVATE HELPERS *****/
        /// <summary>
        /// Helper function to find all first tier (* and /) operators. These are the operators that will
        /// be executed first.
        /// </summary>
        /// <param name="instructions">The list of expressions to search in</param>
        /// <returns>The index of the operator if found, if none found returns -1</returns>
        private int FindFirstTier(List<Instruction> instructions)
        {
            int start = instructions.Count - 1;

            for (int i = start; i >= 0; i--)
            {
                Instruction temp = instructions[i];
                if (temp.operation == '*' || temp.operation == '/')
                {
                    return i;
                }
            }
            return -1;
        }

        /// <summary>
        /// Helper function to find all second tier (+ and -) operators. These are the operators that will
        /// be executed second.
        /// </summary>
        /// <param name="instructions"></param>
        /// <returns>The index of the operator if found, if none found returns -1</returns>
        private int FindSecondTier(List<Instruction> instructions)
        {
            int start = instructions.Count - 1;

            for (int i = start; i >= 0; i--)
            {
                Instruction temp = instructions[i];
                if (temp.operation == '+' || temp.operation == '-')
                {
                    return i;
                }
            }
            return -1;
        }

        /// <summary>
        /// Helper method to get a subset of an inputted array. 
        /// </summary>
        /// <param name="inst">The set of instructions to be acted upon.</param>
        /// <param name="start">The first index to be included in the subset</param>
        /// <param name="end">The end index inclusive to be in the subset</param>
        /// <returns>The subset from start to end inclusive of the inputted array of instructions</returns>
        private List<Instruction> SliceArray(List<Instruction> inst, int start, int end)
        {
            int length = end - start + 1;

            List<Instruction> output = new List<Instruction>(length);
            for (int i = start; i <= end; i++)
            {
                output.Add(inst[i]);
            }
            return output;
        }
    }
}
