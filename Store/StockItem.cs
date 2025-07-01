using IT_Sale.Products;

namespace IT_Sale.Store
{
    public class StockItem
    {
        public Product Product { get; set; }
        public int Quantity { get; set; }
        public double Price { get; set; }
        public StockItem(Product product, int quantity, double price)
        {
            Product = product;
            Quantity = quantity;
            Price = price;
        }
    }


}
