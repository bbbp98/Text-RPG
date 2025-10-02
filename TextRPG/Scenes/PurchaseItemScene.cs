using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Formats.Asn1.AsnWriter;
using static TextRPG.Program;

namespace TextRPG.Scenes
{
     internal class PurchaseItemScene : Scene
     {
          private Character character;

          public PurchaseItemScene(Character character)
          {
               this.character = character;
          }

          public override void HandleInput(byte input)
          {
               switch (input)
               {
                    case 0:
                         SetScene(new ShopScene(character));
                         break;
                    case 1:
                    case 2:
                    case 3:
                    case 4:
                    case 5:
                    case 6:
                         Purchase(input);
                         break;
                    default:
                         Console.ForegroundColor = ConsoleColor.Red;
                         Console.WriteLine("잘못된 입력입니다.\n");
                         Console.ForegroundColor = ConsoleColor.White;
                         break;
               }
          }

          public override void Show()
          {
               Console.WriteLine("상점 - 아이템 구매");
               Console.WriteLine("필요한 아이템을 얻을 수 있는 상점입니다.");
               Console.WriteLine();

               Console.WriteLine("[보유 골드]");
               Console.ForegroundColor = ConsoleColor.Yellow;
               Console.WriteLine($"{character.Gold} G");
               Console.ForegroundColor = ConsoleColor.White;
               Console.WriteLine();

               Console.WriteLine("[아이템 목록]");
               int i = 1;
               foreach (Item item in GetItems())
               {
                    Console.Write($"- {i++} ");
                    item.ShowInfo(Program.SceneNames.PurchaseItemScene);
               }
               Console.WriteLine();

               Console.WriteLine("0. 나가기");
          }

          private void Purchase(byte input)
          {
               Item item = GetItems()[input - 1];

               // 아이템 보유 체크
               if (item.HasItem)
               {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine($"이미 {item.Name}을(를) 보유하고 있습니다.");
                    Console.ForegroundColor = ConsoleColor.White;
                    Console.WriteLine();
               }
               else
               {
                    // 보유 금액 체크
                    if (character.Gold >= item.Price)
                    {
                         character.Gold -= item.Price;

                         item.HasItem = true;
                         character.Inventory.Add(item);
                         Console.WriteLine($"{item.Name}을(를) 구매했습니다.");
                         Console.WriteLine();
                    }
                    else
                    {
                         Console.ForegroundColor = ConsoleColor.Red;
                         Console.WriteLine("보유 금액이 부족합니다.");
                         Console.ForegroundColor = ConsoleColor.White;
                         Console.WriteLine();
                    }
               }
          }
     }
}
