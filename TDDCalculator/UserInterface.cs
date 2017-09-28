using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.RegularExpressions;
using TDDCalculator.Interfaces;

namespace TDDCalculator
{
    /// <summary>
    /// Class for handling user input including parsing and handling erroneous input. Passes data
    /// onto the calculator's invoker to continue on downstream.
    /// </summary>
    public class UserInterface
    {
        private List<char> _input;
        private Regex _pattern = new Regex(@"[+-/*0-9.]");
        private Regex _opPattern = new Regex(@"[+/*-]");
        private Regex _specialOpPattern = new Regex(@"[!X]");
        private IInvoker _invoker;
        List<char> _num;
        private char _op;
        private bool _invertState;

        /// <summary>
        /// Initializing the client sets up necessary objects and then jumps into the input loop.
        /// </summary>
        public UserInterface(IInvoker invoker)
        {
            _input = new List<char>();
            _invoker = invoker;
            _num = new List<char>();
            _op = '+';
            _invertState = false;
        }

/***** API MEHTODS *****/

        /// <summary>
        /// Interaction component with user input. Contains the logic for handling all 
        /// input including negative numbers and decimals.
        /// </summary>
        public void GetInput(char ch)
        {
            // Check if the key is part of the expression syntax
            if (_pattern.IsMatch(ch.ToString()) || _specialOpPattern.IsMatch(ch.ToString()))
            {
                HandleExpression(ch);
            }
            // Input is allowed, but not part of the general expression syntax. Handle explicitly...
            else
            {
                HandleNonExpressions(ch);
            }
        }

/***** PRIVATE METHODS *****/
        /// <summary>
        /// Handle mapping user input to to the AddInstruction method on the invoker.
        /// </summary>
        /// <param name="ch">Last character that user inputted</param>
        private void HandleExpression(char ch) {
            // Check if the input is an operator
            if (_opPattern.IsMatch(ch.ToString()) || _specialOpPattern.IsMatch(ch.ToString()))
            {
                // Handle the negative case: if I receive a '-' and there are no digits seen, 
                // this is the lead of a negative number. So track it as a digit.
                if (_num.Count == 0 && ch == '-')
                {
                    _num.Add(ch);
                }
                else if (ch == 'X') {
                    if (_op == '/' && _invertState)
                    {
                        SafeAddInstruction('I', 0);
                    }
                    else {
                        Retake("Invalid syntax detected for 1/X, please try again.");
                    }
                }
                // If I have digits tracked and see an operator, I submit the operator and value as an instruction
                else if (_num.Count > 0)
                {
                    // An instruction of *1 does not change value but could be the first piece of *1/x, don't add
                    // it as an instruction but track that we are in a potential invert command.
                    if (_op == '*' && GetNumber(_num) == 1)
                    {
                        _invertState = true;
                        _op = ch;
                    }
                    else
                    {
                        SafeAddInstruction(_op, GetNumber(_num));
                        _num = new List<char>();
                        _op = ch;
                    }
                }
                // If the count is 0, this is the first input, track it and move on to the next input.
                else
                {
                    _op = ch;
                }
                // If we have a factorial, send a command for it and continue
                if (_op == '!')
                {
                    SafeAddInstruction('!', 0);
                    _op = '+';
                }
            }
            // If this is not an operator but is in the expression, it is a digit 0-9. Add it to the list of digits.
            else
            {
                _num.Add(ch);
            }
        }

        /// <summary>
        /// Handle mapping user input that is not part of the expression syntax to the individual other methods on 
        /// the invoker.
        /// </summary>
        /// <param name="ch">Last character that user inputted</param>
        private void HandleNonExpressions(char ch) {
            switch (ch)
            {
                case '=':
                    SafeAddInstruction(_op, GetNumber(_num));
                    _num = new List<char>();
                    _op = '+';
                    _invertState = false;
                    SafeSubmit();
                    ResetOperators();
                    break;
                case 'C':
                    SafeAddInstruction(_op, GetNumber(_num));
                    _num = new List<char>();
                    _invoker.Clear();
                    break;
                case 'A':
                    _num = new List<char>();
                    _invoker.AllClear();
                    ResetOperators();
                    break;
                case ' ':
                    break;
                default:
                    InputNotAllowed();
                    _num = new List<char>();
                    break;
            }
        }
        /// <summary>
        /// Submit method wrapped in a try block so if a downstream validator throws an error it will be caught here and gracefully
        /// handled for the user. 
        /// </summary>
        /// <tests>
        /// 
        /// </tests>
        private void SafeSubmit()
        {
            try
            {
                _invoker.SolveEquation();
            }
            catch (Exception e)
            {
                Retake("Invalid expression! Please try again.");
            }
        }
        /// <summary>
        /// Validates a candidate for an instruction. If it is valid then the instruction can be passed on for submission.
        /// </summary>
        /// <param name="op">The operator to use in this instruction.</param>
        /// <param name="value">The value to be operated on.</param>
        private void SafeAddInstruction(char op, double value)
        {
            // Error Conditions:
            if (op == '/' && value == 0)
            {
                Retake("Cannot divide by zero. Try again.");
            }
            else
            {
                _invoker.AddInstruction(op, value);
            }
        }
        /// <summary>
        /// Takes a list of digits and returns a double. Handles negatives and decimals.
        /// </summary>
        /// <param name="list">The list of characters to be parsed into a double.</param>
        /// <returns></returns>
        private double GetNumber(List<char> list)
        {
            if (list.Count == 0)
            {
                return 0;
            }
            char[] arr = list.ToArray<char>();
            string stNum = new string(arr);

            // If the user tries to enter another operator as an operand, do not accept the value.
            try
            {
                return double.Parse(stNum);
            }
            catch (Exception e)
            {
                Retake("Error! A valid number must follow an operator. Try again.");
                return 0;
            }
        }
        /// <summary>
        /// Indicates to the user that they have entered an unallowed character. Calls retake to reset.
        /// </summary>
        private void InputNotAllowed()
        {
            Retake("Character not allowed, try again!");
        }
        /// <summary>
        /// Called when an input that could put the system in an error state is detected. Prints inputted message 
        /// and clears the system to make sure no erroneous input is passed downstream.
        /// </summary>
        /// <param name="message">Message to display to the user.</param>
        private void Retake(string message)
        {
            Console.WriteLine("\n" + message);
            _invoker.AllClear();
            ResetOperators();
        }

        private void ResetOperators() {
            _op = '+';
            _invertState = false;
        }
    }
}
