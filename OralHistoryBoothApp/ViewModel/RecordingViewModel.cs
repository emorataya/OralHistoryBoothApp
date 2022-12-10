using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OralHistoryBoothApp.Model
{
    internal class RecordingViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        private Recording recording;

        public RecordingViewModel(){
            this.recording = new Recording();
        }

        public string Name
        {
            get { return recording.Name; }
            set
            {
                recording.Name = value;
                OnPropertyChanged("Name");
            }
        }

        private void OnPropertyChanged(string property)
        {
            // Notify any controls bound to the ViewModel that the property changed
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(property));
        }
}
}
