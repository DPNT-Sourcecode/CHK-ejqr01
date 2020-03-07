using BeFaster.App.Solutions.CHK;
using BeFaster.App.Solutions.SUM;
using NUnit.Framework;

namespace BeFaster.App.Tests.Solutions.CHK
{
    [TestFixture]
    public class CHKSolutionTest
    {
        [TestCase("A", ExpectedResult = true)]
        public bool CheckIfIsLegalShoppingList(string skus)
        {
            return CheckoutSolution.IsLegalInput(skus);
        }
    }
}

