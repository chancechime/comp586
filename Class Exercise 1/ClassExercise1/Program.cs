public interface Nameable
{
    string GetName();
    void SetName(string name);
}

public abstract class Shop : Nameable
{
    public abstract void CalculateSaleTax();
    public abstract string[] GetInventory();
    public abstract void BuyInventory();
    public abstract string GetName();
    public abstract void SetName(string name);
}

public class DonutShop : Shop
{
    public string CompanyName { get; set; } = string.Empty;
    public string[] MenuItems { get; set; } = Array.Empty<string>();

    public override void CalculateSaleTax()
    {
        Console.WriteLine("Sales Tax has been calculated");
    }

    public override string[] GetInventory()
    {
        return MenuItems;
    }

    public override void BuyInventory()
    {
        Console.WriteLine("Inventory for Donut Shop has been purchased" + Environment.NewLine);
    }

    public override string GetName()
    {
        return CompanyName;
    }

    public override void SetName(string name)
    {
        CompanyName = name;
    }
}

public class PizzaShop : Shop
{
    public string CompanyName { get; set; } = string.Empty;
    public string[] FoodOfferings { get; set; } = Array.Empty<string>();

    public override void CalculateSaleTax()
    {
        Console.WriteLine("Sales Tax has been calculated");
    }

    public override string[] GetInventory()
    {
        return FoodOfferings;
    }

    public override void BuyInventory()
    {
        Console.WriteLine("Inventory for Pizza Shop has been purchased" + Environment.NewLine);
    }

    public override string GetName()
    {
        return CompanyName;
    }

    public override void SetName(string name)
    {
        CompanyName = name;
    }
}

public class CustList
{
    public string Name { get; set; } = string.Empty;
    public List<Shop> Shops {get; set;} = new List<Shop>();
    public string FindCust()
    {
        return "Customer Found";
    }

    public void AddCust(Shop shop)
    {
        Shops.Add(shop);
        Console.WriteLine("Customer Added to Shop");
    }
}

public class Program
{
    public static void Main(string[] args)
    {
        DonutShop donutShop = new DonutShop
        {
            MenuItems = new string[] { "Glazed", "Chocolate", "Sprinkles" }
        };
        donutShop.SetName("Delicious Donuts");
        donutShop.CalculateSaleTax();
        donutShop.BuyInventory();

        PizzaShop pizzaShop = new PizzaShop
        {
            FoodOfferings = new string[] { "Pepperoni", "Vodka", "Veggie Supreme" }
        };
        pizzaShop.SetName("Pizza Paradise");
        pizzaShop.CalculateSaleTax();
        pizzaShop.BuyInventory();

        CustList customer = new CustList
        {
            Name = "Bella Baker"
        };

        Console.WriteLine($"Donut Shop: {donutShop.GetName()}");
        Console.WriteLine("Menu Items: " + string.Join(", ", donutShop.GetInventory()) + Environment.NewLine);
        Console.WriteLine($"Pizza Shop: {pizzaShop.GetName()}");
        Console.WriteLine("Food Offerings: " + string.Join(", ", pizzaShop.GetInventory()) + Environment.NewLine);
        Console.WriteLine($"Customer: {customer.Name}" + Environment.NewLine);
    }
}