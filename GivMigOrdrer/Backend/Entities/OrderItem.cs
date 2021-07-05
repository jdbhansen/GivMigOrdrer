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

        public OrderItem(int id, int quantity, string name = "")
        {
            Id = id;
            Quantity = quantity;
            Name = name;
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
    }
}
