using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using TextRPG;
using static System.Formats.Asn1.AsnWriter;

namespace TextRPG
{
     internal class Character
     {
          int Level { get; set; }
          int Attack { get; set; }
          int EquipmentAttack { get; set; }
          int EquipmentDepence { get; set; }
          int Depence { get; set; }
          int Hp { get; set; }
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
               Attack = 10;
               Depence = 5;
               Hp = 100;
               Gold = 1500;
               EquipmentAttack = 0;
               EquipmentDepence = 0;
               Exp = 0;
               Stamina = 100;

               EquiptedItems = new List<Item>();
          }

          public void ShowInfo()
          {
               EquipmentAttack = 0;
               EquipmentDepence = 0;

               foreach (Item item in EquiptedItems)
               {
                    if (item != null && item.Type == ItemType.Weapon)
                         EquipmentAttack += item.Value;

                    if (item != null && item.Type == ItemType.Armor)
                         EquipmentDepence += item.Value;
               }

               Console.WriteLine($"Lv. {Level}");
               Console.WriteLine($"{Name} ( {Job} )");

               Console.Write($"공격력 : {Attack} ");
               if (EquipmentAttack > 0)
                    Console.Write($"(+{EquipmentAttack})");
               Console.WriteLine();

               Console.Write($"방어력 : {Depence} ");
               if (EquipmentDepence > 0)
                    Console.Write($"(+{EquipmentDepence})");
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
     }
}
