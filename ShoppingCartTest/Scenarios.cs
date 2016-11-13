using Microsoft.VisualStudio.TestTools.UnitTesting;
using SC = ShoppingCart;

namespace ShoppingCartTest
{
    [TestClass]
    public class Scenarios
    {
        [TestMethod, Description("Given the basket has 1 bread, 1 butter and 1 milk when I total the basket then the total should be £2.95")]
        public void JustSumEverythingUp()
        {
            var shoppingCart = new SC.ShoppingCart();
            shoppingCart.AddItem(SC.Shop.GetProduct(typeof(SC.Products.Bread)));
            shoppingCart.AddItem(SC.Shop.GetProduct(typeof(SC.Products.Butter)));
            shoppingCart.AddItem(SC.Shop.GetProduct(typeof(SC.Products.Milk)));

            Assert.AreEqual(shoppingCart.CalculatePrice(), 2.95m);
        }

        [TestMethod, Description("Given the basket has 2 butter and 2 bread when I total the basket then the total should be £3.10")]
        public void Get50PercentDiscountForOneBread()
        {
            var shoppingCart = new SC.ShoppingCart();
            shoppingCart.AddItem(SC.Shop.GetProduct(typeof(SC.Products.Bread)));
            shoppingCart.AddItem(SC.Shop.GetProduct(typeof(SC.Products.Bread)));
            shoppingCart.AddItem(SC.Shop.GetProduct(typeof(SC.Products.Butter)));
            shoppingCart.AddItem(SC.Shop.GetProduct(typeof(SC.Products.Butter)));

            Assert.AreEqual(shoppingCart.CalculatePrice(), 3.10m);
        }

        [TestMethod, Description("Given the basket has 4 milk when I total the basket then the total should be £3.45")]
        public void GetOneOfFourMilkPacksForFree()
        {
            var shoppingCart = new SC.ShoppingCart();
            shoppingCart.AddItem(SC.Shop.GetProduct(typeof(SC.Products.Milk)));
            shoppingCart.AddItem(SC.Shop.GetProduct(typeof(SC.Products.Milk)));
            shoppingCart.AddItem(SC.Shop.GetProduct(typeof(SC.Products.Milk)));
            shoppingCart.AddItem(SC.Shop.GetProduct(typeof(SC.Products.Milk)));

            Assert.AreEqual(shoppingCart.CalculatePrice(), 3.45m);
        }

        [TestMethod, Description("Given the basket has 2 butter, 1 bread and 8 milk when I total the basket then the total should be £9.00")]
        public void Get50PercertsOffBreadAnd2PacksOfMilkForFree()
        {
            var shoppingCart = new SC.ShoppingCart();
            shoppingCart.AddItem(SC.Shop.GetProduct(typeof(SC.Products.Butter)));
            shoppingCart.AddItem(SC.Shop.GetProduct(typeof(SC.Products.Butter)));
            shoppingCart.AddItem(SC.Shop.GetProduct(typeof(SC.Products.Bread)));
            shoppingCart.AddItem(SC.Shop.GetProduct(typeof(SC.Products.Milk)));
            shoppingCart.AddItem(SC.Shop.GetProduct(typeof(SC.Products.Milk)));
            shoppingCart.AddItem(SC.Shop.GetProduct(typeof(SC.Products.Milk)));
            shoppingCart.AddItem(SC.Shop.GetProduct(typeof(SC.Products.Milk)));
            shoppingCart.AddItem(SC.Shop.GetProduct(typeof(SC.Products.Milk)));
            shoppingCart.AddItem(SC.Shop.GetProduct(typeof(SC.Products.Milk)));
            shoppingCart.AddItem(SC.Shop.GetProduct(typeof(SC.Products.Milk)));
            shoppingCart.AddItem(SC.Shop.GetProduct(typeof(SC.Products.Milk)));

            Assert.AreEqual(shoppingCart.CalculatePrice(), 9.00m);
        }
    }
}
