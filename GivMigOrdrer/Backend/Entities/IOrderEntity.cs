using GivMigOrdrer.Backend.Entities.Enums;
using System.Collections.Generic;

namespace GivMigOrdrer.Backend.Entities
{
    public interface IOrderEntity<T> where T : IOrderItem
    {
        /// <summary>
        /// Gets the OrderEntity's id.
        /// </summary>
        string Id { get; }
        /// <summary>
        /// Gets the Orderentity's courier type
        /// </summary>
        CourierType Courier { get; }
        /// <summary>
        /// gets the orderentity's items
        /// </summary>
        /// <returns>List of items</returns>
        List<T> GetItems();
        /// <summary>
        /// adds an item to the orderentity
        /// </summary>
        /// <param name="item">item to add</param>
        /// <returns>the count of current items</returns>
        int AddItemToOrder(T item);
        /// <summary>
        /// Gets the first item in the list with the highest quantity
        /// </summary>
        /// <returns></returns>
        T GetItemWithGreatestQuantity();
        /// <summary>
        /// gets the item with lowest id
        /// </summary>
        /// <returns></returns>
        T GetItemWithLowestId();
    }
}
