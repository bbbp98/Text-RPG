using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Formats.Asn1.AsnWriter;
using static TextRPG.Program;

namespace TextRPG.Scenes
{
    internal class PlayerInfoScene : Scene
    {
        private Character character;

        public PlayerInfoScene(Character character)
        {
            this.character = character;
        }

        public override void Show()
        {
            Console.WriteLine("상태 보기");
            Console.WriteLine("캐릭터의 정보가 표시됩니다.");
            Console.WriteLine();
            character.ShowInfo();

            Console.WriteLine("0. 나가기");
        }

        public override void HandleInput(byte input)
        {
            switch (input)
            {
                case 0:
                    //Console.WriteLine("마을로 돌아갑니다.");
                    SetScene(new StartScene(character));
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
