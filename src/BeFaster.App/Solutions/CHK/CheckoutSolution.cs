using BeFaster.Runner.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;

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
        private static readonly IDictionary<string, string> offers = new Dictionary<string, string>()
        {
            {"A", "3-130"},
            {"2B","2-45" }
        };
        private static bool IsValidItem(string item)
        {
            if (string.IsNullOrWhiteSpace(item)) return false;
            if (priceList.ContainsKey(item))
            {
                return true;
            }
            return false;
        }
        public static int ComputePrice(string skus)
        {
            if (string.IsNullOrEmpty(skus)) return 0;
            List<string> shoppingList = skus.Split(',').ToList();
            
            var IndividualItems = shoppingList.Select(x => x).Distinct().ToList();
            var totalPrice = 0;
            IndividualItems.ForEach(item => {
                if (IsValidItem(item))
                {
                    var noOfItems = shoppingList.Where(x => x == item).Count();
                    var offer = offers[item];
                    var noInOffer = Int32.Parse(offer.Split('-')[0]);
                    var withoutOffer = noOfItems % noInOffer;
                    var withOffer = noOfItems / noInOffer;
                    var offerdPrice = Int32.Parse(offer.Split('-')[1]);
                    totalPrice = totalPrice + withOffer * offerdPrice + withOffer * priceList[item];
                }
            });
            return totalPrice;
        }
    }
}

