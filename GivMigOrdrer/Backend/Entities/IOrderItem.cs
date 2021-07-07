using GivMigOrdrer.Backend.Entities.Enums;

namespace GivMigOrdrer.Backend.Entities
{
    public interface IOrderItem
    {
        /// <summary>
        /// Returns the Id of the item
        /// </summary>
        int Id { get; }
        /// <summary>
        /// Gets and sets the int value of the item's quantity
        /// </summary>
        int Quantity { get; set; }
        /// <summary>
        /// Gets and sets the name of the item
        /// </summary>
        string Name { get; set; }
        /// <summary>
        /// Gets the itemType of the item
        /// </summary>
        ItemType ItemType { get; }
        /// <summary>
        /// Adds a int value to the items quantity.
        /// </summary>
        /// <param name="quantityToAdd">int value to add</param>
        /// <returns>The item's new quantity value</returns>
        int AddToQuantity(int quantityToAdd);
    }
}
