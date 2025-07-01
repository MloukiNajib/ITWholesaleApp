using ITWholesale.Basket;

namespace ITWholesale.Promotion
{
    public class PromotionEngine
    {
        public List<Promotion> Promotions { get; set; }
        public PromotionEngine()
        {
            Promotions = new List<Promotion>();
        }
        public void AddPromotion(Promotion promotion)
        {
            Promotions.Add(promotion);
        }
        public double ApplyPromotions(ShoppingCart cart)
        {
            double totalDiscount = 0;
            foreach (var promotion in Promotions)
            {
                totalDiscount += promotion.ApplyPromotion(cart);
            }
            return cart.TotalPrice - totalDiscount;
        }
    }


}
