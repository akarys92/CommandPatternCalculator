using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using TDDTests.Mocks;
using TDDCalculator;

namespace TDDTests
{
    [TestClass]
    public class CalcInvokerTests
    {
        /// <summary>
        /// Test that add instruction is adding instructions to invoker queue
        /// Unit under test: CalcInvoker.AddInstruction()
        /// </summary>
        [TestMethod]
        public void AddInstructionToInvokerQueue()
        {
            // SETUP
            MockReceiver receiver = new MockReceiver();
            CalcInvoker invoker = new CalcInvoker(receiver);

            // ACT
            invoker.AddInstruction('+', 5);

            // ASSERT
            Assert.AreEqual(1, invoker.GetQueueLength());
        }

        /// <summary>
        /// Test that instructions get properly executed on the receiver when 
        /// solve is called
        /// Unit under test: CalcInvoker.SolveEquation()
        /// </summary>
        [TestMethod]
        public void CallSolveEquationMethodOnInvoker()
        {
            // SETUP
            MockReceiver receiver = new MockReceiver();
            CalcInvoker invoker = new CalcInvoker(receiver);
            invoker.AddInstruction('+', 5);

            // ACT
            invoker.SolveEquation();

            // ASSERT
            Assert.AreEqual(receiver.op, '+');
            Assert.AreEqual(receiver.value, 5);
        }

        /// <summary>
        /// Test that clear removes the last command from the queue
        /// Unit under test: CalcInvoker.Clear()
        /// </summary>
        [TestMethod]
        public void CallClearMethodOnInvoker()
        {
            // SETUP
            MockReceiver receiver = new MockReceiver();
            CalcInvoker invoker = new CalcInvoker(receiver);
            invoker.AddInstruction('+', 5);
            invoker.AddInstruction('-', 10);
            invoker.Clear();

            // ACT
            invoker.SolveEquation();

            // ASSERT
            Assert.AreEqual(receiver.op, '+');
            Assert.AreEqual(receiver.value, 5);
        }

        /// <summary>
        /// Test that all clear removes all commands from the queue and resets
        /// the receiver.
        /// Unit under test: CalcInvoker.AllClear()
        /// </summary>
        [TestMethod]
        public void CallAllClearMethodOnInvoker()
        {
            // SETUP
            MockReceiver receiver = new MockReceiver();
            CalcInvoker invoker = new CalcInvoker(receiver);
            invoker.AddInstruction('+', 5);
            invoker.AddInstruction('-', 10);

            // ACT
            invoker.AllClear();

            // ASSERT
            Assert.AreEqual(0, invoker.GetQueueLength());
            Assert.AreEqual(receiver.lastCommand, "Reset");
        }

    }
}
