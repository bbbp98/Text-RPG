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
          private int maxHp = 200;
          private int hp = 0;
          private int maxStamina = 100;
          private int stamina = 0;
          private float atk = 10;
          private float def = 5;
          int[] RequireExp = { 50, 80, 150, 500 };

          int Level { get; set; }
          public float Attack { get; set; }
          public float Defence { get; set; }


          public int Gold { get; set; }
          public int Exp { get; set; }
          public int Hp
          {
               get
               { return hp; }
               set
               {
                    hp = value;
                    if (hp > maxHp)
                         hp = maxHp;
                    else if (hp < 0)
                         hp = 0;
               }
          }
          public int Stamina
          {
               get
               { return stamina; }
               set
               {
                    stamina = value;
                    if (stamina > maxStamina)
                         stamina = maxStamina;
                    else if (stamina < 0)
                         stamina = 0;
               }
          }

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
               hp = maxHp;
               //hp = 10;
               Gold = 1000;
               Exp = 0;
               stamina = maxStamina;
               //stamina = 10;

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

               if (Exp > RequireExp[Level - 1])
               {
                    LevelUp();
               }
          }

          private void LevelUp()
          {
               Exp -= RequireExp[Level - 1];
               Level++;

               atk += 0.5f;
               def += 1f;
          }
     }
}
