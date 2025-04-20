

namespace VendingMachine
{
    public class Inventory
    {
        private List<Item> _items;

        public Inventory()
        {
            _items = new List<Item>();
        }

        public void AddItem(Item item)
        {
            var existingItem = GetItemById(item.Id);
            if (existingItem != null)
            {
                _items.Remove(existingItem);
                _items.Add(new Item(
                    existingItem.Id, 
                    existingItem.Name, 
                    existingItem.Price, 
                    existingItem.Description, 
                    existingItem.Quantity + item.Quantity));
            }
            else
            {
                _items.Add(item);
            }
        }

        public List<Item> GetItems()
        {
            return _items.Where(item => item.Quantity > 0).ToList();
        }

        public Item GetItemById(int itemId)
        {
            return _items.FirstOrDefault(item => item.Id == itemId);
        }
    }
}