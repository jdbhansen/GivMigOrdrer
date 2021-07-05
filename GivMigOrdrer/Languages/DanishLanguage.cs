using System.Collections.Generic;

namespace GivMigOrdrer.Languages
{
    public class DanishLanguage : ILanguage
    {
        public string Id { get; } = $"dk";

        private readonly Dictionary<string, string> texts;

        public DanishLanguage()
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
            texts[$"title"] = $"Den Gloriøse \"Magter Ikke At Tælle\"-App";
            texts[$"undertitle"] = $"Samler antallet af varer fra ordrelinjerne";
            texts[$"pasteorders"] = $"Indsæt ordrelinjerne her:";
            texts[$"copytoclipboard"] = $"Kopier til udklipsholder";
            texts[$"convert"] = $"Konvertér";
            texts[$"clear"] = $"Slet alt";
            texts[$"orderentitynumber"] = $"Ordrenummerkolonne";
            texts[$"orderidbox"] = $"Vælg kolonnen med ordrenummeret";
            texts[$"itemnumbertext"] = $"Varenummerkolonne:";
            texts[$"itemnumberbox"] = $"Vælg kolonnen med varenummeret";
            texts[$"quantity"] = $"Varemængdekolonne:";
            texts[$"quantitytooltip"] = $"Vælg kolonnen med antallet af varer";
            texts[$"itemname"] = $"Varenavnkolonne:";
            texts[$"itemnametooltip"] = $"Vælg kolonnen med varenavnet";
            texts[$"sortitems"] = $"Sortér varerne efter:";
            texts[$"itemnumber"] = $"Varenummeret";
            texts[$"itemquantity"] = $"Varemængden";
            texts[$"copiedtoclipboard"] = $"Kopieret til udklipsholderen!";
            texts[$"howtopickcolumns"] = $"Billedet nedenfor viser, hvordan man vælger kolonneværdierne, hvis man, som Cherwin og Juan, har opsat tabellen anderledes.";
            texts[$"example1"] = $"Her ville værdierne være:\nVarenummerkolonne: 3\nVaremængdekolonne (antal): 5\nVarenavnkolonne: 6";
        }
    }
}
