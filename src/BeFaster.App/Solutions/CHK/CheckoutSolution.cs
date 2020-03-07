using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace BeFaster.App.Solutions.CHK
{
    public static class CheckoutSolution
    {
        private static IDictionary<char, int> totalPricePerItem = new Dictionary<char, int>();

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
            totalPricePerItem = new Dictionary<char, int>();
        
            List<char> shoppingList = skus.ToCharArray().ToList();

            var IndividualItems = shoppingList.Select(x => x).Distinct().ToList();
            var totalPrice = 0;
            var discount = 0;

            IndividualItems.ForEach(item =>
            {
            if (IsValidItem(item))
            {
                var noOfItems = shoppingList.Where(x => x == item).Count();
                if(totalPricePerItem.ContainsKey(item))
                {
                    var temp = totalPricePerItem[item] + GetTotalPrice(item, noOfItems, shoppingList);
                    totalPricePerItem[item] = Math.Max(totalPricePerItem[item], 0);
                }
                else
                    totalPricePerItem.Add(item, GetTotalPrice(item, noOfItems, shoppingList));
            }
            });


            return totalPrice-discount;
        }

        private static int GetTotalPrice(char sku, int noOfItems, List<char> shoppingList)
        {
            if(!offers.ContainsKey(sku))
                return noOfItems * priceList[sku];
            var offer = offers[sku].Split(',');
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
                var total = 0;
                if (o.Split('*').Count() < 2) //Not offering other sku and only offers in price
                {
                    offerdPrice = Int32.Parse(o.Split('-')[1]);
                    total = withOffer * offerdPrice + withoutOffer * priceList[sku];
                }
                else
                {
                    withOffer = Math.Min(withOffer, shoppingList.Count(x => x == o.Split('*')[1].ToCharArray()[0]));
                    total = noOfItems * priceList[sku];
                    if (totalPricePerItem.ContainsKey(sku))
                    {
                        totalPricePerItem[o.Split('*')[1].ToCharArray()[1]] = totalPricePerItem[o.Split('*')[1].ToCharArray()[1]] -totalPricePerItem[sku];
                    }
                    else
                        totalPricePerItem.Add(sku, GetTotalPrice(sku, noOfItems, shoppingList));
                }

                itemTotalPrice = total <= itemTotalPrice ? total : itemTotalPrice;
            }
            return itemTotalPrice;
        }
    }
}
