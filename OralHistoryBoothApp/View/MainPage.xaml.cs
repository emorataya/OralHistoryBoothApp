using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using OralHistoryBoothApp.Model;
using OralHistoryBoothApp.ViewModel;
using OralHistoryBoothApp.Views;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;
using static System.Net.Mime.MediaTypeNames;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace OralHistoryBoothApp
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        private AdminViewModel adminViewModel { get; set; }
        private RecordingViewModel Recording { get; set; }

       
        public MainPage()
        {
            this.InitializeComponent();

            Recording = new RecordingViewModel();
            adminViewModel = new AdminViewModel();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            this.Frame.Navigate(typeof(Recordingpage));

            //adminViewModel.AdminInfo.Add(new RecordingViewModel { Name = Recording.Name });

        }
    }
}
