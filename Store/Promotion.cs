using IT_Sale.ShoppingCart;

namespace IT_Sale.Store
{
    public class Promotion
    {
        public string Name { get; set; }
        public Func<ShoppingCart.ShoppingCart, double> ApplyPromotion { get; set; }
    }


}
