using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Formats.Asn1.AsnWriter;
using static TextRPG.Program;

namespace TextRPG.Scenes
{
     internal class DungeonClearScene : Scene
     {
          private Character character;

          public DungeonClearScene(Character character)
          {
               this.character = character;
          }

          public override void HandleInput(byte input)
          {
               switch (input)
               {
                    case 0:
                         SetScene(new DungeonEntranceScene(character));
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
               Random rand = new Random();
               // 20 ~ 35 랜덤으로 체력 감소
               int hpDecrease = rand.Next(20, 36);
               float defDiff = GetNowDungeon().RequiredDef - character.Defence;
               // hpDecrease : 감소될 체력량
               hpDecrease += (int)defDiff;

               // 던전 입장으로 소모되는 체력
               int hpBefore = character.Hp;
               character.Hp -= hpDecrease;

               if (character.Hp < 0)
                    character.Hp = 0;

               // 권장 방어력보다 낮은 경우
               if (defDiff > 0)
               {
                    int randNum = rand.Next(0, 10);

                    // fail clear
                    if (randNum < 4)
                    {
                         Console.ForegroundColor = ConsoleColor.Red;
                         Console.WriteLine("던전 클리어 실패");
                         Console.WriteLine($"{GetNowDungeon().Name}을 클리어 하지 못했습니다.");
                         Console.WriteLine();
                         Console.ForegroundColor = ConsoleColor.White;

                         Console.WriteLine("[탐험 결과]");
                         Console.WriteLine($"체력 {hpBefore} -> {character.Hp}");
                         Console.WriteLine();
                         Console.WriteLine("1. 다시하기");
                         Console.WriteLine("0. 나가기");
                         Console.WriteLine();
                         return;
                    }
               }

               // clear

               // Random().NextDouble() : 0.0 ~ 1.0 사이 랜덤 값 반환
               float bonus = (float)((rand.NextDouble() * character.Attack) + character.Attack);
               bonus *= 0.01f;
               bonus = GetNowDungeon().RewardGold * bonus;
               int clearGold = GetNowDungeon().RewardGold + (int)bonus;

               int goldBefore = character.Gold;
               character.Gold += clearGold;
               character.Exp += GetNowDungeon().RewardExp;

               Console.WriteLine("던전 클리어");
               Console.WriteLine("축하합니다!!");
               Console.WriteLine($"{GetNowDungeon().Name}을 클리어 하였습니다");
               Console.WriteLine();

               Console.WriteLine("[탐험 결과]");
               Console.WriteLine($"체력 {hpBefore} -> {character.Hp}");
               Console.WriteLine($"Gold {goldBefore} G -> {character.Gold} G");
               Console.WriteLine();

               Console.WriteLine("1. 다시하기");
               Console.WriteLine("0. 나가기");
          }
     }
}
