using BeFaster.Runner.Exceptions;
using System.Collections.Generic;

namespace BeFaster.App.Solutions.CHK
{
    public static class CheckoutSolution
    {
        private static readonly IDictionary<string, int> priceList = new Dictionary<string, int>()
        {
            {"A",50 },
            {"B",30 },
            {"C",20 },
            {"D",15 }
        };
        private static readonly IDictionary<string, int> offers = new Dictionary<string, int>()
        {
            {"3A", 130},
            {"2B",45 }
        };
        public static int ComputePrice(string skus)
        {
            throw new SolutionNotImplementedException();
        }
    }
}
A    | 50    | 3A for 130     |
| B    | 30    | 2B for 45      |
| C    | 20    |                |
| D    | 15 