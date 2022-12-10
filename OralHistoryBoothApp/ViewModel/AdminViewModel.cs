using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OralHistoryBoothApp.Model;

namespace OralHistoryBoothApp.ViewModel
{
    internal class AdminViewModel : INotifyPropertyChanged
    {

        public event PropertyChangedEventHandler PropertyChanged;

        private RecordingsInfo recordingsinfo;

        public ObservableCollection<RecordingViewModel> AdminInfo { get; set; }

        public string Name
        {
            get { return recordingsinfo.Name; }
            set
            {
                recordingsinfo.Name = value;
                OnPropertyChanged(this, new PropertyChangedEventArgs("Name"));
            }
        }
        public AdminViewModel()
        {
            this.recordingsinfo = new RecordingsInfo();
            AdminInfo = new ObservableCollection<RecordingViewModel>();

            foreach (var admin in recordingsinfo.recordings)
            {
                var newMovie = new RecordingViewModel { Name = admin.Name };
                newMovie.PropertyChanged += OnPropertyChanged;
                AdminInfo.Add(newMovie);
            }
        }
        private void OnPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            // Theater name or MovieViewModel changed, so let UI know
            PropertyChanged?.Invoke(sender, e);
        }

    }

 
  


}
