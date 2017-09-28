using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using TDDCalculator.HelperObjects;

namespace TDDTests
{
    [TestClass]
    public class OperationNodeTests
    {
        /// <summary>
        /// Test to make sure that trees are being properly formed according to the order of operations 
        /// property. We will take the known form of an expression represented by a pre order traversal
        /// and assert that it is the form of the tree.
        /// Unit Under Test: OperationNode constructor
        /// </summary>
        [TestMethod]
        public void ConfirmTreeOrderingCorrect()
        {
            /*
             *  5 + 3 x 2 => 
             *           +
             *       +       x
             *     0   5   3   2
             *   Pre Order Traverse: +, +, 0, 5, x, 3, 2
             */

            // SETUP
            List<Instruction> instList = new List<Instruction>();
            Instruction i1 = new Instruction();
            i1.value = 5;
            i1.operation = '+';
            instList.Add(i1);

            Instruction i2 = new Instruction();
            i2.value = 3;
            i2.operation = '+';
            instList.Add(i2);

            Instruction i3 = new Instruction();
            i3.value = 2;
            i3.operation = '*';
            instList.Add(i3);

            // ACT
            OperationNode expressionTree = new OperationNode(instList);

            //ASSERT
            Assert.AreEqual('+', expressionTree.operation);
            Assert.AreEqual('+', expressionTree.leftNode.operation);
            Assert.AreEqual(0, expressionTree.leftNode.leftNode.result);
            Assert.AreEqual(5, expressionTree.leftNode.rightNode.result);
            Assert.AreEqual('*', expressionTree.rightNode.operation);
            Assert.AreEqual(3, expressionTree.rightNode.leftNode.result);
            Assert.AreEqual(2, expressionTree.rightNode.rightNode.result);
        }

        /// <summary>
        /// Confirms that a known order of operations dependent expression is solved correctly.
        /// 5 + 3 * 2 = 11
        /// Unit Under Test: OperationNode.Solve()
        /// </summary>
        [TestMethod]
        public void SolveForKnownExpressionValueInExpressionTree() {
            //SETUP
            List<Instruction> instList = new List<Instruction>();
            Instruction i1 = new Instruction();
            i1.value = 5;
            i1.operation = '+';
            instList.Add(i1);

            Instruction i2 = new Instruction();
            i2.value = 3;
            i2.operation = '+';
            instList.Add(i2);

            Instruction i3 = new Instruction();
            i3.value = 2;
            i3.operation = '*';
            instList.Add(i3);
            OperationNode expressionTree = new OperationNode(instList);

            // ACT
            double value = expressionTree.Solve();

            // ASSERT
            Assert.AreEqual(11, value);
        }

        /// <summary>
        /// Solving an empty tree should give a value of 0.
        /// Unit Under Test: OperationNode.Solve()
        /// </summary>
        [TestMethod]
        public void SolveEmptyTreeReturnsZero()
        {
            // SETUP 
            List<Instruction> instList = new List<Instruction>();
            instList.Add(new Instruction());
            OperationNode expressionTree = new OperationNode(instList);

            // ACT
            double solution = expressionTree.Solve();

            //ASSERT
            Assert.AreEqual(0, solution);
        }
    }
}
