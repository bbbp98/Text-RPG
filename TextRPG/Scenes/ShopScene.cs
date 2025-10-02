using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Formats.Asn1.AsnWriter;
using static TextRPG.Program;

namespace TextRPG.Scenes
{
     internal class ShopScene : Scene
     {
          private Character character;

          public ShopScene(Character character)
          {
               this.character = character;
          }

          public override void HandleInput(byte input)
          {
               switch (input)
               {
                    case 0:
                         SetScene(new StartScene(character));
                         break;
                    case 1:
                         SetScene(new PurchaseItemScene(character));
                         break;
                    case 2:
                         SetScene(new SellingItemScene(character));
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
               Console.WriteLine("상점");
               Console.WriteLine("필요한 아이템을 얻을 수 있는 상점입니다.");
               Console.WriteLine();

               Console.WriteLine("[보유 골드]");
               Console.ForegroundColor = ConsoleColor.Yellow;
               Console.WriteLine($"{character.Gold} G");
               Console.ForegroundColor = ConsoleColor.White;
               Console.WriteLine();

               Console.WriteLine("[아이템 목록]");
               foreach (Item item in GetItems())
               {
                    Console.Write("- ");
                    item.ShowInfo(Program.SceneNames.ShopScene);
               }
               Console.WriteLine();

               Console.WriteLine("1. 아이템 구매");
               Console.WriteLine("2. 아이템 판매");
               Console.WriteLine("0. 나가기");
          }
     }
}
