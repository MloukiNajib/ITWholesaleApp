using ITWholesale.Basket;

namespace ITWholesale.Store
{
    public class Stock
    {
        // public List<Product> Products { get; set; }
        public List<StockItem> StockItems { get; set; }
        public Stock()
        {
            StockItems = new List<StockItem>();
        }
        public void AddStockItem(StockItem stockItem)
        {
            StockItems.Add(stockItem);
        }

        // display stock items
        public void DisplayStock(String message)
        {
            Console.WriteLine($" {message} ");
            foreach (var item in StockItems)
            {
                Console.WriteLine($"Product: {item.Product.ProductType}, Description: {item.Product.Description}, Quantity: {item.Quantity}, Price: {item.Price}");
            }
        }

        // Update stock after purchase
        public void UpdateStock(ShoppingCart cart)
        {
            foreach (var item in cart.cartItemList)
            {
                var stockItem = StockItems.FirstOrDefault(si => si.Product.ProductType == item.Product.ProductType);
                if (stockItem != null)
                {
                    stockItem.Quantity -= item.Quantity;
                }
            }
        }

        // get total revenue
        public double GetTotalRevenue()
        {
            return StockItems.Sum(item => (item.InitialQuantity - item.Quantity) * item.Price);          
        }

    }


}
