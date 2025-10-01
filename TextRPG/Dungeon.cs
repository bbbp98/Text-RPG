using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TextRPG
{


     internal class Dungeon
     {
          public Difficulty Difficulty { get; set; }
          public string Name { get; set; }
          public int RequiredDef { get; set; }
          public int RewardGold { get; set; }
          public int RewardExp { get; set; }

          public Dungeon(Difficulty difficulty)
          {
               DungeonData data = new DungeonData();

               Difficulty = difficulty;
               Name = data.Names[(int)Difficulty];
               RequiredDef = data.RequiredDefs[(int)Difficulty];
               RewardGold = data.RewardGolds[(int)Difficulty];
               RewardExp = data.RewardExps[(int)Difficulty];
          }

          public void ShowInfo()
          {
               Console.Write($"{Name,-10}| ");
               Console.WriteLine($"방어력 {RequiredDef} 이상 권장");
          }
     }
}
