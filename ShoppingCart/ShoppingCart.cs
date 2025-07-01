using IT_Sale.Products;

namespace IT_Sale.ShoppingCart
{
    public class ShoppingCart
    {
        public List<CartItem> ItemList { get; set; }
        public double TotalPrice => ItemList.Sum(item => item.TotalPrice);

        public ShoppingCart()
        {
            ItemList = new List<CartItem>();
        }

        public void AddItem(CartItem cartItem)
        {
            ItemList.Add(cartItem);
        }
    }


}
