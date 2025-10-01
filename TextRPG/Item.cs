using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TextRPG
{
     public enum ItemType
     {
          Armor,
          Weapon,
     }

     public enum ItemIndexes
     {
          IronArmor,
          SpartaArmor,
          SpartaSpear,
          OldSword,
     }

     internal class Item
     {
          public string Name { get; set; }
          public ItemType Type { get; set; }
          public int Value { get; set; }
          string Description { get; set; }

          public bool HasEquipped { get; set; }

          public Item(string name, ItemType type, int value, string description)
          {
               Name = name;
               Type = type;
               Value = value;
               Description = description;
               HasEquipped = false;
          }

          public void ShowInfo()
          {

               string[] valueType =
               {
                         "방어력",
                         "공격력",
                    };

               if (HasEquipped)
                    Console.Write($"{"[E]" + Name,-10}| ");
               else
                    Console.Write($"{Name,-10}| ");
               Console.Write($"{valueType[(int)Type] + " +" + Value,-10}| ");
               Console.Write($"{Description,-30}");
               Console.WriteLine();
          }
     }
}
