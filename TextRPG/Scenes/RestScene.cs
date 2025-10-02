using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Formats.Asn1.AsnWriter;
using static TextRPG.Program;

namespace TextRPG.Scenes
{
     internal class RestScene : Scene
     {
          private Character character;
          private int price = 500;

          public RestScene(Character character)
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
                         if (character.Gold >= price)
                         {
                              Console.WriteLine("휴식을 완료했습니다.");
                              Console.WriteLine();
                              character.Gold -= price;
                              character.Hp += 100;
                              character.Stamina += 20;
                         }
                         else
                         {
                              Console.ForegroundColor = ConsoleColor.Red;
                              Console.WriteLine("Gold가 부족합니다.");
                              Console.ForegroundColor = ConsoleColor.White; ;
                              Console.WriteLine();
                         }
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
               int price = 500;

               Console.WriteLine("휴식하기");
               Console.Write($"{price} G 를 내면 체력을 회복할 수 있습니다.");
               Console.WriteLine($" (보유 골드 : {character.Gold} G)");
               Console.WriteLine();

               Console.WriteLine("1. 휴식하기");
               Console.WriteLine("0. 나가기");
          }
     }
}
