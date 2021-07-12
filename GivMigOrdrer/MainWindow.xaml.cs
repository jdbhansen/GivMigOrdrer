using GivMigOrdrer.Backend.Converters;
using GivMigOrdrer.Backend.Entities;
using GivMigOrdrer.Backend.Entities.Enums;
using GivMigOrdrer.Languages;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Printing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Brushes = System.Drawing.Brushes;

namespace GivMigOrdrer
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private static readonly LanguageHolder _languages = new LanguageHolder();
        private static readonly IConvertStringToOrderEntities<IOrderEntity<IOrderItem>> CSO = new StringToOrderConverter();
        private static readonly StringBuilder _sb = new StringBuilder();
        private readonly Font font = new Font("Times New Roman", 11);
        private readonly PrintDocument doc = new PrintDocument();


        public MainWindow()
        {
            ToolTipService.ShowDurationProperty.OverrideMetadata(typeof(DependencyObject), new FrameworkPropertyMetadata(int.MaxValue));
            InitializeComponent();
            SetAllTextToLanguage();
            ItemTypeBox.IsEnabled = false;
        }

        private void SetAllTextToLanguage(string id = "en")
        {
            _languages.SelectLanguage(id);
            TitleText.Text = _languages.GetTextWithName("title");
            UnderTitle.Text = _languages.GetTextWithName("undertitle");
            Itemname.Text = _languages.GetTextWithName(Itemname.Name);
            Quantity.Text = _languages.GetTextWithName(Quantity.Name);
            ItemNumberText.Text = _languages.GetTextWithName(ItemNumberText.Name);
            ItemNumberBox.ToolTip = _languages.GetTextWithName(ItemNumberBox.Name);
            QuantityToolTip.ToolTip = _languages.GetTextWithName(QuantityToolTip.Name);
            ItemNameTooltip.ToolTip = _languages.GetTextWithName(ItemNameTooltip.Name);
            CopyToClipboard.Content = _languages.GetTextWithName(CopyToClipboard.Name);
            OrderEntityNumber.Text = _languages.GetTextWithName(OrderEntityNumber.Name);
            OrderIdBox.ToolTip = _languages.GetTextWithName(OrderIdBox.Name);
            Clear.Content = _languages.GetTextWithName(Clear.Name);
            Convert.Content = _languages.GetTextWithName(Convert.Name);
            Pasteorders.Content = _languages.GetTextWithName(Pasteorders.Name);
            Sortitems.Text = _languages.GetTextWithName(Sortitems.Name);
            Itemnumber.Content = _languages.GetTextWithName(Itemnumber.Name);
            Itemquantity.Content = _languages.GetTextWithName(Itemquantity.Name);
            Example1.Text = _languages.GetTextWithName(Example1.Name);
            Howtopickcolumns.Text = _languages.GetTextWithName(Howtopickcolumns.Name);
            itemboxtext.Text = _languages.GetTextWithName(itemboxtext.Name);
            PrintButton.Content = _languages.GetTextWithName(PrintButton.Name);
        }

        private void EN_Click(object sender, RoutedEventArgs e)
        {
            SetAllTextToLanguage(EN.Name.ToLower());
        }

        private void DK_Click(object sender, RoutedEventArgs e)
        {
            SetAllTextToLanguage(DK.Name.ToLower());
        }

        private void ES_Click(object sender, RoutedEventArgs e)
        {
            SetAllTextToLanguage(ES.Name.ToLower());
        }

        private void Convert_Click(object sender, RoutedEventArgs e)
        {
            LinesConverted.Text = "0";
            int[] coords =
                {
                int.Parse(OrderIdBox.Text) - 1,
                int.Parse(ItemNumberBox.Text) - 1,
                int.Parse(QuantityToolTip.Text) - 1,
                int.Parse(ItemNameTooltip.Text) - 1
                };
            string str = InputBox.Text;
            GetOrders(str, coords);
        }

        private void Clear_Click(object sender, RoutedEventArgs e)
        {
            SetOutputBoxText("");
            LinesConverted.Text = "0";
            InputBox.Text = "";
        }

        private async void GetOrders(string str, int[] coords)
        {
            IOrderEntity<IOrderItem>[] orders = Array.Empty<IOrderEntity<IOrderItem>>();
            Task task = Task.Run(() =>
            {
                orders = CSO.ConvertStringToOrders(str, coords, SeparatorType.TSV);
            });
            SetOutputBoxText("Loading...");
            LinesConverted.Text = "Loading...";
            DisableInput();
            await task;
            if (orders != null && orders.Length != 0)
            {
                LinesConverted.Text = CSO.ConvertedLines.ToString();
                if (GetOnlyItems.IsChecked == true)
                {
                    Dictionary<int, IOrderItem> items = new Dictionary<int, IOrderItem>();
                    foreach (IOrderEntity<IOrderItem> order in orders)
                    {
                        foreach (IOrderItem item in order.GetItems())
                        {
                            if (items.ContainsKey(item.Id))
                            {
                                IOrderItem mappedItem = items[item.Id];
                                _ = mappedItem.AddToQuantity(item.Quantity);
                                items[mappedItem.Id] = mappedItem;
                            }
                            else
                            {
                                items.Add(item.Id, item);
                            }
                        }
                    }
                    IOrderItem[] itemArray = new IOrderItem[items.Count];
                    items.Values.CopyTo(itemArray, 0);
                    if (SortBox.SelectedIndex == 0)
                    {
                        Array.Sort(itemArray, (x, y) => x.Id.CompareTo(y.Id));
                    }
                    else
                    {
                        Array.Sort(itemArray, (x, y) => y.Quantity.CompareTo(x.Quantity));
                    }
                    SetOutputBoxText(ConvertItemsToString(itemArray));
                }
                else
                {
                    orders = GetOnlyOrdersWithType(orders);

                    if (SortBox.SelectedIndex == 0 && GetOnlyItems.IsChecked == false)
                    {
                        Array.Sort(orders, (x, y) => x.GetItemWithLowestId().Id.CompareTo(y.GetItemWithLowestId().Id));
                    }
                    if (SortBox.SelectedIndex == 1 && GetOnlyItems.IsChecked == false)
                    {
                        Array.Sort(orders, (x, y) => y.GetItemWithGreatestQuantity().Quantity.CompareTo(x.GetItemWithGreatestQuantity().Quantity));
                    }
                    if (orders.Length != 0)
                    {
                        SetOutputBoxText(ConvertOrdersToString(orders));
                    }
                    else
                    {
                        _ = orders;
                        SetOutputBoxText($"There is no orders with {GetItemType(ItemTypeBox.SelectedIndex - 1)} items.");
                    }
                }
            }
            else
            {
                LinesConverted.Text = "0";
                SetOutputBoxText($"Computer says \"no\".");
            }
            EnableInput();
        }

        //private IOrderItem[] GetOnlyItemsOfType(IOrderItem[] items)
        //{
        //    int selected = ItemTypeBox.SelectedIndex;
        //    ItemType itemType = (ItemType)selected;
        //    List<IOrderItem> itemList = new List<IOrderItem>();
        //    for (int i = 0; i < items.Length; i++)
        //    {
        //        if (items[i].ItemType == itemType)
        //        {
        //            itemList.Add(items[i]);
        //        }
        //    }
        //    return items.ToArray();
        //}

        private IOrderEntity<IOrderItem>[] GetOnlyOrdersWithType(IOrderEntity<IOrderItem>[] orders)
        {
            int selected = ItemTypeBox.SelectedIndex;
            if (selected != 0)
            {
                // Kunne bare caste selected - 1 til ItemType, men en switch gør,
                // at man er fuldstændig sikker på hvad itemtype er.
                ItemType itemType = GetItemType(selected - 1);
                List<IOrderEntity<IOrderItem>> orderList = new List<IOrderEntity<IOrderItem>>();
                for (int i = 0; i < orders.Length; i++)
                {
                    if (orders[i].GetItemWithLowestId().ItemType == itemType)
                    {
                        orderList.Add(orders[i]);
                    }
                }
                return orderList.ToArray();
            }
            return orders;
        }

        private ItemType GetItemType(int value)
        {
            switch (value)
            {
                case 0:
                    return ItemType.Accessory;
                case 1:
                    return ItemType.Headset;
                case 2:
                    return ItemType.Mouse;
                case 3:
                    return ItemType.Mousepad;
                case 4:
                    return ItemType.Keyboard;
                case 5:
                    return ItemType.Misc;
                default:
                    return ItemType.Misc;
            }
        }

        private string ConvertItemsToString(IOrderItem[] items)
        {
            _ = _sb.Clear();
            _ = _sb.Append($"id\tqty\n");
            for (int i = 0; i < items.Length; i++)
            {
                _ = _sb.Append(items[i]);
            }
            return _sb.ToString();
        }

        private string ConvertOrdersToString(IOrderEntity<IOrderItem>[] orders)
        {
            _ = _sb.Clear();

            for (int i = 0; i < orders.Length; i++)
            {
                _ = _sb.Append(orders[i].ToString());
                _ = _sb.Append($"\n");
            }
            return _sb.ToString();
        }

        private void EnableInput()
        {
            Convert.IsEnabled = true;
            Clear.IsEnabled = true;
            EN.IsEnabled = true;
            DK.IsEnabled = true;
            ES.IsEnabled = true;
            CopyToClipboard.IsEnabled = true;
            InputBox.IsEnabled = true;
        }

        private void DisableInput()
        {
            Convert.IsEnabled = false;
            Clear.IsEnabled = false;
            EN.IsEnabled = false;
            DK.IsEnabled = false;
            ES.IsEnabled = false;
            CopyToClipboard.IsEnabled = false;
            InputBox.IsEnabled = false;
        }

        private void SetOutputBoxText(string str)
        {
            OutputBox.Text = str;
        }

        private void CopyToClipboard_Click(object sender, RoutedEventArgs e)
        {
            Clipboard.SetText(OutputBox.Text);
            SetOutputBoxText(_languages.GetTextWithName("copiedtoclipboard"));
        }

        private void PrintButton_Click(object sender, RoutedEventArgs e)
        {
            string str = OutputBox.Text;
            if (string.IsNullOrEmpty(str) == false)
            {
                doc.PrintPage += delegate (object send, PrintPageEventArgs eventArgs)
                {
                    _ = eventArgs.Graphics.MeasureString(
                        str,
                        font,
                        eventArgs.MarginBounds.Size,
                        StringFormat.GenericTypographic,
                        out int charsOnPage,
                        out int LinesOnPage
                        );

                    eventArgs.Graphics.DrawString(
                        str,
                        font,
                        Brushes.Black,
                        eventArgs.MarginBounds
                        );

                    str = str.Substring(charsOnPage);
                    eventArgs.HasMorePages = str.Length > 0;
                };
                doc.Print();
            }
        }

        private void GetOnlyItems_Click(object sender, RoutedEventArgs e)
        {
            ItemTypeBox.IsEnabled = GetOnlyItems.IsChecked != true;
        }
    }
}
