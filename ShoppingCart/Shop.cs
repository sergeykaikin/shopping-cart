using System;
using System.Collections.Generic;
using System.Linq;

namespace ShoppingCart
{
    /* The idea was that all products informtion is owned by Shop */
    public static class Shop
    {
        /* Product/Price mappings */
        public static Dictionary<Type, decimal> AvailableProducts = new Dictionary<Type, decimal>() {
            {typeof(Products.Milk), 1.15m},
            {typeof(Products.Butter), 0.8m},
            {typeof (Products.Bread), 1m}
        };
        /* For each product there may be a rule which takes into account items from ShoppingCart and calculates a discounted price */
        public static Dictionary<Type, Func<List<Product>, decimal>> DiscountRules = new Dictionary<Type, Func<List<Product>, decimal>>() {
            {typeof(Products.Milk), procucts => {
                var milkPacks = procucts.Where(p => p.GetType() == typeof(Products.Milk)).ToList();
                var count = milkPacks.Count();

                if (count > 0) {
                    var price = milkPacks[0].Price;
                    var discountedPacksCount = Math.Floor(count / 4.0m);

                    return (discountedPacksCount * (3 * price)) + ((count - (discountedPacksCount * 4)) * price);
                } else {
                    return 0m;
                }
            }},
            {typeof(Products.Bread), procucts => {
                var loavesOfBread = procucts.Where(p => p.GetType() == typeof(Products.Bread)).ToList();
                var loavesCount = loavesOfBread.Count();

                if (loavesCount > 0) {
                    var price = loavesOfBread[0].Price;
                    var butterPacks = procucts.Where(p => p.GetType() == typeof(Products.Butter)).ToList();
                    var discountedPacksCount = Math.Floor(butterPacks.Count() / 2.0m);
                    var sum = 0m;

                    while (loavesCount > 0) {
                        if (discountedPacksCount > 0) {
                            discountedPacksCount--;
                            sum += (price * 0.5m);
                        } else {
                            sum += price;
                        }

                        loavesCount--;
                    }

                    return sum;
                } else {
                    return 0m;
                }
            }}
        };
        /* Factory method which will return a Product with its actual price */
        public static Product GetProduct(Type product)
        {
            decimal price;

            if (Shop.AvailableProducts.TryGetValue(product, out price))
            {
                return (Product)Activator.CreateInstance(product, new Object[] { price });
            }
            else {
                throw new ArgumentOutOfRangeException("Product does not exist ");
            }
        }
        /* This one actually does a calculation depending on a Product type it uses a discount rule (it is exists) or just sums Products' prices */
        public static decimal CalculatePrice(List<Product> items)
        {
            decimal sum = 0m;

            foreach (Type product in Shop.AvailableProducts.Keys)
            {
                Func<List<Product>, decimal> discountRule;

                if (Shop.DiscountRules.TryGetValue(product, out discountRule))
                {
                    sum += discountRule(items);
                }
                else {
                    var products = items.Where(p => p.GetType() == product).ToList();
                    var count = products.Count();

                    if (count > 0)
                    {
                        sum += count * products[0].Price;
                    }
                }
            }

            return sum;
        }
    }
}
