using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TextRPG
{
     public enum Difficulty
     {
          Easy,
          Normal,
          Hard,
     }

     internal class DungeonData
     {
          public string[] Names =
          {
               "쉬운 던전",
               "일반 던전",
               "어려운 던전",
          };

          public int[] RequiredDefs =
          {
               5, //"쉬운 던전",
               11, //"일반 던전",
               17, //"어려운 던전",
          };

          public int[] RewardGolds =
          {
               1000, //"쉬운 던전",
               1700, //"일반 던전",
               2500, //"어려운 던전",
          };

          public int[] RewardExps =
          {
               50, //"쉬운 던전",
               100, //"일반 던전",
               200, //"어려운 던전",
          };
     }
}
