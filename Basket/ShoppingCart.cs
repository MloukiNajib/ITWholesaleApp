using ITWholesale.Products;

namespace ITWholesale.Basket
{
    public class ShoppingCart
    {
        public List<CartItem> cartItemList { get; set; }
        public double TotalPrice => cartItemList.Sum(item => item.TotalPrice);

        public ShoppingCart()
        {
            cartItemList = new List<CartItem>();
        }

        public void AddItem(CartItem cartItem)
        {
            cartItemList.Add(cartItem);
        }
    }


}
