using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using TDDCalculator.Commands;
using TDDTests.Mocks;

namespace TDDTests
{
    [TestClass]
    public class CommandsTest
    {
        /// <summary>
        /// Test that the reset command properly sets values properly
        /// Unit under test: ResetCommand.Execute();
        /// </summary>
        [TestMethod]
        public void TestResetCommandPassedToReceiver()
        {
            // SETUP
            MockReceiver receiver = new MockReceiver();
            ResetCommand command = new ResetCommand(receiver);

            // ACT
            command.Execute();

            // ASSERT
            Assert.AreEqual("Reset", receiver.lastCommand);
        }

        /// <summary>
        /// Test that the Print command properly sets values properly
        /// Unit under test: PrintCommand.Execute();
        /// </summary>
        [TestMethod]
        public void TestPrintCommandPassedToReceiver()
        {
            // SETUP
            MockReceiver receiver = new MockReceiver();
            PrintCommand command = new PrintCommand(receiver);

            // ACT
            command.Execute();

            // ASSERT
            Assert.AreEqual("Print", receiver.lastCommand);
        }

        /// <summary>
        /// Test that the Solve command properly sets values properly
        /// Unit under test: SolveCommand.Execute();
        /// </summary>
        [TestMethod]
        public void TestSolveCommandPassedToReceiver()
        {
            // SETUP
            MockReceiver receiver = new MockReceiver();
            SolveCommand command = new SolveCommand(receiver);

            // ACT
            command.Execute();

            // ASSERT
            Assert.AreEqual("Solve", receiver.lastCommand);
        }

        /// <summary>
        /// Test that the add instruction command properly sets values
        /// Unit under test: AddInstructionCommand.Execute();
        /// </summary>
        [TestMethod]
        public void TestAddInstructionByValuePassedToReceiver()
        {
            // SETUP
            MockReceiver receiver = new MockReceiver();
            AddInstructionCommand command = new AddInstructionCommand(receiver, '+', 0);

            // ACT
            command.Execute();

            // ASSERT
            Assert.AreEqual('+', receiver.op);
            Assert.AreEqual(0, receiver.value);
        }     
    }
}
