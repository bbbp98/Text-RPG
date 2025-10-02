using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Formats.Asn1.AsnWriter;
using static TextRPG.Program;

namespace TextRPG.Scenes
{
    internal class StartScene : Scene
    {
        const string WELCOME_MESSAGE =
        "스파르타 마을에 오신 여러분 환영합니다.\n" +
        "이곳에서 던전으로 들어가기 전 활동을 할 수 있습니다.\n";

        private Character character;

        public StartScene(Character character)
        {
            this.character = character;
        }

        public override void Show()
        {
            Console.WriteLine(WELCOME_MESSAGE);

            Console.WriteLine("1. 상태보기");
            Console.WriteLine("2. 인벤토리");
            Console.WriteLine("3. 랜덤 모험");
            Console.WriteLine("4. 마을 순찰하기");
            Console.WriteLine("5. 훈련하기");
            Console.WriteLine("6. 상점");
            Console.WriteLine("7. 던전입장");
            Console.WriteLine("8. 휴식하기");
            Console.WriteLine();
            Console.WriteLine("9. 저장");
            Console.WriteLine("0. 저장 후 종료");
            Console.WriteLine();
        }

        public override void HandleInput(byte input)
        {
            switch (input)
            {
                case 0:
                    Console.WriteLine("게임을 종료합니다.\n");
                    Save();
                    SetRunning(false);
                    break;
                case 1:
                    SetScene(new PlayerInfoScene(character));
                    break;
                case 2:
                    SetScene(new InventoryScene(character));
                    break;
                case 3:
                    SetScene(new AdventureScene(character));
                    break;
                case 4:
                    SetScene(new PatrolScene(character));
                    break;
                case 5:
                    SetScene(new TrainingScene(character));
                    break;
                case 6:
                    SetScene(new ShopScene(character));
                    break;
                case 7:
                    SetScene(new DungeonEntranceScene(character));
                    break;
                case 8:
                    SetScene(new RestScene(character));
                    break;
                case 9:
                    Save();
                    break;
                default:
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("잘못된 입력입니다.\n");
                    Console.ForegroundColor = ConsoleColor.White;
                    break;
            }
        }
    }
}
