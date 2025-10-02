using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Threading.Tasks;

using TextRPG;
using static System.Formats.Asn1.AsnWriter;

namespace TextRPG
{
     internal class Character
     {
          // 기본 캐릭터 스탯
          private int maxLevel = 5;
          private int maxHp = 200;
          private int nowHp = 0;
          private int maxStamina = 100;
          private int nowStamina = 0;
          private int[] RequireExp = { 50, 80, 150, 500 };
          private float atk = 10;
          private float def = 5;

          public int Level { get; set; }
          public float Attack { get; set; }
          public float Defence { get; set; }


          public int Gold { get; set; }
          public int Exp { get; set; }
          public int Hp
          {
               get
               { return nowHp; }
               set
               {
                    nowHp = value;
                    if (nowHp > maxHp)
                         nowHp = maxHp;
                    else if (nowHp < 0)
                         nowHp = 0;
               }
          }
          public int Stamina
          {
               get
               { return nowStamina; }
               set
               {
                    nowStamina = value;
                    if (nowStamina > maxStamina)
                         nowStamina = maxStamina;
                    else if (nowStamina < 0)
                         nowStamina = 0;
               }
          }

          public string Name { get; set; }
          public string Job { get; set; }

          public List<Item> Inventory { get; set; }
          public List<Item> EquiptedItems { get; set; }

          public Character()
          {
               Name = "";
               Job = "";

               Inventory = new List<Item>();

               EquiptedItems = new List<Item>
               {
                    null!,
                    null!
               };
          }

          public Character(string name, string job)
          {
               Level = 1;
               Name = name;
               Job = job;
               Attack = atk;
               Defence = def;
               nowHp = maxHp;
               Gold = 1000;
               Exp = 0;
               nowStamina = maxStamina;

               Inventory = new List<Item>();

               EquiptedItems = new List<Item>
               {
                    null!,
                    null!
               };
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

               Console.Write($"공격력 : {(float)Attack} ");
               if (EquiptedItems[(int)ItemType.Weapon] != null)
               {
                    if (EquiptedItems[(int)ItemType.Weapon].Value > 0)
                    {
                         Console.ForegroundColor = ConsoleColor.Green;
                         Console.Write($"(+{EquiptedItems[(int)ItemType.Weapon].Value})");
                         Console.ForegroundColor = ConsoleColor.White;
                    }
                    Console.WriteLine();
               }


               Console.Write($"방어력 : {(float)Defence} ");
               if (EquiptedItems[(int)ItemType.Armor] != null)
               {
                    if (EquiptedItems[(int)ItemType.Armor].Value > 0)
                    {
                         Console.ForegroundColor = ConsoleColor.Green;
                         Console.Write($"(+{EquiptedItems[(int)ItemType.Armor].Value})");
                         Console.ForegroundColor = ConsoleColor.White;
                    }
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
               atk = 10 + (0.5f * (Level - 1));
               def = 5 + (1f * (Level - 1));

               if (EquiptedItems[(int)ItemType.Weapon] != null)
                    Attack = atk + EquiptedItems[(int)ItemType.Weapon].Value;
               else
                    Attack = atk;


               if (EquiptedItems[(int)ItemType.Armor] != null)
                    Defence = def + EquiptedItems[(int)ItemType.Armor].Value;
               else
                    Defence = def;


               if (Level < maxLevel)
               {
                    LevelUp();
               }
          }

          public void LevelUp()
          {
               int beforeLevel = Level;
               float beforeAtk = atk;
               float beforeDef = def;

               while (Exp > RequireExp[Level - 1])
               {
                    Exp -= RequireExp[Level - 1];
                    Level++;
                    atk += 0.5f;
                    def += 1f;

                    if (Exp > RequireExp[Level - 1])
                         continue;

                    Console.ForegroundColor = ConsoleColor.Cyan;

                    Console.WriteLine("레벨업!!");
                    Console.Write($"{"Level: ",-10}{beforeLevel,-5} => ");  // 이전 레벨
                    Console.WriteLine(Level);
                    Console.Write($"{"기본 공격력 : ",-10}{beforeAtk,-5} => ");  // 이전 공격력
                    Console.WriteLine(atk);
                    Console.Write($"{"기본 방어력 : ",-10}{beforeDef,-5} => ");  // 이전 방어력
                    Console.WriteLine(def);
                    Console.WriteLine();

                    Console.ForegroundColor = ConsoleColor.White;
               }

               //Console.ForegroundColor = ConsoleColor.Cyan;

               //Console.WriteLine("레벨업!!");
               //Console.Write($"{"Level: ",-10}{Level,-5} => ");
               //Exp -= RequireExp[Level - 1];
               //Level++;
               //Console.WriteLine(Level);

               //Console.Write($"{"기본 공격력 : ",-10}{atk,-5} => ");
               //atk += 0.5f;
               //Console.WriteLine(atk);

               //Console.Write($"{"기본 방어력 : ",-10}{def,-5} => ");
               //def += 1f;
               //Console.WriteLine(def);
               //Console.WriteLine();

               //Console.ForegroundColor = ConsoleColor.White;
          }
     }
}
