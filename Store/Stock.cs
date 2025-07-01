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

        //public void UpdateStock(string productName, int quantity)
        //{
        //    var product = Products.FirstOrDefault(p => p.ProductType == productName);
        //    if (product != null)
        //    {
        //        product.Quantity += quantity;
        //    }
        //}
        //public double GetTotalRevenue()
        //{
        //    return Products.Sum(p => p.TotalPrice * (p.Quantity - p.Quantity));
        //}
    }


}
