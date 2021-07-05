using GivMigOrdrer.Backend.Entities.Enums;
using System.Collections.Generic;

namespace GivMigOrdrer.Backend.Entities
{
    public interface IOrderEntity<T> where T : IOrderItem
    {
        string Id { get; }
        CourierType Courier { get; }
        List<T> GetItems();
        int AddItemToOrder(T item);
        /// <summary>
        /// Gets the first item in the list with the highest quantity
        /// </summary>
        /// <returns></returns>
        T GetItemWithGreatestQuantity();
        T GetItemWithLowestId();
    }
}
