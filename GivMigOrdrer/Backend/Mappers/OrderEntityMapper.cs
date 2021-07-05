using GivMigOrdrer.Backend.Entities;
using System.Collections.Generic;

namespace GivMigOrdrer.Backend.Mappers
{
    public class OrderEntityMapper : IOrderEntityMapper<IOrderEntity<IOrderItem>>
    {
        private static readonly Dictionary<string, IOrderEntity<IOrderItem>> _orderDict = new Dictionary<string, IOrderEntity<IOrderItem>>();

        public bool AddOrder(IOrderEntity<IOrderItem> order)
        {
            if (OrderIsValid(order))
            {
                if (_orderDict.ContainsKey(order.Id))
                {
                    IOrderEntity<IOrderItem> mappedOrder = _orderDict[order.Id];
                    _ = mappedOrder.AddItemToOrder(order.GetItemWithGreatestQuantity());
                    _orderDict[mappedOrder.Id] = mappedOrder;
                    return true;
                }
                else
                {
                    _orderDict.Add(order.Id, order);
                    return true;
                }
            }
            return false;
        }

        private static bool OrderIsValid(IOrderEntity<IOrderItem> order)
        {
            if (order != null && string.IsNullOrEmpty(order.Id) == false)
            {
                return true;
            }
            return false;
        }

        public void Clear()
        {
            _orderDict.Clear();
        }

        public IOrderEntity<IOrderItem>[] GetOrders()
        {
            if (_orderDict == null)
            {
                return null;
            }
            IOrderEntity<IOrderItem>[] orders = new IOrderEntity<IOrderItem>[_orderDict.Count];
            _orderDict.Values.CopyTo(orders, 0);
            return orders;
        }
    }
}