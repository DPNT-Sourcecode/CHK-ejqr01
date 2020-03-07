using BeFaster.App.Solutions.CHK;
using BeFaster.App.Solutions.SUM;
using NUnit.Framework;

namespace BeFaster.App.Tests.Solutions.CHK
{
    [TestFixture]
    public class CHKSolutionTest
    {
        [TestCase("A", ExpectedResult = true)]
        [TestCase("ABCD", ExpectedResult = true)]
        [TestCase("AAA", ExpectedResult = true)]
        public bool CheckIfIsLegalShoppingListAndReturnTrue(string skus)
        {
            return CheckoutSolution.IsLegalInput(skus);
        }
        [TestCase("-", ExpectedResult = false)]
        [TestCase("", ExpectedResult = false)]
        [TestCase("  ", ExpectedResult = false)]
        [TestCase("E", ExpectedResult = false)]
        public bool CheckIfIsLegalShoppingListAndReturnFalse(string skus)
        {
            return CheckoutSolution.IsLegalInput(skus);
        }
        [TestCase("A", ExpectedResult = 50)]
        public int ComputeThePriceAndReturnResult(string skus)
        {
            return CheckoutSolution.ComputePrice(skus);
        }
    }
}

