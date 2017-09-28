using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using TDDCalculator;
using TDDTests.Mocks;

namespace TDDTests
{
    /// <summary>
    /// Test the user interface class. The strategy here is to create a mock invoker, pass inputs through the
    /// user interface and confirm that the right methods are being called with appropriate params.
    /// </summary>
    [TestClass]
    public class UserInterfaceTests
    {
        /// <summary>
        /// Test that a command from the user interface creates a command on the invoker.
        /// Unit under test: UserInterface.GetInput()
        /// </summary>
        [TestMethod]
        public void CreateInstructionThrougUI()
        {
            // SETUP
            MockInvoker invoker = new MockInvoker();
            UserInterface ui = new UserInterface(invoker);

            // ACT
            ui.GetInput('+');
            ui.GetInput('1');
            ui.GetInput('0');
            ui.GetInput('=');

            // ASSERT
            //  1 was added to the list of instructions
            Assert.AreEqual(1, invoker.instructions.Count);
        }

        /// <summary>
        /// Test that the solve method is executed when an = is provided.
        /// Unit under test: UserInterface.Solve()
        /// </summary>
        [TestMethod]
        public void SolveIsCalledFromUI()
        {
            // SETUP
            MockInvoker invoker = new MockInvoker();
            UserInterface ui = new UserInterface(invoker);

            // ACT
            ui.GetInput('=');

            // ASSERT
            Assert.AreEqual(true, invoker.SolveEquationCalled);
        }

        /// <summary>
        /// Test that the clear method is executed when an = is provided.
        /// Unit under test: UserInterface.Solve()
        /// </summary>
        [TestMethod]
        public void ClearMethodIsCalledFromUI()
        {
            // SETUP
            MockInvoker invoker = new MockInvoker();
            UserInterface ui = new UserInterface(invoker);

            // ACT
            ui.GetInput('C');
            ui.GetInput('=');

            // ASSERT
            Assert.AreEqual(true, invoker.ClearCalled);
        }

        /// <summary>
        /// Test that the clear method is executed when an = is provided.
        /// Unit under test: UserInterface.Solve()
        /// </summary>
        [TestMethod]
        public void AllClearMethodIsCalledFromUI()
        {
            // SETUP
            MockInvoker invoker = new MockInvoker();
            UserInterface ui = new UserInterface(invoker);

            // ACT
            ui.GetInput('A');
            ui.GetInput('=');

            // ASSERT
            Assert.AreEqual(true, invoker.AllClearCalled);
        }

        /// <summary>
        /// Test that the UI creates correct operation on instructions.
        /// Unit under test: UserInterface.GetInput()
        /// </summary>
        [TestMethod]
        public void NormalOperationCorrectlyCalled()
        {
            // SETUP
            MockInvoker invoker = new MockInvoker();
            UserInterface ui = new UserInterface(invoker);

            // ACT
            ui.GetInput('1');
            ui.GetInput('+');
            ui.GetInput('1');
            ui.GetInput('0');
            ui.GetInput('+');
            ui.GetInput('3');

            // ASSERT
            Assert.AreEqual('+', invoker.instructions[1].operation);
            Assert.AreEqual(10, invoker.instructions[1].value);

        }

        /// <summary>
        /// Test that the UI creates factorial operation on instructions.
        /// Unit under test: UserInterface.GetInput()
        /// </summary>
        [TestMethod]
        public void FactorialOperationCorrectlyCalled()
        {
            // SETUP
            MockInvoker invoker = new MockInvoker();
            UserInterface ui = new UserInterface(invoker);

            // ACT
            ui.GetInput('5');
            ui.GetInput('!');

            // ASSERT
            Assert.AreEqual(5, invoker.instructions[0].value);
            Assert.AreEqual('!', invoker.instructions[1].operation);
        }

        /// <summary>
        /// Test that the UI creates invert operation on instructions.
        /// Unit under test: UserInterface.GetInput()
        /// </summary>
        [TestMethod]
        public void InvertOperationCorrectlyCalled()
        {
            // SETUP
            MockInvoker invoker = new MockInvoker();
            UserInterface ui = new UserInterface(invoker);

            // ACT
            ui.GetInput('5');
            ui.GetInput('*');
            ui.GetInput('1');
            ui.GetInput('/');
            ui.GetInput('X');
            ui.GetInput('=');

            // ASSERT
            Assert.AreEqual(5, invoker.instructions[0].value);
            Assert.AreEqual('I', invoker.instructions[1].operation);
        }

        /// <summary>
        /// Test that if a user inputs erroneous data the invoker's starts over with an AllClear command.
        /// Unit under test: UserInterface.GetInput()
        /// </summary>
        [TestMethod]
        public void ErroneousInputClearsInvoker()
        {
            // SETUP
            MockInvoker invoker = new MockInvoker();
            UserInterface ui = new UserInterface(invoker);

            // ACT
            ui.GetInput('B');

            // ASSERT
            Assert.AreEqual(false, invoker.AddInstructionCalled);
            Assert.AreEqual(true, invoker.AllClearCalled);
            Assert.AreEqual(false, invoker.ClearCalled);
            Assert.AreEqual(false, invoker.SolveEquationCalled);
        }
    }
}
