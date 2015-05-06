using System;
using System.Linq;
using IntelliSun.Collections;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace IntelliSun.Tests
{
    [TestClass]
    public class EnumerableExtensionsTests
    {
        [TestMethod]
        public void DistinctByTest()
        {
            const int expectedItems = 2;
            var collection = new[] {
                new Entity(234),
                new Entity(664),
                new Entity(664)
            };

            var result = collection.DistinctBy(e => e.Value);
            var count = result.Count();

            Assert.AreEqual(count, expectedItems);
        }

        private class Entity
        {
            public Entity(int value)
            {
                this.Value = value;
            }

            public int Value { get; private set; }
        }
    }
}
