using GivMigOrdrer.Backend.Entities;

namespace GivMigOrdrer.Backend.Converters
{
    public interface IConvertStringToOrderEntities<T> where T : IOrderEntity<IOrderItem>
    {
        T[] ConvertStringToOrders(string str, int[] columns, SeparatorType separatorType);
        int ConvertedLines { get; }
    }

    public enum SeparatorType
    {
        CSV,
        TSV
    }
}
