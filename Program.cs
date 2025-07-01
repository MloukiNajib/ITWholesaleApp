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

        // 1. ajouter des produits au panier : 5 switch, 15 playstation, 3 pc bureautique, 2 pc gamer

        ShoppingCart cart = new ShoppingCart();
        cart.AddItem(new CartItem(switchProduct, 5, switchStock.Price));
        cart.AddItem(new CartItem(playstationProduct, 15, playstationStock.Price));
        cart.AddItem(new CartItem(pcBureauProduct, 3, pcBureauStock.Price));
        cart.AddItem(new CartItem(pcGamerProduct, 2, pcGamerStock.Price));

        // 2. afficher le montant du panier
        Console.WriteLine($"Montant du panier avant promotions : {cart.TotalPrice} €");

        // 3. appliquer les promotions du jour et afficher le prix remisé :
        //  jour 1 : promo 1 = on paye 3 switch, 8 playstation, 2 pc b et 1 pc
        // défenir les promotions
        PromotionEngine promotionEngine = new PromotionEngine();
        promotionEngine.AddPromotion(promotion1);
        double day1Price = promotionEngine.ApplyPromotions(cart);
        Console.WriteLine($"Prix remisé jour 1 : {day1Price} €");

        //  jour 2 : promos 2+3
        // Réinitialiser le panier pour le jour 2
        cart = new ShoppingCart();
        cart.AddItem(new CartItem(switchProduct, 5, switchStock.Price));
        cart.AddItem(new CartItem(playstationProduct, 15, playstationStock.Price));
        cart.AddItem(new CartItem(pcBureauProduct, 3, pcBureauStock.Price));
        cart.AddItem(new CartItem(pcGamerProduct, 2, pcGamerStock.Price));
        promotionEngine = new PromotionEngine();
        promotionEngine.AddPromotion(promotion2);
        promotionEngine.AddPromotion(promotion3);
        double day2Price = promotionEngine.ApplyPromotions(cart);
        Console.WriteLine($"Prix remisé jour 2 : {day2Price} €");

        //  jour 3 : promo 4
        // Réinitialiser le panier pour le jour 3
        cart = new ShoppingCart();
        cart.AddItem(new CartItem(switchProduct, 5, switchStock.Price));
        cart.AddItem(new CartItem(playstationProduct, 15, playstationStock.Price));
        cart.AddItem(new CartItem(pcBureauProduct, 3, pcBureauStock.Price));
        cart.AddItem(new CartItem(pcGamerProduct, 2, pcGamerStock.Price));
        promotionEngine = new PromotionEngine();
        promotionEngine.AddPromotion(promotion4);
        double day3Price = promotionEngine.ApplyPromotions(cart);
        Console.WriteLine($"Prix remisé jour 3 : {day3Price} €");

        // 4. afficher l'état du stock et le chiffre d’affaires en fin de chaque journée
        double totalRevenue = stock.StockItems.Sum(item => item.Quantity * item.Price);
        Console.WriteLine("État du stock :");
        foreach (var stockItem in stock.StockItems)
        {
            Console.WriteLine($"{stockItem.Product.ProductType} - Quantité: {stockItem.Quantity}, Prix unitaire: {stockItem.Price} €");
        }
        Console.WriteLine($"Chiffre d'affaires total : {totalRevenue} €");
        Console.WriteLine("Fin du programme.");


    }
}
