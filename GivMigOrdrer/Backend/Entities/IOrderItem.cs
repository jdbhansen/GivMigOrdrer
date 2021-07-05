namespace GivMigOrdrer.Backend.Entities
{
    public interface IOrderItem
    {
        int Id { get; }
        int Quantity { get; set; }
        string Name { get; set; }
        int AddToQuantity(int quantityToAdd);
    }
}
