using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.Storage;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace OralHistoryBoothApp.View
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class AdminRecordingsPage : Page
    {
        public AdminRecordingsPage()
        {
            this.InitializeComponent();

            // Get the MP3 files in the "Music" folder.
            string[] mp3Files = Directory.GetFiles("C:\\Users\\fabri\\AppData\\Local\\Packages\\373b416a-eb2b-495f-8916-344e0f68b79a_p3exjxwffkpjw\\LocalState", "*.mp3");

            // Add the MP3 files to the ListView.
            foreach (string mp3File in mp3Files)
            {
                MP3ListView.Items.Add(mp3File);
            }
        }
    }

}
