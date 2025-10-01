using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TextRPG
{

     internal class Item
     {
          public string Name { get; set; }
          public ItemType Type { get; set; }
          public int Value { get; set; }
          public int Price { get; set; }
          string Description { get; set; }

          public bool IsEquipped { get; set; }
          public bool HasItem { get; set; }

          public Item(ItemType type, ItemIndexes index)
          {
               ItemData data = new ItemData();

               Name = data.itemNames[(int)index];
               Type = type;
               Value = data.itemValues[(int)index];
               Price = data.itemPrices[(int)index];
               Description = data.itemDescriptions[(int)index];
               IsEquipped = false;
               HasItem = false;
          }

          public void ShowInfo()
          {
               string[] valueType =
               {
                    "방어력",
                    "공격력",
               };

               if (IsEquipped)
               {
                    Console.ForegroundColor = ConsoleColor.Yellow;
                    Console.Write("[E]");
                    Console.ForegroundColor = ConsoleColor.White;
                    Console.Write($"{Name,-7}| ");
               }
               else
                    Console.Write($"{Name,-10}| ");
               Console.Write($"{valueType[(int)Type] + " +" + Value,-10}| ");
               Console.Write($"{Description,-30}");
               Console.WriteLine();
          }

          public void ShowInfo(Program.Scene scene)
          {
               string[] valueType =
               {
                    "방어력",
                    "공격력",
               };

               Console.Write($"{Name,-10}| ");
               Console.Write($"{valueType[(int)Type] + " +" + Value,-10}| ");
               Console.Write($"{Description,-30}");
               Console.Write("| ");

               if (scene == Program.Scene.ShopScene
                    || scene == Program.Scene.PurchaseItemScene)
               {
                    if (HasItem)
                         Console.Write("구매 완료");
                    else
                         Console.Write($"{Price} G");
               }
               else if (scene == Program.Scene.SellingItemScene)
               {
                    Console.Write($"{(int)(Price * 0.85f)} G");
               }

               Console.WriteLine();
          }
     }
}
