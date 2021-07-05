using System;

namespace GivMigOrdrer.Backend.Converters
{
    public static class ConverterHelper
    {
        public static bool IsStringValid(string str, int minLength)
        {
            return !string.IsNullOrEmpty(str) && str.Length >= minLength;
        }

        public static bool DoesIntArrayRepeatValues(int[] columnNumbers)
        {
            if (columnNumbers != null && columnNumbers.Length >= 2)
            {
                for (int i = 0; i < columnNumbers.Length; i++)
                {
                    for (int j = i + 1; j < columnNumbers.Length; j++)
                    {
                        if (columnNumbers[i] == columnNumbers[j])
                        {
                            return true;
                        }
                    }
                }
            }
            return false;
        }

        public static bool IsColumnNumbersWithinBounds(string[] strs, int[] columnNumbers)
        {
            if (strs.Length >= columnNumbers.Length)
            {
                for (int i = 0; i < columnNumbers.Length; i++)
                {
                    if (columnNumbers[i] >= strs.Length)
                    {
                        return false;
                    }
                }
            }
            return true;
        }

        public static bool IsItemIdAndQtyValid(string idStr, string QtyStr)
        {
            return IsStringAnInt(idStr) && IsQtyValid(QtyStr);
        }

        public static bool IsQtyValid(string str)
        {
            if (str.Contains(",") || str.Contains("."))
            {
                return IsStringAnInt(RemoveDotOrComma(str));
            }
            return false;
        }

        public static string RemoveDotOrComma(string str)
        {
            if (str.Contains(","))
            {
                return str.Substring(0, str.IndexOf(","));
            }
            if (str.Contains("."))
            {
                return str.Substring(0, str.IndexOf("."));
            }
            return "bob";
        }

        public static bool IsStringAnInt(string str)
        {
            return int.TryParse(str, out _);
        }

        public static int ConvertToInt(string str)
        {
            return Convert.ToInt32(str);
        }
    }
}
