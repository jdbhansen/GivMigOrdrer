using System.Collections.Generic;

namespace GivMigOrdrer.Languages
{
    public class EnglishLanguage : ILanguage
    {
        public string Id { get; } = $"en";
        private readonly Dictionary<string, string> texts;

        public EnglishLanguage()
        {
            texts = new Dictionary<string, string>
            {
                { "placeholder", "placeholder" }
            };
            InitLanguage();
        }

        public string GetTextWithName(string name)
        {
            string str = name.ToLower();
            return texts.ContainsKey(str) ? texts[str] : texts[$"placeholder"];
        }

        private void InitLanguage()
        {
            texts[$"title"] = $"The Glorious \"Magter Ikke At Tælle\"-App";
            texts[$"undertitle"] = $"Combines the item quantities from the order lines";
            texts[$"pasteorders"] = $"Paste the orders lines here:";
            texts[$"copytoclipboard"] = $"Copy to clipboard";
            texts[$"convert"] = $"Convert";
            texts[$"clear"] = $"Clear";
            texts[$"orderentitynumber"] = "Order number column:";
            texts[$"orderidbox"] = $"Select the column with the order number:";
            texts[$"itemnumbertext"] = $"Item number column (varenummer):";
            texts[$"itemnumberbox"] = $"Select the column number with the item numbers (varenummer)";
            texts[$"quantity"] = $"Item quantity column (antal):";
            texts[$"quantitytooltip"] = $"Select the column number with the items quantity (antal)";
            texts[$"itemname"] = $"Item name column (varenavn):";
            texts[$"itemnametooltip"] = $"Select the column number with the items name (varenavn)";
            texts[$"sortitems"] = $"Sort the items by:";
            texts[$"itemnumber"] = $"The item number";
            texts[$"itemquantity"] = $"The item quantity";
            texts[$"copiedtoclipboard"] = $"Copied to clipboard!";
            texts[$"howtopickcolumns"] = $"The image below shows how to pick the column values, if you like Cherwin and Juan orders the table differently than the \"default\"";
            texts[$"example1"] = $"Here the values would be\nItem number column (varenummer): 3\nItem quantity column (antal): 5\nItem name column (varenavn): 6";
        }
    }
}
