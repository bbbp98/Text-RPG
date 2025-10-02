using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Formats.Asn1.AsnWriter;
using static TextRPG.Program;

namespace TextRPG.Scenes
{
     internal class SellingItemScene : Scene
     {
          private Character character;

          public SellingItemScene(Character character)
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
                         // 판매 메서드
                         Selling(input);
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
               Console.WriteLine("상점 - 아이템 판매");
               Console.WriteLine("필요한 아이템을 판매할 수 있는 상점입니다.");
               Console.WriteLine();

               Console.WriteLine("[보유 골드]");
               Console.ForegroundColor = ConsoleColor.Yellow;
               Console.WriteLine($"{character.Gold} G");
               Console.ForegroundColor = ConsoleColor.White;
               Console.WriteLine();

               Console.WriteLine("[아이템 목록]");
               int i = 1;
               foreach (Item item in character.Inventory)
               {
                    Console.Write($"- {i++} ");
                    item.ShowInfo(Program.SceneNames.SellingItemScene);
               }
               Console.WriteLine();

               Console.WriteLine("0. 나가기");
          }

          private void Selling(byte input)
          {
               // 인벤토리에 아이템이 없는데 입력이 들어올 경우
               if (character.Inventory.Count == 0)
               {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("판매할 수 있는 아이템이 없습니다.");
                    Console.ForegroundColor = ConsoleColor.White;
                    return;
               }


               Item item = character.Inventory[input - 1];

               int typeIdx = (int)item.Type;
               // 장착중인 아이템인지 체크
               // 판매는 그대로 진행, 장착 해제
               if (character.EquiptedItems[typeIdx].Name == item.Name)
               {
                    // 장착 해제
                    character.EquiptedItems[typeIdx].HasItem = false;
                    character.EquiptedItems[typeIdx].IsEquipped = false;
                    character.EquiptedItems[typeIdx] = null!;
               }

               int price = (int)(item.Price * 0.85f);
               character.Gold += price;
               character.Inventory.Remove(item);
               GetItems().Find(i => i.Name == item.Name)!.HasItem = false;
               GetItems().Find(i => i.Name == item.Name)!.IsEquipped = false;
               Console.WriteLine($"{item.Name}을(를) 판매했습니다. {price} G를 획득했습니다.");
               Console.WriteLine();
          }
     }
}
