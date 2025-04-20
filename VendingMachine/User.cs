

namespace VendingMachine
{
    public class User
    {
        public string Name { get; private set; }
        public Bank Bank { get; private set; }
        private List<Item> _purchasedItems;

        public User(string name, Bank bank)
        {
            Name = name;
            Bank = bank;
            _purchasedItems = new List<Item>();
        }

        public Item BuyItem(int itemId, VendingMachine vendingMachine)
        {
            Item purchasedItem = vendingMachine.SellItem(itemId, Bank);
            if (purchasedItem != null)
            {
                _purchasedItems.Add(purchasedItem);
                return purchasedItem;
            }
            return null;
        }

        public List<Item> GetPurchasedItems()
        {
            return _purchasedItems;
        }

        public bool AddMoney(decimal amount)
        {
            return Bank.Deposit(amount);
        }
    }
}