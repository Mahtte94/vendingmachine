

namespace VendingMachine
{
    class Program
    {
        static async Task Main(string[] args)
        {
            
            Inventory inventory = new Inventory();
            inventory.AddItem(new Item(1, "Soda", 1.50m, "Refreshing soft drink", 5));
            inventory.AddItem(new Item(2, "Chips", 2.00m, "Crunchy potato chips", 5));
            inventory.AddItem(new Item(3, "Chocolate", 2.50m, "Sweet chocolate bar", 5));
            inventory.AddItem(new Item(4, "Water", 1.00m, "Bottled mineral water", 5));
            inventory.AddItem(new Item(5, "Sandwich", 3.50m, "Fresh sandwich", 5));
            
            Bank bank = new Bank(5.00m); 
            User user = new User("Customer", bank);
            VendingMachine vendingMachine = new VendingMachine(inventory);

            bool running = true;
            
            DisplayMessage($"Welcome to the Fantastic Vending Machine!\nYou have ${bank.GetBalance():F2} to start.");

            while (running)
            {
                DisplayMenu();
                Console.Write("Enter your choice (1-6): ");
                string choice = Console.ReadLine();

                switch (choice)
                {
                    case "1":
                        DisplayItems(vendingMachine.DisplayItems());
                        break;

                    case "2": 
                        DisplayBalance(user.Bank.GetBalance());
                        break;

                    case "3": 
                        Console.Write("Enter amount to add: $");
                        if (decimal.TryParse(Console.ReadLine(), out decimal amount))
                        {
                            if (amount <= 0)
                            {
                                DisplayMessage("Invalid amount. Please enter a positive number.");
                            }

                            if (amount + user.Bank.GetBalance() > 1000)
                            {
                                DisplayMessage("You cannot have more than $1000 in your account.");

                            }
                            else
                            {
                                user.AddMoney(amount);
                                DisplayMessage($"Added ${amount:F2} to your balance.");
                            }
                        }
                        else
                        {
                            DisplayMessage("Invalid input. Please enter a valid number.");
                        }
                        break;

                    case "4": 
                        var items = vendingMachine.DisplayItems();
                        DisplayItems(items);
                        if (items.Count > 0)
                        {
                            Console.Write("Enter the ID of the item you want to buy: ");
                            if (int.TryParse(Console.ReadLine(), out int itemId))
                            {
                                Item item = inventory.GetItemById(itemId);

                                if (item == null)
                                {
                                    DisplayMessage("Invalid item ID.");
                                }
                                else if (item.Quantity <= 0)
                                {
                                    DisplayMessage("This item is out of stock.");
                                }
                                else if (item.Price > user.Bank.GetBalance())
                                {
                                    DisplayMessage($"Insufficient funds. You need ${item.Price:F2} but have ${user.Bank.GetBalance():F2}.");
                                }
                                else
                                {
                                    Item purchasedItem = user.BuyItem(itemId, vendingMachine);
                                    if (purchasedItem != null)
                                    {
                                        DisplayMessage($"Successfully purchased {purchasedItem.Name} for ${purchasedItem.Price:F2}.");
                                    }
                                    else
                                    {
                                        DisplayMessage("Failed to purchase the item.");
                                    }
                                }
                            }
                            else
                            {
                                DisplayMessage("Invalid input. Please enter a valid item ID.");
                            }
                        }
                        break;

                    case "5": 
                        DisplayPurchasedItems(user.GetPurchasedItems());
                        break;

                    case "6": 
                        DisplayMessage("Thank you for using the Fantastic Vending Machine!");
                        running = false;
                        break;

                    default:
                        DisplayMessage("Invalid choice. Please enter a number between 1 and 6.");
                        break;
                }
            }
        }

        static void DisplayMenu()
        {
            Console.WriteLine("\n===== FANTASTIC VENDING MACHINE =====");
            Console.WriteLine("1. View available items");
            Console.WriteLine("2. View your balance");
            Console.WriteLine("3. Add money");
            Console.WriteLine("4. Buy an item");
            Console.WriteLine("5. View purchased items");
            Console.WriteLine("6. Exit");
            Console.WriteLine("======================================\n");
        }

        static void DisplayItems(List<Item> items)
        {
            Console.WriteLine("\n===== AVAILABLE ITEMS =====");
            if (items.Count == 0)
            {
                Console.WriteLine("No items available.");
            }
            else
            {
                foreach (Item item in items)
                {
                    Console.WriteLine(item.GetInfo());
                }
            }
            Console.WriteLine("===========================\n");
        }

        static void DisplayBalance(decimal balance)
        {
            Console.WriteLine($"\nYour current balance: ${balance:F2}\n");
        }

        static void DisplayPurchasedItems(List<Item> items)
        {
            Console.WriteLine("\n===== YOUR PURCHASES =====");
            if (items.Count == 0)
            {
                Console.WriteLine("You have not purchased any items yet.");
            }
            else
            {
                for (int i = 0; i < items.Count; i++)
                {
                    Console.WriteLine($"{i + 1}. {items[i].Name} - ${items[i].Price:F2}");
                }
            }
            Console.WriteLine("==========================\n");
        }

        static void DisplayMessage(string message)
        {
            Console.WriteLine($"\n{message}\n");
        }
    }
}