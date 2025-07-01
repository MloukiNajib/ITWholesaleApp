using System.Security.Cryptography.X509Certificates;
using ITWholesale.Products;

namespace ITWholesale.Basket
{
    public class CartItem 
    {
        public Product Product { get; set; }
        public int Quantity { get; set; }
        public double UnitPrice { get; set; }
        public double TotalPrice => UnitPrice * Quantity;


        public CartItem(Product product, int quantity, double unitprice)
        {
            this.Product = product;
            this.Quantity = quantity;
            this.UnitPrice = unitprice;
        }

  

    }


}
