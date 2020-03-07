using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace BeFaster.App.Solutions.CHK
{
    public static class CheckoutSolution
    {
        private static readonly IDictionary<char, int> priceList = new Dictionary<char, int>()
        {
            {'A',50 },
            {'B',30 },
            {'C',20 },
            {'D',15 }
        };
        private static readonly IDictionary<char, string> offers = new Dictionary<char, string>()
        {
            {'A', "3-130"},
            {'B',"2-45" }
        };
        private static bool IsValidItem(char item)
        {
            if (priceList.ContainsKey(item))
            {
                return true;
            }
            return false;
        }
        public static bool IsLegalInput(string skus)
        {
            var items = skus.ToCharArray().ToList();
            foreach (var item in items)
            {
                if (!priceList.ContainsKey(x)) return false;

            }
         
            return true;
        }
        public static int ComputePrice(string skus)
        {
            if (string.IsNullOrEmpty(skus)) return 0;
            if (!IsLegalInput(skus))
                return -1;
            List<char> shoppingList = skus.ToCharArray().ToList();

            var IndividualItems = shoppingList.Select(x => x).Distinct().ToList();
            var totalPrice = 0;
            IndividualItems.ForEach(item =>
            {
                if (IsValidItem(item))
                {
                    var noOfItems = shoppingList.Where(x => x == item).Count();
                    if (offers.ContainsKey(item))
                    {
                        var offer = offers[item];
                        var noInOffer = Int32.Parse(offer.Split('-')[0]);
                        var withoutOffer = noOfItems % noInOffer;
                        var withOffer = noOfItems / noInOffer;
                        var offerdPrice = Int32.Parse(offer.Split('-')[1]);
                        totalPrice = totalPrice + withOffer * offerdPrice + withOffer * priceList[item];
                    }
                    else
                    {
                        totalPrice = noOfItems * priceList[item];
                    }
                }
            });
            return totalPrice;
        }
    }
}



