using GivMigOrdrer.Backend.Entities.Enums;

namespace GivMigOrdrer.Backend.Entities
{
    public class OrderItem : IOrderItem
    {
        private int _quantity;

        public int Quantity
        {
            get => _quantity;
            set
            {
                if (value > 0)
                {
                    _quantity = value;
                }
            }
        }

        public int Id { get; }

        public string Name { get; set; }

        public ItemType ItemType { get; }

        public OrderItem(int id, int quantity, string name = "")
        {
            Id = id;
            Quantity = quantity;
            Name = name;
            ItemType = ItemTypeSelector(id);
        }

        public int AddToQuantity(int quantityToAdd)
        {
            if (quantityToAdd > 0)
            {
                _quantity += quantityToAdd;
            }
            return Quantity;
        }

        public override string ToString()
        {
            return $"{Id}\t{Quantity}\t{Name}\n";
        }

        private static ItemType ItemTypeSelector(int itemId)
        {
            if (itemId < 61000)
            {
                return ItemType.Accessory;
            }
            if (itemId < 62000)
            {
                return ItemType.Headset;
            }
            if (itemId < 63000)
            {
                return ItemType.Mouse;
            }
            if (itemId < 64000)
            {
                return ItemType.Mousepad;
            }
            if (itemId < 65000)
            {
                return ItemType.Keyboard;
            }
            return ItemType.Misc;
        }
    }
}