using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;

namespace Overcoach.Tests
{
    [TestFixture]
    public sealed class TestExtensionMethods
    {

        [Test]
        public void TestCombinations()
        {
            var letters = new[] {'a', 'b', 'c', 'd'};
            Assert.AreEqual(6, letters.Combinations(2).Count());
        }

        [Test]
        public void TestCombinationsWithRepetition()
        {
            var letters = new List<char>(new[] { 'a', 'b', 'c', 'd' });
            Assert.AreEqual(10, letters.CombinationsWithRepition(2).Count());
        }
    }
}
