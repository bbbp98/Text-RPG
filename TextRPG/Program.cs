using System.Numerics;
using System.Runtime.CompilerServices;
using System.Security.AccessControl;
using System.Security.Principal;
using System.Text;
using System.Text.Json;
using System.Xml.Linq;
using TextRPG.Scenes;

namespace TextRPG
{
     internal class Program
     {
          public enum SceneNames
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

          private const string playerSaveData = "player.json";
          private const string itemSaveData = "items.json";

          static private bool isRunning = true;

          static private Character character = new Character();

          // scene list
          static private Scene? currentScene;

          // dungeon list
          static private List<Dungeon> dungeons = new List<Dungeon>();
          static private Dungeon nowDungeon = new Dungeon();

          // item list
          static private List<Item> items = new List<Item>();

          static void Main(string[] args)
          {
               // print korean error fix code
               Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);
               var cp949 = Encoding.GetEncoding(949);
               Console.InputEncoding = cp949;
               Console.OutputEncoding = cp949;

               Initialize();

               while (isRunning)
               {
                    character.Update();
                    currentScene!.Show();
                    Console.WriteLine();
                    Console.WriteLine("원하시는 행동을 입력해주세요.");
                    Console.Write(">> ");
                    byte input = byte.TryParse(Console.ReadLine(), out byte val) ? val : byte.MaxValue;  // check input, if TryParse return false => go to case default
                    Console.Clear();
                    currentScene.HandleInput(input);
               }
          }

          static private void Initialize()
          {
               // dungeon initialize
               foreach (Difficulty diff in Enum.GetValues(typeof(Difficulty)))
               {
                    dungeons.Add(new Dungeon(diff));
               }

               // items initialize
               if (File.Exists(itemSaveData))
               {
                    string json = File.ReadAllText(itemSaveData);
                    items = JsonSerializer.Deserialize<List<Item>>(json)!;
               }
               else
               {
                    foreach (ItemIndexes idx in Enum.GetValues(typeof(ItemIndexes)))
                    {
                         if ((int)idx < Enum.GetValues(typeof(ItemIndexes)).Length / 2)
                              items.Add(new Item(ItemType.Armor, idx));
                         else
                              items.Add(new Item(ItemType.Weapon, idx));
                    }
               }

               // character initialize
               if (File.Exists(playerSaveData))
               {
                    string json = File.ReadAllText(playerSaveData);
                    character = JsonSerializer.Deserialize<Character>(json)!;
               }
               else
               {
                    Console.Write("이름을 입력하세요: ");
                    string name = Console.ReadLine() ?? "";

                    Console.Write("직업을 입력하세요: ");
                    string job = Console.ReadLine() ?? "";

                    character = new Character(name, job);
                    character.Inventory.Add(items[(int)ItemIndexes.NoviceArmor]);
                    character.Inventory[0].HasItem = true;
                    character.Inventory[0].IsEquipped = true;

                    character.Inventory.Add(items[(int)ItemIndexes.OldSword]);
                    character.Inventory[1].HasItem = true;
                    character.Inventory[1].IsEquipped = true;

                    character.EquiptedItems[(int)ItemType.Armor] = character.Inventory[0];
                    character.EquiptedItems[(int)ItemType.Weapon] = character.Inventory[1];

               }

               Console.ForegroundColor = ConsoleColor.White;
               Console.Clear();

               // scene initialize
               currentScene = new StartScene(character);
          }

          static public Scene GetCurrentScene()
          {
               return currentScene!;
          }

          static public void SetScene(Scene scene)
          {
               currentScene = scene;
          }

          static public List<Item> GetItems()
          {
               return items;
          }

          static public List<Dungeon> GetDungeons()
          {
               return dungeons;
          }

          static public Dungeon GetNowDungeon()
          {
               return nowDungeon;
          }

          static public void SetNowDungeon(Dungeon dungeon)
          {
               nowDungeon = dungeon;
          }

          static public void SetRunning(bool able)
          {
               isRunning = able;
          }

          static public void Save()
          {
               // save character infomation
               string saveJson = JsonSerializer.Serialize(character, new JsonSerializerOptions
               {
                    WriteIndented = true // JSON 보기 좋게 저장
               });
               File.WriteAllText(playerSaveData, saveJson);

               // save items
               saveJson = JsonSerializer.Serialize(items, new JsonSerializerOptions
               {
                    WriteIndented = true
               });
               File.WriteAllText(itemSaveData, saveJson);
          }
     }
}
