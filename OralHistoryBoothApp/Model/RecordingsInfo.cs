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
        public List<Recording> recordings { get; set; }

        public RecordingsInfo()
        {
            Name = "Admin Recording List";

            recordings = new List<Recording>()
            {
                new Recording {Name = "Karen Carballo"}
            };
        }
        //, Decade= 10, Student=true, Tag="Chapel"
    }
}
