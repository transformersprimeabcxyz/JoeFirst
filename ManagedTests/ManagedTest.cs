using System;
using Managed;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ManagedTests
{
    [TestClass]
    public class TestManagedClasses
    {
        [TestMethod]
        [Timeout(1000)]
        public void CheckMatch()
        {
            bool result;

            result = CodingProblems.checkMatch("This is a test", "Thi.... a test");
            Assert.AreEqual(true, result, "[This is a test][Thi.... a test]");

            result = CodingProblems.checkMatch("Why would this work someHow? ", "*How.");
            Assert.AreEqual(false, result, "[Why would this work someHow? ][*How.]");

            result = CodingProblems.checkMatch("There is NoWay", "*NoWay*");
            Assert.AreEqual(true, result, "[There is NoWay][*NoWay*]");

            result = CodingProblems.checkMatch("ba", "a*");
            Assert.AreEqual(false, result, "[ba][a*]");

            result = CodingProblems.checkMatch("TempAbcdeAbcdefTemp", "*Abcdef*");
            Assert.AreEqual(true, result, "[TempAbcdeAbcdefTemp][*Abcdef*]");
        }

        [TestMethod]
        [Timeout(1000)]
        public void ModifyString()
        {
            string retString;
            string inputString, expected;

            inputString = "This is a interesting test";
            expected = "THIS si A gnitseretni TEST";
            retString = CodingProblems.modifyString(inputString);
            Assert.AreEqual(expected, retString);

            inputString = "What?";
            expected = "WHAT?";
            retString = CodingProblems.modifyString(inputString);
            Assert.AreEqual(expected, retString);

            inputString = "What? Time!";
            expected = "WHAT? !emiT";
            retString = CodingProblems.modifyString(inputString);
            Assert.AreEqual(expected, retString);
        }

        [TestMethod]
        [Timeout(1000)]
        public void FindNextSparse()
        {
            uint input;
            uint expected;
            uint result;

            input = 0xA3;
            expected = 0xA5;
            result = CodingProblems.findNextSparse(input);
            Assert.AreEqual(expected, input, string.Format("{0:X} -> {1:X} Actual -> {2:X}", input, expected, result));
        }
    }
}
