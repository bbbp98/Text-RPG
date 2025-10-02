using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Formats.Asn1.AsnWriter;
using static TextRPG.Program;

namespace TextRPG.Scenes
{
     internal class AdventureScene : Scene
     {
          private Character character;
          private int needStamina = 10;

          public AdventureScene(Character character)
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
               if (character.CheckStamina(needStamina) == false)
               {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("스태미나가 부족합니다.");
                    Console.ForegroundColor = ConsoleColor.White;
                    Console.WriteLine();

                    SetScene(new StartScene(character));
                    GetCurrentScene().Show();

                    return;
               }

               character.Stamina -= needStamina;

               int randNum = new Random().Next(0, 2);
               if (randNum == 0)
               {
                    Console.WriteLine("몬스터 조우! 골드 500 획득\n");
                    character.Gold += 500;
               }
               else
               {
                    Console.WriteLine("아무 일도 일어나지 않았다.\n");
               }

               Console.WriteLine("1. 다시하기");
               Console.WriteLine("0. 나가기");
          }
     }
}
