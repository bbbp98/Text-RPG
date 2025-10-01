using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TextRPG
{
     public enum ItemIndexes
     {
          NovieArmor,
          IronArmor,
          SpartaArmor,
          OldSword,
          bronzeAx,
          SpartaSpear,
     }
     public enum ItemType
     {
          Armor,
          Weapon,
     }

     internal class ItemData
     {
          public string[] itemNames =
          {
               "수련자 갑옷",
               "무쇠 갑옷",
               "스파르타의 갑옷",
               "낡은 검",
               "청동 도끼",
               "스파르타의 창",
          };

          public string[] itemDescriptions =
          {
               "수련에 도움을 주는 갑옷입니다.",
               "무쇠로 만들어져 튼튼한 갑옷입니다.",
               "스파르타의 전사들이 사용했다는 전설의 갑옷입니다.",
               "쉽게 볼 수 있는 낡은 검 입니다.",
               "어디선가 사용됐던거 같은 도끼입니다.",
               "스파르타의 전사들이 사용했다는 전설의 창입니다.",
          };

          public int[] itemValues =
          {
               5,   // "수련자 갑옷"
               9,   // "무쇠 갑옷",
               10,  // "스파르타의 갑옷",
               2,   // "낡은 검",
               5,   // "청동 도끼"
               7,   // "스파르타의 창",
          };

          public int[] itemPrices =
          {
               1000,   // "수련자 갑옷"
               2000,   // "무쇠 갑옷",
               3500,  // "스파르타의 갑옷",
               600,   // "낡은 검",
               1500,   // "청동 도끼"
               3000,   // "스파르타의 창",
          };
     }
}
