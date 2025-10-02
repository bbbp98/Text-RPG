using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Formats.Asn1.AsnWriter;
using static TextRPG.Program;

namespace TextRPG.Scenes
{
     internal class TrainingScene : Scene
     {
          private Character character;
          private int needStamina = 15;
          public TrainingScene(Character character)
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
                    Console.WriteLine("스태미나가 부족합니다.");
                    Console.WriteLine();

                    SetScene(new StartScene(character));
                    GetCurrentScene().Show();
                    return;
               }

               character.Stamina -= needStamina;

               int randNum = new Random().Next(0, 100);
               int gettingExp = 0;

               if (randNum < 15)
               {
                    Console.WriteLine("훈련이 잘 되었습니다.");
                    gettingExp = 60;
               }
               else if (randNum < (15 + 60))
               {
                    Console.WriteLine("오늘 하루 열심히 훈련했습니다.");
                    gettingExp = 40;
               }
               else
               {
                    Console.WriteLine("하기 싫다...훈련이...");
                    gettingExp = 30;
               }

               character.Exp += gettingExp;
               Console.WriteLine($"경험치를 {gettingExp} 획득했습니다.");

               Console.WriteLine();
               Console.WriteLine("0. 나가기");
          }
     }
}
