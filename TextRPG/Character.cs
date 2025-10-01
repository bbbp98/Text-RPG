using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;

using TextRPG;
using static System.Formats.Asn1.AsnWriter;

namespace TextRPG
{
     internal class Character
     {
          // 기본 캐릭터 스탯
          private int atk = 10;
          private int def = 5;

          int Level { get; set; }
          public int Attack { get; set; }
          public int Defence { get; set; }
          public int Hp { get; set; }

          public int Gold { get; set; }
          public int Exp { get; set; }
          public int Stamina { get; set; }


          string Name { get; set; }
          string Job { get; set; }

          public List<Item> EquiptedItems { get; set; }

          public Character(string name, string job)
          {
               Level = 1;
               Name = name;
               Job = job;
               Attack = atk;
               Defence = def;
               Hp = 100;
               Gold = 15000;
               Exp = 0;
               Stamina = 100;

               EquiptedItems = new List<Item>();
          }

          public void ShowInfo()
          {
               foreach (Item item in EquiptedItems)
               {
                    if (item != null && item.Type == ItemType.Weapon)
                         Attack = atk + item.Value;

                    if (item != null && item.Type == ItemType.Armor)
                         Defence = def + item.Value;
               }

               Console.WriteLine($"Lv. {Level}");
               Console.WriteLine($"{Name} ( {Job} )");

               Console.Write($"공격력 : {Attack} ");
               if (EquiptedItems[(int)ItemType.Weapon].Value > 0)
               {
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.Write($"(+{EquiptedItems[(int)ItemType.Weapon].Value})");
                    Console.ForegroundColor = ConsoleColor.White;
               }
               Console.WriteLine();

               Console.Write($"방어력 : {Defence} ");
               if (EquiptedItems[(int)ItemType.Armor].Value > 0)
               {
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.Write($"(+{EquiptedItems[(int)ItemType.Armor].Value})");
                    Console.ForegroundColor = ConsoleColor.White;
               }
               Console.WriteLine();

               Console.WriteLine($"체  력 : {Hp}");
               Console.WriteLine($"Gold : {Gold}");
               Console.WriteLine($"스태미나 : {Stamina}");
               Console.WriteLine();
          }

          public bool CheckStamina(int stamina)
          {
               return (Stamina >= stamina);
          }

          public void Update()
          {
               Attack = atk + EquiptedItems[(int)ItemType.Weapon].Value;
               Defence = def + EquiptedItems[(int)ItemType.Armor].Value;
          }
     }
}
