using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Formats.Asn1.AsnWriter;
using static TextRPG.Program;

namespace TextRPG.Scenes
{
    internal class InventoryScene : Scene
    {
        private Character character;
        public InventoryScene(Character character)
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
                    SetScene(new EquipmentScene(character));
                    break;
                case 2:
                    SetScene(new InventorySortScene(character));
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
            Console.WriteLine("인벤토리");
            Console.WriteLine("보유 중인 아이템을 관리할 수 있습니다.");
            Console.WriteLine();

            Console.WriteLine("[아이템 목록]");
            Console.WriteLine();

            foreach (Item item in character.Inventory)
            {
                item.ShowInfo();
            }
            Console.WriteLine();

            Console.WriteLine("1. 장착 관리");
            Console.WriteLine("2. 아이템 정렬");
            Console.WriteLine("0. 나가기");
        }

    }
}
