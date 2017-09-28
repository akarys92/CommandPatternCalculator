using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using TDDCalculator;
using TDDCalculator.HelperObjects;

namespace TDDTests
{
    [TestClass]
    public class CalcReceiverTests
    {
        /// <summary>
        /// Make sure solving on empty returns 0, no exceptions thrown.
        /// Unit under test: CalcReceiver.Solve()
        /// </summary>
        [TestMethod]
        public void SolvingWithNoInstructionsIsZeroReceiver() {
            // SETUP
            CalcReceiver receiver = new CalcReceiver();

            // ACT
            receiver.Solve();

            // ASSERT
            Assert.AreEqual(0, receiver.GetCurrentValue());
        }

        /// <summary>
        /// Test that instructions are being created with the appropriate values and operators.
        /// Unit under test: CalcReceiver.AddInstruction()
        /// </summary>
        [TestMethod]
        public void AddInstructionToReceiver() {
            // SETUP
            CalcReceiver receiver = new CalcReceiver();
            
            // ACT
            receiver.AddInstruction('+', 10);
            Instruction i = receiver.GetLastInstruction();

            // ASSERT
            Assert.AreEqual('+', i.operation);
            Assert.AreEqual(10, i.value);
        }

        /// <summary>
        /// Tests basic operation of calculator using addition. 
        /// Unit under test: CalcReceiver.Solve()
        /// </summary>
        [TestMethod]
        public void SolveBasicAdditionOnReceiver()
        {
            // 10 + 10 = 20
            // SETUP
            CalcReceiver receiver = new CalcReceiver();
            receiver.AddInstruction('+', 10);
            receiver.AddInstruction('+', 10);

            // ACT
            receiver.Solve();

            // ASSERT
            Assert.AreEqual(20, receiver.GetCurrentValue());
        }

        /// <summary>
        /// Tests basic operation of calculator using a negative number.
        /// Unit under test: CalcReceiver.Solve()
        /// </summary>
        [TestMethod]
        public void SolveWithNegativeOnReceiver()
        {
            // -5*5/3= = -8.3333
            // SETUP
            CalcReceiver receiver = new CalcReceiver();
            receiver.AddInstruction('+', -5);
            receiver.AddInstruction('*', 5);
            receiver.AddInstruction('/', 3);

            // ACT
            receiver.Solve();

            // ASSERT
            Assert.AreEqual(-8.33, receiver.GetCurrentValue(), .01);
        }

        /// <summary>
        /// Tests basic operation of calculator using a decimal number. 
        /// Unit under test: CalcReceiver.Solve()
        /// </summary>
        [TestMethod]
        public void SolveBasicAdditionWithDecimalOnReceiver()
        {
            // 0.5 + 10 = 10.5
            // SETUP
            CalcReceiver receiver = new CalcReceiver();
            receiver.AddInstruction('+', 0.5);
            receiver.AddInstruction('+', 10);

            // ACT
            receiver.Solve();

            //ASSERT
            Assert.AreEqual(10.5, receiver.GetCurrentValue());
        }

        /// <summary>
        /// Tests order of operation.
        /// Unit under test: CalcReceiver.Solve()
        /// </summary>
        [TestMethod]
        public void SolveWithOrderOfOperationOnReceiver()
        {
            // -5 * 5 - 15 / 3 = -30
            // SETUP
            CalcReceiver receiver = new CalcReceiver();
            receiver.AddInstruction('+', -5);
            receiver.AddInstruction('*', 5);
            receiver.AddInstruction('-', 15);
            receiver.AddInstruction('/', 3);

            // ACT
            receiver.Solve();

            // ASSERT
            Assert.AreEqual(-30, receiver.GetCurrentValue());
        }

        /// <summary>
        /// Tests expression using an inversion operator. 
        /// Unit under test: CalcReceiver.Solve()
        /// </summary>
        [TestMethod]
        public void SolveWithInversionOperationOnReceiver()
        {
            // 0.5 * 1 / x * 2 = 4
            // SETUP
            CalcReceiver receiver = new CalcReceiver();
            receiver.AddInstruction('+', 0.5);
            receiver.AddInstruction('I', 0);
            receiver.AddInstruction('*', 2);
            
            // ACT
            receiver.Solve();

            // ASSERT
            Assert.AreEqual(4, receiver.GetCurrentValue());
        }

        /// <summary>
        /// Tests expression using a factorial operator. 
        /// Unit under test: CalcReceiver.Solve()
        /// </summary>
        [TestMethod]
        public void SolveWithFactorialOperationOnReceiver()
        {
            // 5! / 4 = 30
            // SETUP
            CalcReceiver receiver = new CalcReceiver();
            receiver.AddInstruction('+', 5);
            receiver.AddInstruction('!', 0);
            receiver.AddInstruction('/', 4);
            
            // ACT
            receiver.Solve();
            
            // ASSERT
            Assert.AreEqual(30, receiver.GetCurrentValue());
        }

        /// <summary>
        /// Test that calling a factorial for a decimal throws an invalid operation exception. (Discussed
        /// in readme).
        /// Unit under test: CalcReceiver.Solve()
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(InvalidOperationException))]
        public void FactorialOfDecimalAttemptThrowsException() {
            // Assert .5! = Throws an exception
            //SETUP
            CalcReceiver receiver = new CalcReceiver();
            receiver.AddInstruction('+', 0.5);
            receiver.AddInstruction('!', 0);
            // ACT
            receiver.Solve();
        }

        /// <summary>
        /// Test that attempting to divide by zero throws an invalid operation exception.
        /// Unit under test: CalcReceiver.Solve()
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(InvalidOperationException))]
        public void DivideByZeroAttemptThrowsExceptionFromReceiver()
        {
            // Assert .5! = Throws an exception
            // SETUP
            CalcReceiver receiver = new CalcReceiver();
            
            // ACT
            receiver.AddInstruction('/', 0);
            receiver.Solve();
        }

        /// <summary>
        /// Tests that the internal value (state) of the receiver remains after a solve command.
        /// Unit under test: CalcReceiver state
        /// </summary>
        [TestMethod]
        public void TestContinuationInReceiver() {
            // 5! / 4 = 30
            // SETUP
            CalcReceiver receiver = new CalcReceiver();
            receiver.AddInstruction('+', 5);
            receiver.AddInstruction('!', 0);
            receiver.AddInstruction('/', 4);

            // ACT
            receiver.Solve();
            // 5! / 4 = 30

            receiver.AddInstruction('-', 10);
            receiver.Solve();
            // 30 - 10 = 20

            // ASSERT
            Assert.AreEqual(20, receiver.GetCurrentValue());
        }
    }
}
