using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using TestDIPS.Core.Utility;

namespace UnitTestProject1

{
    [TestClass]
    public class StringCalculatorTests
    {
        [TestMethod]
        public void Add_GivenEmtyString_Returns0()
        {
            VerifyAdd("", 0);
        }

        [TestMethod]
        public void Add_GivenOneNumber_ReturnsNumber()
        {
            VerifyAdd("1", 1);
        }

        [TestMethod]
        public void Add_GivenTwoNumbers_ReturnsSum()
        {
            VerifyAdd("1,2", 3);
        }

        [TestMethod]
        public void Add_GivenAnyLeangthOfNumbers_ReturnsSum()
        {
            VerifyAdd("1,2,3", 6);
        }

        [TestMethod]
        public void Add_GivenNewLines_ReturnsSum()
        {
            VerifyAdd("1\n2", 3);
        }

        [TestMethod]
        public void Add_GivenDelimiters_ReturnsSum()
        {
            VerifyAdd("//;\n1;2", 3);
        }

        [TestMethod]
        public void Add_GivenDelimitersAtAnyLength_ReturnsSum()
        {
            VerifyAdd("//[***]\n1***2***3", 6);
        }

        [TestMethod]
        public void Add_MultipleGivenDelimiters_ReturnsSum()
        {
            VerifyAdd("//[*][%]\n1*2%3*4*4", 14);
        }

        [TestMethod]
        public void Add_MultipleGivenDelimitersWithDifferentLength_ReturnsSum()
        {
            VerifyAdd("//[***][%]\n1***2%3\n4***4", 14);
        }

        [TestMethod]
        public void Add_NegativeNumber_ErrorMessageContainsNumber()
        {
            var aCalculator = new Calculator();

            Exception exception = Record.Exception(() => aCalculator.Add("-1"));

            Assert.IsInstanceOfType(exception, typeof(ArgumentException));
            Assert.AreEqual("Negatives not allowed: -1", exception.Message);
        }

        [TestMethod]
        public void Add_NumbersBiggerThanThousand_ReturnsSumIgnoringNumber()
        {
            VerifyAdd("2,1001", 2);
        }

        private static void VerifyAdd(string numbers, int expected)
        {
            var aCalculator = new Calculator();

            int result = aCalculator.Add(numbers);

            Assert.AreEqual(expected, result);
        }

        
    }
}