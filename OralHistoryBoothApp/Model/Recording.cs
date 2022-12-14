using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using Windows.System;

namespace OralHistoryBoothApp.Model
{
    public class Recording
    {
        internal Action<object, PropertyChangedEventArgs> PropertyChanged;

        public string Name { get; set; }
        public string Decade { get; set; }
        public string Tag { get; set; }
        public string Student { get; set; }

    }

    //public class RecordingManager
    //{
    //    public static List<Recording> GetRecordings()
    //    {
    //            var records = new List<Recording>();
                

    //    }
    //}
}
