using System;
using System.Collections.Generic;

namespace BeFaster.App.Solutions.CHK
{
    class PriceOffer
    {
        public string Sku { get; set; }
        public int MinQty { get; set; }
        public int OfferedPrice { get; set; }
    }
    class FreeProduct
    {
        public string Sku { get; set; }
        public int MinQty { get; set; }
        public string OfferedProduct { get; set; }
        public int OfferQty { get; set; }
    }
    class Product
    {
        public Product(string sku, int price)
        {
            Sku = sku;
            Price = price;
        }

        public string Sku { get; set; }
        public int Price { get; set; }
    }
    class BasketItem
    {
        public BasketItem(string sku, int qty, int unitPrice)
        {
            Sku = sku;
            Qty = qty;
            UnitPrice = unitPrice;
            SubTotal = unitPrice * qty;
        }

        public string Sku { get; set; }
        public int Qty { get; set; }
        public int UnitPrice { get; set; }
        public int SubTotal { get; set; }
        public int Discount { get; set; }
        public int Total { get; set; }
    }
    public static class CheckoutSolution
    {
        static List<PriceOffer> priceOffers = new List<PriceOffer>
        {
            {new PriceOffer(){Sku="A",MinQty=5,OfferedPrice=200} },
            {new PriceOffer(){Sku="A",MinQty=3,OfferedPrice=130} },
            {new PriceOffer(){Sku="B",MinQty=2,OfferedPrice=45} },
            {new PriceOffer(){Sku="F",MinQty=3,OfferedPrice=20} }

        };
        static List<FreeProduct> freeProductsOffer = new List<FreeProduct>()
        {
            {new FreeProduct(){Sku="E",MinQty=2, OfferedProduct="B", OfferQty=1} }
        };
        static List<Product> priceList = new List<Product>()
        {
            {new Product("A",50)},
            {new Product("B",30) },
            {new Product("C",20) },
            {new Product("D",15) },
            {new Product("E",40) },
            {new Product("F",10) }
        };

        static void ApplyOffers()
        {
            foreach (var item in shoppingList)
            {
                var offer = freeProductsOffer.Find(x => x.Sku == item.Sku);
                var withOffer = 0;
                if (offer != null)
                {
                    withOffer = item.Qty / offer.MinQty;
                    var applied = 0;
                    if (shoppingList.Find(x => x.Sku == offer.OfferedProduct) != null)
                    {
                        applied = Math.Min(withOffer, shoppingList.Find(x => x.Sku == offer.OfferedProduct).Qty);
                        var temp = shoppingList.Find(x => x.Sku == offer.OfferedProduct);
                        temp.Qty -= applied;
                        if (temp.Discount > 0)
                        {
                            temp.Discount = 0;
                            PriceReductionOffer(temp);
                        }
                    }
                };
                 PriceReductionOffer(item);

            }
        }

        private static void PriceReductionOffer(BasketItem item)
        {
            var discountOffers = priceOffers.FindAll(x => x.Sku == item.Sku);
            var qty = item.Qty;
            foreach (var discountOffer in discountOffers)
            {
                var withOffer = qty / discountOffer.MinQty;
                var discount = withOffer * discountOffer.MinQty * item.UnitPrice - withOffer * discountOffer.OfferedPrice;
                item.Discount += discount;
                qty -= withOffer * discountOffer.MinQty;
                if (qty == 0) break;
            }

        }

        public static int ComputePrice(string skus)
        {
            if (string.IsNullOrWhiteSpace(skus)) return 0;

            if (!isValidSkus(skus)) return -1;
            shoppingList = new List<BasketItem>();
            PrepareShoppingList(skus);
            ApplyOffers();

            var total = 0;

            foreach (var item in shoppingList)
            {
                total += item.Qty * item.UnitPrice - item.Discount;
            }
            return total;
        }

        private static void PrepareShoppingList(string skus)
        {
            foreach (var item in skus)
            {
                if (shoppingList.Find(x => x.Sku == item.ToString()) == null)
                {
                    shoppingList.Add(new BasketItem(
                        item.ToString(),
                        skus.Split(item).Length - 1,
                        priceList.Find(x => x.Sku == item.ToString()).Price
                        ));
                }
            }
        }
        static List<BasketItem> shoppingList = new List<BasketItem>();
        public static bool isValidSkus(string skus)
        {
            if (string.IsNullOrWhiteSpace(skus)) return false;
            foreach (var item in skus)
            {
                if (priceList.FindAll(x => x.Sku == item.ToString()).Count < 1) return false;
            }
            return true;
        }
    }
}
