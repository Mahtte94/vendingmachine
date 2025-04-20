

namespace VendingMachine
{
    public class VendingMachine
    {
        private Inventory _inventory;

        public VendingMachine(Inventory inventory)
        {
            _inventory = inventory;
        }

        public List<Item> DisplayItems()
        {
            return _inventory.GetItems();
        }

        public Item SellItem(int itemId, Bank bank)
        {
            Item item = _inventory.GetItemById(itemId);
            if (item == null || item.Quantity <= 0)
            {
                return null;
            }

            if (bank.Withdraw(item.Price))
            {
                item.DecreaseQuantity();
                
                return new Item(
                    item.Id,
                    item.Name,
                    item.Price,
                    item.Description,
                    1 
                );
            }
            
            return null;
        }
    }
}