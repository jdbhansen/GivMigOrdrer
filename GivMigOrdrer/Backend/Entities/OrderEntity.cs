using GivMigOrdrer.Backend.Entities.Enums;
using System.Collections.Generic;
using System.Text;

namespace GivMigOrdrer.Backend.Entities
{
    public class OrderEntity : IOrderEntity<IOrderItem>
    {
        private readonly List<IOrderItem> _items = new List<IOrderItem>();

        public string Id { get; }

        public CourierType Courier { get; } = CourierType.UPS;

        public OrderEntity(string id, CourierType courier = CourierType.UPS)
        {
            Id = id;
            Courier = courier;
        }

        public int AddItemToOrder(IOrderItem item)
        {
            if (item != null && item.Id != 0)
            {
                for (int i = 0; i < _items.Count; i++)
                {
                    if (_items[i].Id == item.Id)
                    {
                        _ = _items[i].AddToQuantity(item.Quantity);
                        SortItemsById();
                        return _items.Count;
                    }
                }
                _items.Add(item);
                SortItemsById();
            }
            return _items.Count;
        }

        public List<IOrderItem> GetItems()
        {
            return _items.Count == 0 ? null : _items;
        }

        public IOrderItem GetItemWithGreatestQuantity()
        {
            if (_items.Count == 0)
            {
                return null;
            }
            if (_items.Count == 1)
            {
                return _items[0];
            }
            else
            {
                SortItemsByQuantity();
                return _items[0];
            }
        }

        public IOrderItem GetItemWithLowestId()
        {
            if (_items.Count == 0)
            {
                return null;
            }
            if (_items.Count == 1)
            {
                return _items[0];
            }
            SortItemsById();
            return _items[0];
        }

        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            _ = sb.Append($"{Id}\nid\tqty\n");
            for (int i = 0; i < _items.Count; i++)
            {
                IOrderItem item = _items[i];
                _ = sb.Append(item);
            }
            return sb.ToString();
        }



        /// <summary>
        /// Sorts the list after the items' quantities.
        /// </summary>
        private void SortItemsByQuantity()
        {
            _items.Sort((x, y) => y.Quantity.CompareTo(x.Quantity));
        }

        private void SortItemsById()
        {
            _items.Sort((x, y) => x.Id.CompareTo(y.Id));
        }
    }
}
