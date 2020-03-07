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
            {'D',15 },
            {'E',40 }
        };
        private static readonly IDictionary<char, string> offers = new Dictionary<char, string>()
        {
            {'A', "3-130 ,5-200"},
            {'B',"2-45" },
            {'E', "2-1*B" },
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
            if (string.IsNullOrWhiteSpace(skus)) return false;
            var items = skus.ToCharArray().ToList();
            foreach (var item in items)
            {
                if (!priceList.ContainsKey(item)) return false;

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
            var discount = 0;

            IndividualItems.ForEach(item =>
            {
                if (IsValidItem(item))
                {
                    var noOfItems = shoppingList.Where(x => x == item).Count();
                    if (offers.ContainsKey(item))
                    {
                        var offer = offers[item].Split(',');
                        var noInOffer = 0;
                        var withoutOffer = 0;
                        var withOffer = 0;
                        var offerdPrice = 0;
                        var itemTotalPrice = Int32.MaxValue;
                        foreach (var o in offer)
                        {
                            noInOffer = Int32.Parse(o.Split('-')[0]);
                            withoutOffer = noOfItems % noInOffer;
                            withOffer = noOfItems / noInOffer;
                            if(o.Split('*').Count()< 2) //Not offering other sku and only offers in price
                                offerdPrice = Int32.Parse(o.Split('-')[1]);
                            else
                            {
                                discount += withOffer * priceList[o.Split('*')[1].ToCharArray()[0]];
                            }
                            var total = withOffer * offerdPrice + withoutOffer * priceList[item];
                            total = total <= itemTotalPrice ? totalPrice : itemTotalPrice;
                        }

                        totalPrice += itemTotalPrice;
                    }
                    else
                    {
                        totalPrice += noOfItems * priceList[item];
                    }
                }
            });
            return totalPrice-discount;
        }
    }
}

