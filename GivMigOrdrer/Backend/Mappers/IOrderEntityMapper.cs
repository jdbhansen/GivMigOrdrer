using GivMigOrdrer.Backend.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GivMigOrdrer.Backend.Mappers
{
    public interface IOrderEntityMapper<T> where T : IOrderEntity<IOrderItem>
    {
        T[] GetOrders();
        bool AddOrder(T order);
        void Clear();
    }
}
