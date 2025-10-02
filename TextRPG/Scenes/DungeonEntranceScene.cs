using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Formats.Asn1.AsnWriter;
using static TextRPG.Program;

namespace TextRPG.Scenes
{
     internal class DungeonEntranceScene : Scene
     {
          private Character character;

          public DungeonEntranceScene(Character character)
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
                    case 2:
                    case 3:
                         // load dungeon clear scene
                         SetScene(new DungeonClearScene(character));
                         SetNowDungeon(GetDungeons()[input - 1]);
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
               Console.WriteLine("던전입장");
               Console.WriteLine("이곳에서 던전으로 들어가기전 활동을 할 수 있습니다.");
               Console.WriteLine();

               // 난이도 별 던전 
               int i = 1;
               foreach (Dungeon dungeon in GetDungeons())
               {
                    Console.Write($"{i++}. ");
                    dungeon.ShowInfo();
               }

               Console.WriteLine("0. 나가기");
          }
     }
}
