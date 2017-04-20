using System;
using System.Collections.Generic;
using System.Text;

namespace Dal.Model
{
    public class StatisticViewModel
    {
        public Dictionary<string,int> AllTopScore { get; set; }

        public Dictionary<string, int> TopScore { get; set; }

        public string MostPoplularQuiz { get; set; }

        public KeyValuePair<string,string> HardestAndEasiestQuestion { get; set; }
    }
}
