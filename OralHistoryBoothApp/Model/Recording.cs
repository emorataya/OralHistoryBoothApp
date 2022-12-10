using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.System;

namespace OralHistoryBoothApp.Model
{
    internal class Recording
    {
        public string Name { get; set; }
        public int Decade { get; set; }
        public string Tag { get; set; }
        public bool Student { get; set; }
    }
}
