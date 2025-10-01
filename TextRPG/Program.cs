using System.Runtime.CompilerServices;
using System.Security.Principal;

namespace TextRPG
{
     internal class Program
     {
          enum Scene
          {
               StartScene,
               PlayerInfoScene,
               InventoryScene,
               InventorySortScene,
               EquipmentScene,
               AdventureScene,
               PatrolScene,
               TrainingScene,
          }

          static string[] itemNames =
          {
               "무쇠갑옷",
               "스파르타의 갑옷",
               "스파르타의 창",
               "낡은 검",
          };

          static string[] itemDescriptions =
          {
               "무쇠로 만들어져 튼튼한 갑옷입니다.",
               "스파르타의 전사들이 사용했다는 전설의 갑옷입니다.",
               "스파르타의 전사들이 사용했다는 전설의 창입니다.",
               "쉽게 볼 수 있는 낡은 검 입니다.",
          };

          static int[] itemValues =
          {
               5,
               10,
               7,
               2,
          };

          const string WELCOME_MESSAGE =
          "스파르타 마을에 오신 여러분 환영합니다.\n" +
          "이곳에서 던전으로 들어가기 전 활동을 할 수 있습니다.\n";

          static private bool isRunning = true;
          static private Scene scene = Scene.StartScene;

          static Character character = new Character("Chad", "전사");
          static List<Item> itemLists = new List<Item>();

          static int needStamina = 0;

          static void Main(string[] args)
          {
               itemLists.Add(new Item(itemNames[(int)ItemIndexes.IronArmor], ItemType.Armor, itemValues[(int)ItemIndexes.IronArmor], itemDescriptions[(int)ItemIndexes.IronArmor]));
               itemLists.Add(new Item(itemNames[(int)ItemIndexes.SpartaArmor], ItemType.Armor, itemValues[(int)ItemIndexes.SpartaArmor], itemDescriptions[(int)ItemIndexes.SpartaArmor])); ;
               itemLists.Add(new Item(itemNames[(int)ItemIndexes.SpartaSpear], ItemType.Weapon, itemValues[(int)ItemIndexes.SpartaSpear], itemDescriptions[(int)ItemIndexes.SpartaSpear]));
               itemLists.Add(new Item(itemNames[(int)ItemIndexes.OldSword], ItemType.Weapon, itemValues[(int)ItemIndexes.OldSword], itemDescriptions[(int)ItemIndexes.OldSword]));

               // 기본 장비 장착
               character.EquiptedItems.Add(itemLists[(int)ItemIndexes.IronArmor]);
               itemLists[(int)ItemIndexes.IronArmor].HasEquipped = true;
               character.EquiptedItems.Add(itemLists[(int)ItemIndexes.OldSword]);
               itemLists[(int)ItemIndexes.OldSword].HasEquipped = true;

               while (isRunning)
               {
                    PrintBehaviorList();
                    InputBehavior();
               }
          }

          static void PrintBehaviorList()
          {
               switch (scene)
               {
                    case Scene.StartScene:
                         StartScene();
                         break;
                    case Scene.PlayerInfoScene:
                         PlayerInfoScene();
                         break;
                    case Scene.InventoryScene:
                         InventoryScene();
                         break;
                    case Scene.InventorySortScene:
                         InventorySortScene();
                         break;
                    case Scene.EquipmentScene:
                         EquipmentScene();
                         break;
                    case Scene.AdventureScene:
                         AdventureScene();
                         break;
                    case Scene.PatrolScene:
                         PatrolScene();
                         break;
                    case Scene.TrainingScene:
                         TrainingScene();
                         break;
               }


               Console.WriteLine("원하시는 행동을 입력해주세요.");
               Console.Write(">> ");
          }

          static void InputBehavior()
          {
               byte input = 0;

               if (byte.TryParse(Console.ReadLine(), out input) == false)
                    input = byte.MaxValue;   // default로 가서 값 다시 받기


               Console.Clear();

               // scene 별로 입력에 대한 함수 실행
               switch (scene)
               {
                    case Scene.StartScene:
                         StartSceneInput(input);
                         break;
                    case Scene.PlayerInfoScene:
                         PlayerInfoSceneInput(input);
                         break;
                    case Scene.InventoryScene:
                         InventorySceneInput(input);
                         break;
                    case Scene.InventorySortScene:
                         InventorySortSceneInput(input);
                         break;
                    case Scene.EquipmentScene:
                         EquipmentSceneInput(input);
                         break;
                    case Scene.AdventureScene:
                         AdventureSceneInput(input);
                         break;
                    case Scene.PatrolScene:
                         PatrolSceneInput(input);
                         break;
                    case Scene.TrainingScene:
                         TrainingSceneInput(input);
                         break;
                    default:
                         break;
               }

               //Console.WriteLine();
          }

          #region Scene Method
          static void StartScene()
          {
               Console.WriteLine(WELCOME_MESSAGE);

               Console.ForegroundColor = ConsoleColor.Yellow;
               Console.WriteLine("1. 상태보기");
               Console.WriteLine("2. 인벤토리");
               Console.WriteLine("3. 랜덤 모험");
               Console.WriteLine("4. 마을 순찰하기");
               Console.WriteLine("5. 훈련하기");
               Console.WriteLine("0. 종료하기");
               Console.ForegroundColor = ConsoleColor.White;

               Console.WriteLine();
          }

          static void StartSceneInput(byte input)
          {
               switch (input)
               {
                    case 0:
                         Console.WriteLine("게임을 종료합니다.\n");
                         isRunning = false;
                         break;
                    case 1:
                         scene = Scene.PlayerInfoScene;
                         break;
                    case 2:
                         scene = Scene.InventoryScene;
                         break;
                    case 3:
                         scene = Scene.AdventureScene;
                         break;
                    case 4:
                         scene = Scene.PatrolScene;
                         break;
                    case 5:
                         scene = Scene.TrainingScene;
                         break;
                    default:
                         Console.ForegroundColor = ConsoleColor.Red;
                         Console.WriteLine("잘못된 입력입니다.\n");
                         Console.ForegroundColor = ConsoleColor.White;
                         break;
               }

          }

          static void PlayerInfoScene()
          {
               Console.WriteLine("상태 보기");
               Console.WriteLine("캐릭터의 정보가 표시됩니다.");
               Console.WriteLine();
               character.ShowInfo();

               Console.ForegroundColor = ConsoleColor.Yellow;
               Console.WriteLine("0. 나가기");
               Console.ForegroundColor = ConsoleColor.White;

               Console.WriteLine();
          }

          static void PlayerInfoSceneInput(byte input)
          {
               switch (input)
               {
                    case 0:
                         //Console.WriteLine("마을로 돌아갑니다.");
                         scene = Scene.StartScene;
                         break;
                    default:
                         Console.ForegroundColor = ConsoleColor.Red;
                         Console.WriteLine("잘못된 입력입니다.\n");
                         Console.ForegroundColor = ConsoleColor.White;
                         break;
               }

          }

          static void InventoryScene()
          {
               Console.WriteLine("인벤토리");
               Console.WriteLine("보유 중인 아이템을 관리할 수 있습니다.");
               Console.WriteLine();

               Console.WriteLine("[아이템 목록]");
               Console.WriteLine();

               foreach (Item item in itemLists)
               {
                    item.ShowInfo();
               }
               Console.WriteLine();

               Console.ForegroundColor = ConsoleColor.Yellow;
               Console.WriteLine("1. 장착 관리");
               Console.WriteLine("2. 아이템 정렬");
               Console.WriteLine("0. 나가기");
               Console.ForegroundColor = ConsoleColor.White;

               Console.WriteLine();
          }

          static void InventorySceneInput(byte input)
          {
               switch (input)
               {
                    case 0:
                         scene = Scene.StartScene;
                         break;
                    case 1:
                         scene = Scene.EquipmentScene;
                         break;
                    case 2:
                         scene = Scene.InventorySortScene;
                         break;
                    default:
                         Console.ForegroundColor = ConsoleColor.Red;
                         Console.WriteLine("잘못된 입력입니다.\n");
                         Console.ForegroundColor = ConsoleColor.White;
                         break;
               }
          }

          static void InventorySortScene()
          {
               Console.WriteLine("인벤토리 - 아이템 정렬");
               Console.WriteLine("보유 중인 아이템을 관리할 수 있습니다.");
               Console.WriteLine();

               Console.WriteLine("[아이템 목록]");
               Console.WriteLine();

               foreach (Item item in itemLists)
               {
                    item.ShowInfo();
               }
               Console.WriteLine();

               Console.ForegroundColor = ConsoleColor.Yellow;
               Console.WriteLine("1. 이름");
               Console.WriteLine("2. 장착순");
               Console.WriteLine("3. 공격력");
               Console.WriteLine("4. 방어력");
               Console.WriteLine("0. 나가기");
               Console.ForegroundColor = ConsoleColor.White;

               Console.WriteLine();
          }

          static void InventorySortSceneInput(byte input)
          {
               switch (input)
               {
                    // 정렬 타입 선택
                    case 0:
                         scene = Scene.InventoryScene;
                         break;
                    case 1:
                         // 이름순 정렬
                         // 람다식 버전
                         //itemLists.Sort((x, y) => x.Name.CompareTo(y.Name));

                         itemLists = itemLists.OrderBy(p => p.Name).ToList();
                         break;
                    case 2:
                         // 장착순 정렬

                         // 람다식 버전
                         //itemLists.Sort((x, y) => y.HasEquipped.CompareTo(x.HasEquipped));
                         // 람다식에서 x, y 위치에 따라 true먼저, false먼저 결정 가능

                         // LINQ .OrderBy버전
                         itemLists = itemLists.OrderByDescending(p => p.HasEquipped).ToList();
                         break;
                    case 3:
                         // ThenBy를 사용하면 조건을 추가할 수 있음
                         // ~~Descending을 사용하면 내림차순으로 바꿀 수 있음
                         // 기본 OrderBy, ThenBy는 오름차순
                         itemLists = itemLists.OrderByDescending(p => p.Type).ThenByDescending(p => p.Value).ToList();
                         break;
                    case 4:
                         itemLists = itemLists.OrderBy(p => p.Type).ThenByDescending(p => p.Value).ToList();
                         break;
                    default:
                         Console.ForegroundColor = ConsoleColor.Red;
                         Console.WriteLine("잘못된 입력입니다.\n");
                         Console.ForegroundColor = ConsoleColor.White;
                         break;
               }
          }

          static void EquipmentScene()
          {
               Console.WriteLine("인벤토리");
               Console.WriteLine("보유 중인 아이템을 관리할 수 있습니다.");

               Console.WriteLine();
               Console.WriteLine("[아이템 목록]");
               Console.WriteLine();

               int i = 1;
               foreach (Item item in itemLists)
               {
                    Console.Write($"- {i} ");
                    item.ShowInfo();
                    i++;
               }

               Console.WriteLine();

               Console.ForegroundColor = ConsoleColor.Yellow;
               Console.WriteLine("0. 나가기");
               Console.ForegroundColor = ConsoleColor.White;

               Console.WriteLine();
          }

          static void EquipmentSceneInput(byte input)
          {
               // 1이 들어오면 0으로
               int select = 0;

               if (input > 0)
                    select = (int)itemLists[input - 1].Type;


               // 장착하면 무조건 그 장비가 장착, 기존에 장착 중이던 장비는 해제
               switch (input)
               {
                    case 0:
                         scene = Scene.InventoryScene;
                         break;
                    // 무쇠 갑옷
                    case 1:
                         if (character.EquiptedItems[select] != null)
                              character.EquiptedItems[select].HasEquipped = false;

                         character.EquiptedItems[select] = itemLists[(int)ItemIndexes.IronArmor];
                         character.EquiptedItems[select].HasEquipped = true;
                         break;
                    // 스파르타 갑옷
                    case 2:
                         if (character.EquiptedItems[select] != null)
                              character.EquiptedItems[select].HasEquipped = false;

                         character.EquiptedItems[select] = itemLists[(int)ItemIndexes.SpartaArmor];
                         character.EquiptedItems[select].HasEquipped = true;
                         break;
                    // 스파르타 갑옷
                    case 3:
                         if (character.EquiptedItems[select] != null)
                              character.EquiptedItems[select].HasEquipped = false;

                         character.EquiptedItems[select] = itemLists[(int)ItemIndexes.SpartaSpear];
                         character.EquiptedItems[select].HasEquipped = true;
                         break;
                    // 낡은 검
                    case 4:
                         if (character.EquiptedItems[select] != null)
                              character.EquiptedItems[select].HasEquipped = false;

                         character.EquiptedItems[select] = itemLists[(int)ItemIndexes.OldSword];
                         character.EquiptedItems[select].HasEquipped = true;
                         break;

                    default:
                         Console.ForegroundColor = ConsoleColor.Red;
                         Console.WriteLine("잘못된 입력입니다.\n");
                         Console.ForegroundColor = ConsoleColor.White;
                         break;
               }
          }

          static void AdventureScene()
          {
               needStamina = 10;

               if (character.CheckStamina(needStamina) == false)
               {
                    Console.WriteLine("스태미나가 부족합니다.");
                    Console.WriteLine();

                    scene = Scene.StartScene;
                    StartScene();

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

               Console.ForegroundColor = ConsoleColor.Yellow;
               Console.WriteLine("1. 랜덤 모험");
               Console.WriteLine("0. 나가기");
               Console.ForegroundColor = ConsoleColor.White;

               Console.WriteLine();
          }

          static void AdventureSceneInput(byte input)
          {
               switch (input)
               {
                    case 0:
                         scene = Scene.StartScene;
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

          static void PatrolScene()
          {
               needStamina = 5;

               if (character.CheckStamina(needStamina) == false)
               {
                    Console.WriteLine("스태미나가 부족합니다.");
                    Console.WriteLine();

                    scene = Scene.StartScene;
                    StartScene();

                    return;
               }

               character.Stamina -= needStamina;

               int randNum = new Random().Next(0, 10);
               int gettingGold = 0;

               switch (randNum)
               {
                    case 0:
                         Console.WriteLine("마을 아이들이 모여있다. 간식을 사줘볼까?");
                         gettingGold = 500;
                         break;
                    case 1:
                         Console.WriteLine("촌장님을 만나서 심부름을 했다.");
                         gettingGold = 2000;
                         break;
                    case 2:
                    case 3:
                         Console.WriteLine("길 잃은 사람을 안내해주었다.");
                         gettingGold = 1000;
                         break;
                    case 4:
                    case 5:
                    case 6:
                         Console.WriteLine("마을 주민과 인사를 나눴다. 선물을 받았다.");
                         gettingGold = 500;
                         break;
                    case 7:
                    case 8:
                    case 9:
                         Console.WriteLine("아무 일도 일어나지 않았다.");
                         break;
               }

               if (gettingGold != 0)
               {
                    character.Gold += gettingGold;
                    Console.WriteLine($"{gettingGold}G를 얻었다.");
               }
               Console.WriteLine();

               Console.ForegroundColor = ConsoleColor.Yellow;
               Console.WriteLine("1. 마을 순찰하기");
               Console.WriteLine("0. 나가기");
               Console.ForegroundColor = ConsoleColor.White;

               Console.WriteLine();
          }

          static void PatrolSceneInput(byte input)
          {
               switch (input)
               {
                    case 0:
                         scene = Scene.StartScene;
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

          static void TrainingScene()
          {
               needStamina = 15;

               if (character.CheckStamina(needStamina) == false)
               {
                    Console.WriteLine("스태미나가 부족합니다.");
                    Console.WriteLine();

                    scene = Scene.StartScene;
                    StartScene();

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
               Console.ForegroundColor = ConsoleColor.Yellow;
               Console.WriteLine("1. 훈련하기");
               Console.WriteLine("0. 나가기");
               Console.ForegroundColor = ConsoleColor.White;

               Console.WriteLine();
          }

          static void TrainingSceneInput(byte input)
          {
               switch (input)
               {
                    case 0:
                         scene = Scene.StartScene;
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

          #endregion
     }
}
