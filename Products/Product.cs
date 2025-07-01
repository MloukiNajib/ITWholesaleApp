using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IT_Sale.Products
{
    /**
     * Produits :
        - console de jeu (50 switch 200€, 100 playstation 500€)
        - pc portable (10 bureautique 600€, 5 gamer 1500€)
     */
    public class Product
    {
        public String productType { get; set; }
        public string Name { get; set; }
        public String Description { get; set; }

        // public int Quantity { get; set; }
        // public double TotalPrice { get; set; }

    }


}
