using System.Collections.Generic;

namespace GivMigOrdrer.Languages
{
    public class SpanishLanguage : ILanguage
    {
        public string Id { get; } = $"es";
        private readonly Dictionary<string, string> texts;

        public SpanishLanguage()
        {
            texts = new Dictionary<string, string>
            {
                {"placeholder", "placeholder" }
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
            texts[$"title"] = $"La Gloriosa \"No Puede Soportar A Contar\"-App";
            texts[$"undertitle"] = $"Recopila las cantidades de artículos de las líneas de pedido";
            texts[$"pasteorders"] = $"Pega las líneas de pedidos aquí:";
            texts[$"copytoclipboard"] = $"Copiar al portapapeles";
            texts[$"convert"] = $"Converso";
            texts[$"clear"] = $"Limpiar todo";
            texts[$"orderentitynumber"] = "Columna de número de pedido:";
            texts[$"orderidbox"] = $"Seleccione la columna con el número de pedido:";
            texts[$"itemnumbertext"] = $"Columna de número de artículo (varenummer):";
            texts[$"itemnumberbox"] = $"Seleccione el número de columna con los números de artículo (varenummer)";
            texts[$"quantity"] = $"Columna de cantidad de artículo (antal):";
            texts[$"quantitytooltip"] = $"Seleccione el número de columna con la cantidad de artículos (antal)";
            texts[$"itemname"] = $"Columna de nombre de artículo (varenavn):";
            texts[$"itemnametooltip"] = $"Seleccione el número de columna con el nombre de artículos (varenavn)";
            texts[$"sortitems"] = $"Ordenar los elementos por:";
            texts[$"itemnumber"] = $"El número de artículo";
            texts[$"itemquantity"] = $"La cantidad de artículos";
            texts[$"copiedtoclipboard"] = $"Copiado al portapapeles!";
            texts[$"howtopickcolumns"] = $"La siguiente imagen muestra cómo elegir los valores de las columnas, si usted, como Cherwin y Juan, ordena la tabla de manera diferente a la \"predeterminada\".";
            texts[$"example1"] = $"Aquí los valores serían:\nColumna de número de artículo (varenummer): 3\nColumna de cantidad de artículo (antal): 5\nColumna de nombre de artículo (varenavn): 6";
            texts[$"itemboxtext"] = "Seleccionar pedidos con objeto específico:";
            texts[$"printbutton"] = $"Impresión";
        }
    }
}
