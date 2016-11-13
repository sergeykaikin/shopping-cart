using System.Collections.Generic;

namespace ShoppingCart
{
    public class ShoppingCart
    {
        private List<Product> items = new List<Product>();
        public void AddItem(Product item)
        {
            this.items.Add(item);
        }
        public decimal CalculatePrice()
        {
            return Shop.CalculatePrice(this.items);
        }
    }
}
