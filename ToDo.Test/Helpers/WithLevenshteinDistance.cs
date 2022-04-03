using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ToDo.Helpers;

namespace ToDo.Test.Helpers
{
    [TestClass]
    public class WithLevenshteinDistance
    {
        private IStringMetric _stringMetric;
        
        [TestInitialize]
        public void Init()
        {
            _stringMetric = new LevenshteinDistance();
        }
        
        [DataRow("aa")]
        [DataRow("")]
        [DataRow("a")]
        [DataTestMethod]
        public void When_FirstString_Length_Is_Zero_Should_Return_SecondString_Length(string secondString)
        {
            var distance = _stringMetric.Compute(string.Empty, secondString);
            Assert.AreEqual(secondString.Length, distance);
        }
        
        [DataRow("aa")]
        [DataRow("")]
        [DataRow("a")]
        [DataTestMethod]
        public void When_StringSecond_Length_Is_Zero_Should_Return_FirstString_Length(string firstString)
        {
            var distance = _stringMetric.Compute(firstString, String.Empty);
            Assert.AreEqual(firstString.Length, distance);
        }
        
        
        [DataRow("climax", "volmax", 3)]
        [DataRow("Ram", "Raman", 2)]
        [DataRow("mama", "mom", 2)]
        [DataTestMethod]
        public void Should_Return_ExpectedDistance(string firstString, string secondString, int expectedDistance)
        {
            var distance = _stringMetric.Compute(firstString, secondString);
            Assert.AreEqual(expectedDistance, distance);
        }
    }
}
