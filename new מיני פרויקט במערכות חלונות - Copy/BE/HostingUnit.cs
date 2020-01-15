using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BE
{
   public class HostingUnit
   {
        public long HostingUnitKey { get; set; }
        public Host Owner { get; set; }
        public string HostingUnitName { get; set; }
        public Areas Area { get; set; }
        public String SubArea { get; set; }
        public bool[,] Diary { get; set; }
        public uint NumOfSingleBeds { get; set; }
        public uint NumOfDoubleBeds { get; set; }

        public bool ChildrensAttractions { get; set; }
        public bool Garden { get; set; }
        public bool Jacuzzi { get; set; }
        public bool Pool { get; set; }

        public List<DateTime> BookedDays { get; set; }
        string DiaryToString()
        {
            string str = string.Empty;
            bool flag = true;
            for (int month = 1; month <= 12; month++)
            {
                for (int day = 1; day <= DateTime.DaysInMonth(2020, month); day++)
                {
                    if (flag && Diary[month - 1, day - 1])
                    {
                        str += day + " / " + month + " / " + "2020" + " - ";
                        flag = false;
                    }
                    if (!flag && !Diary[month - 1, day - 1])
                    {
                        str += day + " / " + month + " / " + "2020" + "/n";
                        flag = true;
                    }
                }
            }
            return str;
        }

        public override string ToString()
        {
            return "hosting unit key: " + HostingUnitKey.ToString() + "\n" +
                  "hosting unit name: " + HostingUnitName.ToString() + "\n" +
                  "hosting unit area: " + Area.ToString() + "\n" +
                  "hosting unit sub area: " + SubArea + "\n" +
                   DiaryToString();                      
        }
   }
}
