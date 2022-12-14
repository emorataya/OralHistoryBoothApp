using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OralHistoryBoothApp.Model
{
    internal class RecordingsInfo
    {
        public string Name { get; set; }
        public string Decade { get; set; }
        public string Tag { get; set; }
        public string Student { get; set; }
        public List<Recording> recordings { get; set; }

        public RecordingsInfo()
        {
            Name = "Admin Recording List";

            recordings = new List<Recording>()
            {
                new Recording {Name = "", Decade= "1990", Student ="is", Tag= "chapel"}
            };
        }
        //, Decade= 10, Student=true, Tag="Chapel"
    }
}
