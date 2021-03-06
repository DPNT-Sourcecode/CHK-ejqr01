﻿using BeFaster.App.Solutions.CHK;
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
            return CheckoutSolution.isValidSkus(skus);
        }
        [TestCase("-", ExpectedResult = false)]
        [TestCase("", ExpectedResult = false)]
        [TestCase("  ", ExpectedResult = false)]
        [TestCase("F", ExpectedResult = false)]
        public bool CheckIfIsLegalShoppingListAndReturnFalse(string skus)
        {
            return CheckoutSolution.isValidSkus(skus);
        }
        [TestCase("A", ExpectedResult = 50)]
        [TestCase("AA", ExpectedResult = 100)]
        [TestCase("AAA", ExpectedResult = 130)]
        [TestCase("ABCD", ExpectedResult = 115)]
        [TestCase("ABCDABCD", ExpectedResult = 215)]
        [TestCase("ABCDEABCDE", ExpectedResult = 280)]
        [TestCase("EEB", ExpectedResult = 80)]
        [TestCase("EEEEB", ExpectedResult = 160)]
        [TestCase("BEBEEE", ExpectedResult = 160)]
        [TestCase("AAAAA", ExpectedResult = 200)]
        [TestCase("AAAAAA", ExpectedResult = 250)]
        [TestCase("AAAAAAA", ExpectedResult = 300)]
        [TestCase("ABCDECBAABCABBAAAEEAA", ExpectedResult = 665)]
        [TestCase("AAAAAEEBAAABB", ExpectedResult = 455)]
        [TestCase("AAAAAEEBAAABBFFF", ExpectedResult = 475)]
        [TestCase("FFF", ExpectedResult = 20)]
        [TestCase("FF", ExpectedResult = 20)]
        [TestCase("HHHHH", ExpectedResult = 45)]
        [TestCase("HHHH", ExpectedResult = 40)]
        [TestCase("HHHHHHHHHH", ExpectedResult = 80)]
        [TestCase("NNNMM", ExpectedResult = 135)] 
        [TestCase("UUUUPPPPP", ExpectedResult = 320)]
        [TestCase("RRRQRRRQ", ExpectedResult = 300)]
        [TestCase("VVVVV", ExpectedResult = 220)]
        [TestCase("STX", ExpectedResult = 45)]
        [TestCase("STXYZ", ExpectedResult = 82)]
        [TestCase("XYZ", ExpectedResult = 45)]
        [TestCase("SSSZ", ExpectedResult = 65)]
        [TestCase("ABCDEFGHIJKLMNOPQRSTUVWXYZABCDEFGHIJKLMNOPQRSTUVWXYZ", ExpectedResult = 1602)]
        [TestCase("LGCKAQXFOSKZGIWHNRNDITVBUUEOZXPYAVFDEPTBMQLYJRSMJCWH", ExpectedResult = 1602)]
        public int ComputeThePriceAndReturnResult(string skus)
        {
            return CheckoutSolution.ComputePrice(skus);
        }
    }
}
