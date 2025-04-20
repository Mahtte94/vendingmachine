
namespace VendingMachine
{
    public class Item
    {
        public int Id { get; private set; }
        public string Name { get; private set; }
        public decimal Price { get; private set; }
        public string Description { get; private set; }
        public int Quantity { get; private set; }

        public Item(int id, string name, decimal price, string description, int quantity = 1)
        {
            Id = id;
            Name = name;
            Price = price;
            Description = description;
            Quantity = quantity;
        }

        public string GetInfo()
        {
            return $"{Id}. {Name} - ${Price:F2} - {Description} ({Quantity} available)";
        }

        public bool DecreaseQuantity()
        {
            if (Quantity > 0)
            {
                Quantity--;
                return true;
            }
            return false;
        }
    }
}