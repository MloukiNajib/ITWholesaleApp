// See https://aka.ms/new-console-template for more information

using ITWholesale.Products;
using ITWholesale.Basket;
using ITWholesale.Store;
using ITWholesale.Promotion;

public class Program
{
    public static void Main(string[] args)
    {


        // Initialisation des produits
        var switchProduct = new Product { ProductType = "Switch", Description = "Console de jeu" };
        var playstationProduct = new Product { ProductType = "Playstation", Description = "Console de jeu" };
        var pcBureauProduct = new Product { ProductType = "PC Bureautique", Description = "Ordinateur portable bureautique" };
        var pcGamerProduct = new Product { ProductType = "PC Gamer", Description = "Ordinateur portable gamer" };

        // Initialisation du l'inventaire
        var switchStock = new StockItem(switchProduct, 50, 200);
        var playstationStock = new StockItem(playstationProduct, 100, 500);
        var pcBureauStock = new StockItem(pcBureauProduct, 10, 600);
        var pcGamerStock = new StockItem(pcGamerProduct, 5, 1500);

        // Initialisation du Stock
        Stock stock = new Stock();
        stock.AddStockItem(switchStock);
        stock.AddStockItem(playstationStock);
        stock.AddStockItem(pcBureauStock);
        stock.AddStockItem(pcGamerStock);

        // definition des promotions 
        var promotion1 = new Promotion("1 acheté = 1 offert", cart =>
        {
            double discount = 0;
            foreach (var item in cart.cartItemList)
            {
                int freeItemsQuantity = item.Quantity / 2;
                discount += freeItemsQuantity * item.UnitPrice;
            }
            return discount;
        });
        var promotion2 = new Promotion("10€ de réduction tous les 250€", cart =>
        {
            double discount = 0;
            if (cart.TotalPrice >= 250)
            {
                discount = Math.Floor(cart.TotalPrice / 250) * 10;
            }
            return discount;
        });
        var promotion3 = new Promotion("2 PC Gamer achetés = 1 Switch offerte", cart =>
        {
            double discount = 0;

            var pcGamerCount = cart.cartItemList.Where(item => item.Product.ProductType == "PC gamer").Sum(item => item.Quantity);
            if (pcGamerCount >= 2)
            {
                var switchOfferts = pcGamerCount / 2;
                var switchItem = cart.cartItemList.FirstOrDefault(item => item.Product.ProductType == "Switch");
                if (switchItem != null)
                {
                    discount += switchOfferts * switchItem.UnitPrice;
                }
            }
            return discount;
        });
        var promotion4 = new Promotion("20% sur les consoles de jeu", cart =>
        {
            double discount = 0;
            foreach (var item in cart.cartItemList)
            {
                if (item.Product.ProductType == "Switch" || item.Product.ProductType == "Playstation")
                {
                    discount += item.Quantity * item.UnitPrice * 0.20; // 20% de réduction
                }
            }
            return discount;
        });


        // Simulation des 3 jours
        for (int day = 1; day <= 3; day++)
        {
            Console.WriteLine($"\n=== JOUR {day} ===");

            // Afficher l'état du stock en début de journée
            stock.DisplayStock($"État du stock en début de journée:{day}");
            Console.WriteLine($"Chiffre d'affaires cumulé: {stock.GetTotalRevenue()} €");

            // Créer le panier pour la journée
            var cart = new ShoppingCart();
            cart.AddItem(new CartItem(switchProduct, 5, 200));
            cart.AddItem(new CartItem(playstationProduct, 15, 500));
            cart.AddItem(new CartItem(pcBureauProduct, 3, 600));
            cart.AddItem(new CartItem(pcGamerProduct, 2, 1500));

            // Afficher le montant du panier
            Console.WriteLine($"\nMontant du panier avant promotions: {cart.TotalPrice} €");

            // Appliquer les promotions selon le jour
            var promotionEngine = new PromotionEngine();
            double discountedPrice = 0;

            switch (day)
            {
                case 1:
                    promotionEngine.AddPromotion(promotion1);
                    discountedPrice = promotionEngine.ApplyPromotions(cart);
                    break;
                case 2:
                    promotionEngine.AddPromotion(promotion2);
                    promotionEngine.AddPromotion(promotion3);
                    discountedPrice = promotionEngine.ApplyPromotions(cart);
                    break;
                case 3:
                    promotionEngine.AddPromotion(promotion4);
                    discountedPrice = promotionEngine.ApplyPromotions(cart);
                    break;
            }

            Console.WriteLine($"Prix remisé jour {day}: {discountedPrice} €");

            // Mettre à jour le stock
            stock.UpdateStock(cart);

            // Afficher l'état du stock en fin de journée
            stock.DisplayStock($"\nÉtat du stock en fin de journée:{day}");
            Console.WriteLine($"Chiffre d'affaires cumulé: {stock.GetTotalRevenue()} €");
        }



        Console.WriteLine("Fin du programme.");


    }
}
