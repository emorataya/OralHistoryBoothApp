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
        public string Student
        {
            get { return recordingsinfo.Student; }
            set
            {
                recordingsinfo.Student = value;
                OnPropertyChanged(this, new PropertyChangedEventArgs("Student"));
            }
        }

        public string Decade
        {
            get { return recordingsinfo.Decade; }
            set
            {
                recordingsinfo.Decade = value;
                OnPropertyChanged(this, new PropertyChangedEventArgs("decade"));
            }
        }

        public string Tag
        {
            get { return recordingsinfo.Tag; }
            set
            {
                recordingsinfo.Tag = value;
                OnPropertyChanged(this, new PropertyChangedEventArgs("Tag"));

            }
        }

        public AdminViewModel()
        {
            this.recordingsinfo = new RecordingsInfo();
            AdminInfo = new ObservableCollection<RecordingViewModel>();

            foreach (var admin in recordingsinfo.recordings)
            {
                var newAdmin = new RecordingViewModel { Name = admin.Name, Decade = admin.Decade, Student = admin.Student, Tag = admin.Tag};
                newAdmin.PropertyChanged += OnPropertyChanged;
                AdminInfo.Add(newAdmin);
            }
        }
        private void OnPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            PropertyChanged?.Invoke(sender, e);
        }

    }

 
  


}
