using System;
using System.Linq;
using IntelliSun.Text;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace IntelliSun.Tests
{
    [TestClass]
    public class StringCompositorTests
    {
        private const string DefaultCompositionName = "DEFAULT";
        private const string ConditionalCompositionName = "CONDITION";

        private IStringCompositionProvider compositionProvider;

        [TestInitialize]
        public void Setup()
        {
            var cpmProvider = new StringCompositionProvider();
            cpmProvider.AddComposition(DefaultCompositionName, "[A].[$].[B]-[C]");
            cpmProvider.AddComposition(ConditionalCompositionName, "[C?.]__[$]");

            cpmProvider.AddPartsSet("A", "One");
            cpmProvider.AddPartsSet("A", "Two");
            cpmProvider.AddPartsSet("B", "A");
            cpmProvider.AddPartsSet("B", "B");
            cpmProvider.AddPartsSet("C");

            this.compositionProvider = cpmProvider;
        }

        [TestMethod]
        public void SimpleComposeTest()
        {
            var compositor = new StringCompositor(this.compositionProvider);
            var strings = compositor.Compose(DefaultCompositionName, "ARG").ToArray();

            CollectionAssert.AreEquivalent(strings, new[] {
                "One.ARG.A-",
                "One.ARG.B-",
                "Two.ARG.A-",
                "Two.ARG.B-"
            });
        }

        [TestMethod]
        public void ConditionalComposeTest()
        {
            var compositor = new StringCompositor(this.compositionProvider);
            var strings = compositor.Compose(ConditionalCompositionName, "ARG").ToArray();

            CollectionAssert.AreEquivalent(strings, new[] { "__ARG" });
        }

        [TestMethod]
        public void SimpleDecomposeTest()
        {
            const string inputText = "One.CHECK.A-";
            var compositor = new StringCompositor(this.compositionProvider);
            var result = compositor.Decompose(DefaultCompositionName, inputText);

            Assert.AreEqual(result, "CHECK");
        }

        [TestMethod]
        public void ConditionalDecomposeTest()
        {
            const string inputText = "__CHECK";
            var compositor = new StringCompositor(this.compositionProvider);
            var result = compositor.Decompose(ConditionalCompositionName, inputText);

            Assert.IsNotNull(result);
            Assert.AreEqual(result, "CHECK");
        }
    }
}
