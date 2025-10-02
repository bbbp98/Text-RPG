using System.Runtime.CompilerServices;
using System.Security.AccessControl;
using System.Security.Principal;

namespace TextRPG
{
     internal class Program
     {
          public enum Scene
          {
               StartScene,
               PlayerInfoScene,
               InventoryScene,
               InventorySortScene,
               EquipmentScene,
               AdventureScene,
               PatrolScene,
               TrainingScene,
               ShopScene,
               PurchaseItemScene,
               SellingItemScene,
               DungeonEntranceScene,
               DungeonClearScene,
               RestScene,
          }

          const string WELCOME_MESSAGE =
          "스파르타 마을에 오신 여러분 환영합니다.\n" +
          "이곳에서 던전으로 들어가기 전 활동을 할 수 있습니다.\n";

          static private bool isRunning = true;
          static private Scene scene = Scene.StartScene;

          static Character character = new Character("Chad", "전사");

          // dungeon list
          static List<Dungeon> dungeons = new List<Dungeon>();
          static Dungeon nowDungeon;

          // item list
          static List<Item> inventory = new List<Item>();
          static List<Item> items = new List<Item>();

          static int needStamina = 0;

          static void Main(string[] args)
          {
               // dungeon initialize
               foreach (Difficulty diff in Enum.GetValues(typeof(Difficulty)))
               {
                    dungeons.Add(new Dungeon(diff));
               }

               // item 집어넣기
               foreach (ItemIndexes idx in Enum.GetValues(typeof(ItemIndexes)))
               {
                    if ((int)idx < Enum.GetValues(typeof(ItemIndexes)).Length / 2)
                         items.Add(new Item(ItemType.Armor, idx));
                    else
                         items.Add(new Item(ItemType.Weapon, idx));
               }

               // 기본 장비 장착

               inventory.Add(items[(int)ItemIndexes.NoviceArmor]);
               inventory[0].HasItem = true;
               inventory[0].IsEquipped = true;
               inventory.Add(items[(int)ItemIndexes.OldSword]);
               inventory[1].HasItem = true;
               inventory[1].IsEquipped = true;

               character.EquiptedItems.Add(inventory[0]);
               character.EquiptedItems.Add(inventory[1]);

               while (isRunning)
               {
                    PrintBehaviorList();
                    InputBehavior();
                    character.Update();
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
                    case Scene.ShopScene:
                         ShopScene();
                         break;
                    case Scene.PurchaseItemScene:
                         PurchaseItemScene();
                         break;
                    case Scene.SellingItemScene:
                         SellingItemScene();
                         break;
                    case Scene.DungeonEntranceScene:
                         DungeonEntranceScene();
                         break;
                    case Scene.DungeonClearScene:
                         DungeonClearScene();
                         break;
                    case Scene.RestScene:
                         RestScene();
                         break;
               }


               Console.WriteLine("원하시는 행동을 입력해주세요.");
               Console.Write(">> ");
          }

          static void InputBehavior()
          {
               byte input = 0;

               // 인풋 체크: 이상한 문자 받았을 때 default로 실행
               if (byte.TryParse(Console.ReadLine(), out input) == false)
                    input = byte.MaxValue;


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
                    case Scene.ShopScene:
                         ShopSceneInput(input);
                         break;
                    case Scene.PurchaseItemScene:
                         PurchaseItemSceneInput(input);
                         break;
                    case Scene.SellingItemScene:
                         SellingItemSceneInput(input);
                         break;
                    case Scene.DungeonEntranceScene:
                         DungeonEntranceSceneInput(input);
                         break;
                    case Scene.DungeonClearScene:
                         DungeonClearSceneInput(input);
                         break;
                    case Scene.RestScene:
                         RestSceneInput(input);
                         break;
               }
          }


          static void StartScene()
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
               Console.WriteLine("0. 종료하기");

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
                    case 6:
                         scene = Scene.ShopScene;
                         break;
                    case 7:
                         scene = Scene.DungeonEntranceScene;
                         break;
                    case 8:
                         scene = Scene.RestScene;
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

               Console.WriteLine("0. 나가기");
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

               foreach (Item item in inventory)
               {
                    item.ShowInfo();
               }
               Console.WriteLine();

               Console.WriteLine("1. 장착 관리");
               Console.WriteLine("2. 아이템 정렬");
               Console.WriteLine("0. 나가기");
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

               foreach (Item item in inventory)
               {
                    item.ShowInfo();
               }
               Console.WriteLine();

               Console.WriteLine("1. 이름");
               Console.WriteLine("2. 장착순");
               Console.WriteLine("3. 공격력");
               Console.WriteLine("4. 방어력");
               Console.WriteLine("0. 나가기");
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

                         inventory = inventory.OrderBy(p => p.Name).ToList();
                         break;
                    case 2:
                         // 장착순 정렬

                         // 람다식 버전
                         //itemLists.Sort((x, y) => y.HasEquipped.CompareTo(x.HasEquipped));
                         // 람다식에서 x, y 위치에 따라 true먼저, false먼저 결정 가능

                         // LINQ .OrderBy버전
                         inventory = inventory.OrderByDescending(p => p.IsEquipped).ToList();
                         break;
                    case 3:
                         // ThenBy를 사용하면 조건을 추가할 수 있음
                         // ~~Descending을 사용하면 내림차순으로 바꿀 수 있음
                         // 기본 OrderBy, ThenBy는 오름차순
                         inventory = inventory.OrderByDescending(p => p.Type).ThenByDescending(p => p.Value).ToList();
                         break;
                    case 4:
                         inventory = inventory.OrderBy(p => p.Type).ThenByDescending(p => p.Value).ToList();
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
               foreach (Item item in inventory)
               {
                    Console.Write($"- {i++} ");
                    item.ShowInfo();
               }

               Console.WriteLine();

               Console.WriteLine("0. 나가기");
               Console.WriteLine();
          }


          static void EquipmentSceneInput(byte input)
          {
               // 1이 들어오면 0으로
               int select = 0;

               if (input > 0)
                    select = (int)inventory[input - 1].Type;

               // 장착하면 무조건 그 장비가 장착, 기존에 장착 중이던 장비는 해제
               switch (input)
               {
                    case 0:
                         scene = Scene.InventoryScene;
                         break;
                    case 1:
                    case 2:
                    case 3:
                    case 4:
                    case 5:
                    case 6:
                         // 장비 교체
                         if (character.EquiptedItems[select] != null) // character.EquiptedItems에 null이 있을 때 처리
                              character.EquiptedItems[select].IsEquipped = false;
                         character.EquiptedItems[select] = inventory[input - 1];
                         character.EquiptedItems[select].IsEquipped = true;
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

               Console.WriteLine("1. 랜덤 모험");
               Console.WriteLine("0. 나가기");
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

               Console.WriteLine("1. 마을 순찰하기");
               Console.WriteLine("0. 나가기");
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
               Console.WriteLine("1. 훈련하기");
               Console.WriteLine("0. 나가기");
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


          static void ShopScene()
          {
               Console.WriteLine("상점");
               Console.WriteLine("필요한 아이템을 얻을 수 있는 상점입니다.");
               Console.WriteLine();

               Console.WriteLine("[보유 골드]");
               Console.ForegroundColor = ConsoleColor.Yellow;
               Console.WriteLine($"{character.Gold} G");
               Console.ForegroundColor = ConsoleColor.White;
               Console.WriteLine();

               Console.WriteLine("[아이템 목록]");
               foreach (Item item in items)
               {
                    Console.Write("- ");
                    item.ShowInfo(scene);
               }
               Console.WriteLine();

               Console.WriteLine("1. 아이템 구매");
               Console.WriteLine("2. 아이템 판매");
               Console.WriteLine("0. 나가기");
               Console.WriteLine();
          }

          static void ShopSceneInput(byte input)
          {
               switch (input)
               {
                    case 0:
                         scene = Scene.StartScene;
                         break;
                    case 1:
                         scene = Scene.PurchaseItemScene;
                         break;
                    case 2:
                         scene = Scene.SellingItemScene;
                         break;
                    default:
                         Console.ForegroundColor = ConsoleColor.Red;
                         Console.WriteLine("잘못된 입력입니다.\n");
                         Console.ForegroundColor = ConsoleColor.White;
                         break;
               }
          }


          static void PurchaseItemScene()
          {
               Console.WriteLine("상점 - 아이템 구매");
               Console.WriteLine("필요한 아이템을 얻을 수 있는 상점입니다.");
               Console.WriteLine();

               Console.WriteLine("[보유 골드]");
               Console.ForegroundColor = ConsoleColor.Yellow;
               Console.WriteLine($"{character.Gold} G");
               Console.ForegroundColor = ConsoleColor.White;
               Console.WriteLine();

               Console.WriteLine("[아이템 목록]");
               int i = 1;
               foreach (Item item in items)
               {
                    Console.Write($"- {i++} ");
                    item.ShowInfo(scene);
               }
               Console.WriteLine();

               Console.WriteLine("0. 나가기");
               Console.WriteLine();
          }


          static void PurchaseItemSceneInput(byte input)
          {
               switch (input)
               {
                    case 0:
                         scene = Scene.ShopScene;
                         break;
                    case 1:
                    case 2:
                    case 3:
                    case 4:
                    case 5:
                    case 6:
                         Purchase(input);
                         break;
                    default:
                         Console.ForegroundColor = ConsoleColor.Red;
                         Console.WriteLine("잘못된 입력입니다.\n");
                         Console.ForegroundColor = ConsoleColor.White;
                         break;
               }
          }

          static void Purchase(byte input)
          {
               Item item = items[input - 1];

               // 아이템 보유 체크
               if (item.HasItem)
               {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine($"이미 {item.Name}을(를) 보유하고 있습니다.");
                    Console.ForegroundColor = ConsoleColor.White;
                    Console.WriteLine();
               }
               else
               {
                    // 보유 금액 체크
                    if (character.Gold >= item.Price)
                    {
                         character.Gold -= item.Price;

                         item.HasItem = true;
                         inventory.Add(item);
                         Console.WriteLine($"{item.Name}을(를) 구매했습니다.");
                         Console.WriteLine();
                    }
                    else
                    {
                         Console.ForegroundColor = ConsoleColor.Red;
                         Console.WriteLine("보유 금액이 부족합니다.");
                         Console.ForegroundColor = ConsoleColor.White;
                         Console.WriteLine();
                    }
               }
          }


          static void SellingItemScene()
          {
               Console.WriteLine("상점 - 아이템 판매");
               Console.WriteLine("필요한 아이템을 판매할 수 있는 상점입니다.");
               Console.WriteLine();

               Console.WriteLine("[보유 골드]");
               Console.ForegroundColor = ConsoleColor.Yellow;
               Console.WriteLine($"{character.Gold} G");
               Console.ForegroundColor = ConsoleColor.White;
               Console.WriteLine();

               Console.WriteLine("[아이템 목록]");
               int i = 1;
               foreach (Item item in inventory)
               {
                    Console.Write($"- {i++} ");
                    item.ShowInfo(scene);
               }
               Console.WriteLine();

               Console.WriteLine("0. 나가기");
               Console.WriteLine();
          }


          static void SellingItemSceneInput(byte input)
          {
               switch (input)
               {
                    case 0:
                         scene = Scene.ShopScene;
                         break;
                    case 1:
                    case 2:
                    case 3:
                    case 4:
                    case 5:
                    case 6:
                         // 판매 메서드
                         Selling(input);
                         break;
                    default:
                         Console.ForegroundColor = ConsoleColor.Red;
                         Console.WriteLine("잘못된 입력입니다.\n");
                         Console.ForegroundColor = ConsoleColor.White;
                         break;
               }
          }


          static void Selling(byte input)
          {
               // 인벤토리에 아이템이 없는데 입력이 들어올 경우
               if (inventory.Count == 0)
               {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("판매할 수 있는 아이템이 없습니다.");
                    Console.ForegroundColor = ConsoleColor.White;
                    return;
               }


               Item item = inventory[input - 1];

               int typeIdx = (int)item.Type;
               // 장착중인 아이템인지 체크
               // 판매는 그대로 진행, 장착 해제
               if (character.EquiptedItems[typeIdx] == item)
               {
                    item.IsEquipped = false;
                    item.HasItem = false;
                    character.EquiptedItems[typeIdx] = null;
               }

               int price = (int)(item.Price * 0.85f);
               character.Gold += price;
               inventory.Remove(item);
               Console.WriteLine($"{item.Name}을(를) 판매했습니다. {price} G를 획득했습니다.");
               Console.WriteLine();
          }


          static void DungeonEntranceScene()
          {
               Console.WriteLine("던전입장");
               Console.WriteLine("이곳에서 던전으로 들어가기전 활동을 할 수 있습니다.");
               Console.WriteLine();

               // 난이도 별 던전 
               int i = 1;
               foreach (Dungeon dungeon in dungeons)
               {
                    Console.Write($"{i++}. ");
                    dungeon.ShowInfo();
               }

               Console.WriteLine("0. 나가기");
               Console.WriteLine();
          }


          static void DungeonEntranceSceneInput(byte input)
          {
               switch (input)
               {
                    case 0:
                         scene = Scene.StartScene;
                         break;
                    case 1:
                    case 2:
                    case 3:
                         // load dungeon clear scene
                         scene = Scene.DungeonClearScene;
                         nowDungeon = dungeons[input - 1];
                         break;
                    default:
                         Console.ForegroundColor = ConsoleColor.Red;
                         Console.WriteLine("잘못된 입력입니다.\n");
                         Console.ForegroundColor = ConsoleColor.White;
                         break;
               }
          }

          static void DungeonClearScene()
          {
               Random rand = new Random();
               // 20 ~ 35 랜덤으로 체력 감소
               int hpDecrease = rand.Next(20, 36);
               float defDiff = nowDungeon.RequiredDef - character.Defence;
               // hpDecrease : 감소될 체력량
               hpDecrease += (int)defDiff;

               // 던전 입장으로 소모되는 체력
               int hpBefore = character.Hp;
               character.Hp -= hpDecrease;

               //if (character.Hp < hpDecrease)
               //{
               //     Console.ForegroundColor = ConsoleColor.Red;
               //     Console.WriteLine("체력이 없습니다.");
               //     Console.WriteLine();
               //     Console.ForegroundColor = ConsoleColor.White;

               //     scene = Scene.DungeonEntranceScene;
               //     DungeonEntranceScene();

               //     return;
               //}

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
                         Console.WriteLine($"{nowDungeon.Name}을 클리어 하지 못했습니다.");
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
               bonus = nowDungeon.RewardGold * bonus;
               int clearGold = nowDungeon.RewardGold + (int)bonus;

               int goldBefore = character.Gold;
               character.Gold += clearGold;
               character.Exp += nowDungeon.RewardExp;

               Console.WriteLine("던전 클리어");
               Console.WriteLine("축하합니다!!");
               Console.WriteLine($"{nowDungeon.Name}을 클리어 하였습니다");
               Console.WriteLine();

               Console.WriteLine("[탐험 결과]");
               Console.WriteLine($"체력 {hpBefore} -> {character.Hp}");
               Console.WriteLine($"Gold {goldBefore} G -> {character.Gold} G");
               Console.WriteLine();

               Console.WriteLine("1. 다시하기");
               Console.WriteLine("0. 나가기");
               Console.WriteLine();
          }

          static void DungeonClearSceneInput(byte input)
          {
               switch (input)
               {
                    case 0:
                         scene = Scene.DungeonEntranceScene;
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

          static void RestScene()
          {
               int price = 500;

               Console.WriteLine("휴식하기");
               Console.Write($"{price} G 를 내면 체력을 회복할 수 있습니다.");
               Console.WriteLine($" (보유 골드 : {character.Gold} G)");
               Console.WriteLine();

               Console.WriteLine("1. 휴식하기");
               Console.WriteLine("0. 나가기");
               Console.WriteLine();
          }

          static void RestSceneInput(byte input)
          {
               int price = 500;

               switch (input)
               {
                    case 0:
                         scene = Scene.StartScene;
                         break;
                    case 1:
                         if (character.Gold >= price)
                         {
                              Console.WriteLine("휴식을 완료했습니다.");
                              Console.WriteLine();
                              character.Gold -= price;
                              character.Hp += 100;
                              character.Stamina += 20;
                         }
                         else
                         {
                              Console.ForegroundColor = ConsoleColor.Red;
                              Console.WriteLine("Gold가 부족합니다.");
                              Console.ForegroundColor = ConsoleColor.White; ;
                              Console.WriteLine();
                         }
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
