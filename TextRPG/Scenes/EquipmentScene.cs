using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Formats.Asn1.AsnWriter;
using static TextRPG.Program;

namespace TextRPG.Scenes
{
    internal class EquipmentScene : Scene
    {
        private Character character;

        public EquipmentScene(Character character)
        {
            this.character = character;
        }

        public override void HandleInput(byte input)
        {
            // 1이 들어오면 0으로
            int select = 0;

            if (input > 0)
                select = (int)character.Inventory[input - 1].Type;

            // 장착하면 무조건 그 장비가 장착, 기존에 장착 중이던 장비는 해제
            switch (input)
            {
                case 0:
                    SetScene(new InventoryScene(character));
                    break;
                case 1:
                case 2:
                case 3:
                case 4:
                case 5:
                case 6:
                    // 장비 교체
                    if (character.EquiptedItems[select] != null) // character.EquiptedItems에 null이 있을 때 처리
                    {
                        character.EquiptedItems[select].IsEquipped = false;
                        character.Inventory.Find(i => i.Name == character.EquiptedItems[select].Name)!.IsEquipped = false;
                    }
                    character.EquiptedItems[select] = character.Inventory[input - 1];
                    character.EquiptedItems[select].IsEquipped = true;
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

            int i = 1;
            foreach (Item item in character.Inventory)
            {
                Console.Write($"- {i++} ");
                item.ShowInfo();
            }

            Console.WriteLine();
            Console.WriteLine("0. 나가기");
        }
    }
}
