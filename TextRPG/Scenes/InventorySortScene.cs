using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Formats.Asn1.AsnWriter;
using static TextRPG.Program;

namespace TextRPG.Scenes
{
    internal class InventorySortScene : Scene
    {
        private Character character;

        public InventorySortScene(Character character)
        {
            this.character = character;
        }

        public override void HandleInput(byte input)
        {
            switch (input)
            {
                // 정렬 타입 선택
                case 0:
                    SetScene(new InventoryScene(character));
                    break;
                case 1:
                    // 이름순 정렬
                    // 람다식 버전
                    //itemLists.Sort((x, y) => x.Name.CompareTo(y.Name));

                    character.Inventory = character.Inventory.OrderBy(p => p.Name).ToList();
                    break;
                case 2:
                    // 장착순 정렬

                    // 람다식 버전
                    //itemLists.Sort((x, y) => y.HasEquipped.CompareTo(x.HasEquipped));
                    // 람다식에서 x, y 위치에 따라 true먼저, false먼저 결정 가능

                    // LINQ .OrderBy버전
                    character.Inventory = character.Inventory.OrderByDescending(p => p.IsEquipped).ToList();
                    break;
                case 3:
                    // ThenBy를 사용하면 조건을 추가할 수 있음
                    // ~~Descending을 사용하면 내림차순으로 바꿀 수 있음
                    // 기본 OrderBy, ThenBy는 오름차순
                    character.Inventory = character.Inventory.OrderByDescending(p => p.Type).ThenByDescending(p => p.Value).ToList();
                    break;
                case 4:
                    character.Inventory = character.Inventory.OrderBy(p => p.Type).ThenByDescending(p => p.Value).ToList();
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
            Console.WriteLine("인벤토리 - 아이템 정렬");
            Console.WriteLine("보유 중인 아이템을 관리할 수 있습니다.");
            Console.WriteLine();

            Console.WriteLine("[아이템 목록]");
            Console.WriteLine();

            foreach (Item item in character.Inventory)
            {
                item.ShowInfo();
            }
            Console.WriteLine();

            Console.WriteLine("1. 이름");
            Console.WriteLine("2. 장착순");
            Console.WriteLine("3. 공격력");
            Console.WriteLine("4. 방어력");
            Console.WriteLine("0. 나가기");
        }
    }
}
