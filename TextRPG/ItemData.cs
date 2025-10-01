using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TextRPG
{
     public enum ItemIndexes
     {
          IronArmor,
          SpartaArmor,
          SpartaSpear,
          OldSword,
     }

     internal class ItemData
     {
          public string[] itemNames =
          {
               "무쇠갑옷",
               "스파르타의 갑옷",
               "스파르타의 창",
               "낡은 검",
          };

          public string[] itemDescriptions =
          {
               "무쇠로 만들어져 튼튼한 갑옷입니다.",
               "스파르타의 전사들이 사용했다는 전설의 갑옷입니다.",
               "스파르타의 전사들이 사용했다는 전설의 창입니다.",
               "쉽게 볼 수 있는 낡은 검 입니다.",
          };

          public int[] itemValues =
          {
               5,
               10,
               7,
               2,
          };

          public ItemIndexes index;
     }
}
