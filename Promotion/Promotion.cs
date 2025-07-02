using ITWholesale.Basket;

namespace ITWholesale.Promotion
{
    public class Promotion
    {
        public string Name { get; set; }
        public Func<ShoppingCart, double> ApplyPromotion { get; set; }
        public Promotion(string name, Func<ShoppingCart, double> applyPromotion)
        {
            Name = name;
            ApplyPromotion = applyPromotion;
        }
    }


}
