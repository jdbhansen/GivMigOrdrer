using GivMigOrdrer.Backend.Entities;
using GivMigOrdrer.Backend.Mappers;
using System;

namespace GivMigOrdrer.Backend.Converters
{
    public class StringToOrderConverter : IConvertStringToOrderEntities<IOrderEntity<IOrderItem>>
    {
        private static readonly char[] _newLine = Environment.NewLine.ToCharArray();
        private static readonly IOrderEntityMapper<IOrderEntity<IOrderItem>> _mapper = new OrderEntityMapper();
        private readonly int minLength = 20;
        private int _convertedLines;
        public int ConvertedLines { get => _convertedLines; }

        public IOrderEntity<IOrderItem>[] ConvertStringToOrders(string str, int[] columns, SeparatorType separatorType)
        {
            if (ConverterHelper.IsStringValid(str, minLength) && ConverterHelper.DoesIntArrayRepeatValues(columns) == false)
            {
                string[] strLines = str.Split(_newLine);
                _ = str;
                return ConvertFromStringLines(strLines, columns, separatorType);
            }
            return null;
        }

        private IOrderEntity<IOrderItem>[] ConvertFromStringLines(string[] strLines, int[] columns, SeparatorType separatorType)
        {
            _mapper.Clear();
            _convertedLines = 0;
            for (int i = 0; i < strLines.Length; i++)
            {
                string[] separatedStrs = strLines[i].Split(GetSeparatorType(separatorType));
                ConvertFromSeparatedStrings(separatedStrs, columns);
            }
            return _mapper.GetOrders();
        }

        private void ConvertFromSeparatedStrings(string[] separatedStrs, int[] columns)
        {
            if (separatedStrs.Length >= columns.Length)
            {
                if (ConverterHelper.IsColumnNumbersWithinBounds(separatedStrs, columns))
                {
                    if (_mapper.AddOrder(CreateOrder(separatedStrs, columns)))
                    {
                        _convertedLines++;
                    }
                }
            }
        }

        private static IOrderEntity<IOrderItem> CreateOrder(string[] separatedStrs, int[] columns)
        {
            string orderIdStr = separatedStrs[columns[0]];
            string itemIdStr = separatedStrs[columns[1]];
            string itemQtyStr = separatedStrs[columns[2]];
            string itemNameStr = separatedStrs[columns[3]];
            if (ConverterHelper.IsItemIdAndQtyValid(itemIdStr, itemQtyStr))
            {
                int itemId = ConverterHelper.ConvertToInt(itemIdStr);
                int itemQty = ConverterHelper.ConvertToInt(ConverterHelper.RemoveDotOrComma(itemQtyStr));
                IOrderEntity<IOrderItem> order = new OrderEntity(orderIdStr);
                _ = order.AddItemToOrder(new OrderItem(itemId, itemQty, itemNameStr));
                return order;
            }
            return null;
        }

        private static char GetSeparatorType(SeparatorType separatorType)
        {
            switch (separatorType)
            {
                case SeparatorType.CSV:
                    return ';';
                case SeparatorType.TSV:
                    return '\t';
                default:
                    return '\t';
            }
        }
    }
}