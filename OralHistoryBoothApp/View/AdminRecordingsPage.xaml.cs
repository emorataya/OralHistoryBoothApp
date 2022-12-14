using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using OralHistoryBoothApp.Views;
using TagLib.Id3v2;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.Media.Core;
using Windows.Media.Playback;
using Windows.Storage;
using Windows.Storage.FileProperties;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Media.Animation;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace OralHistoryBoothApp.View
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class AdminRecordingsPage : Page
    {
        // Create a MediaPlayer instance
        MediaPlayer player = new MediaPlayer();

        bool isPlaying = false;

        DispatcherTimer timer = new DispatcherTimer();
        public AdminRecordingsPage()
        {
            this.InitializeComponent();

            // Get the MP3 files in the "Music" folder.
            StorageFolder storageFolder = Windows.Storage.ApplicationData.Current.LocalFolder;
            string[] mp3Files = Directory.GetFiles(storageFolder.Path, "*.mp3");


            // Add the MP3 files to the ListView.
            foreach (string mp3File in mp3Files)
            {
                MP3ListView.Items.Add(mp3File);
            }

            timer.Interval = TimeSpan.FromMinutes(5);

          
            // When the timer elapses, navigate to the new page
            timer.Tick += (sender, args) =>
            {

                if (player.PlaybackSession.PlaybackState == MediaPlaybackState.Playing)
                {
                    timer.Stop();
                    timer.Start();
                }
                else if (!(player.PlaybackSession.PlaybackState == MediaPlaybackState.Playing))
                {
                    this.Frame.Navigate(typeof(LoginPage), null, new SuppressNavigationTransitionInfo());
                }
            };

            // When the user moves their mouse or touches the screen, reset the timer
            PointerMoved += (sender, args) =>
            {
                timer.Stop();
                timer.Start();
            };

            // Start the timer
            timer.Start();
        }




        private async void MP3ListView_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            // Get the index of the selected item
            int selectedIndex = MP3ListView.SelectedIndex;

            // Make sure a valid index was selected
            if (selectedIndex >= 0 && selectedIndex < MP3ListView.Items.Count)
            {
                // Get the selected item using the index
                object selectedItem = MP3ListView.Items[selectedIndex];

                // Convert the item to a string
                string selectedItemString = selectedItem.ToString();

                // Create a StorageFile instance for the selected file
                StorageFile file = await StorageFile.GetFileFromPathAsync(selectedItemString);

                MusicProperties properties = await file.Properties.GetMusicPropertiesAsync();

                // Get the metadata from the MusicProperties
                string Name = properties.Title;
                string isStudent = properties.Subtitle;
                string decade = properties.Album;
                //string storyTag = properties.Publisher;


                // Create a string to represent the metadata
                string metadata = $"Name: {Name} \nStudent Attendance: {isStudent} \nDecade of recording: {decade} ";

                DisplayTagsText.Text = metadata;

            }
        }

        private async void DeleteSelectedBtn_Click(object sender, RoutedEventArgs e)
        {
            player.Pause();
            // Get the index of the selected item
            int selectedIndex = MP3ListView.SelectedIndex;

            // Make sure a valid index was selected
            if (selectedIndex >= 0 && selectedIndex < MP3ListView.Items.Count)
            {
                // Get the selected item using the index
                object selectedItem = MP3ListView.Items[selectedIndex];

                // Convert the item to a string
                string selectedItemString = selectedItem.ToString();

                // Create a StorageFile instance for the selected file
                StorageFile file = await StorageFile.GetFileFromPathAsync(selectedItemString);

                MP3ListView.Items.RemoveAt(selectedIndex);

                // Delete the file
                await file.DeleteAsync();
            }
        }

        private void PlayBtn_Click(object sender, RoutedEventArgs e)
        {
            //prueba

            // Get the index of the selected item
            int selectedIndex = MP3ListView.SelectedIndex;

            // Make sure a valid index was selected
            if (selectedIndex >= 0 && selectedIndex < MP3ListView.Items.Count)
            {
                // Get the selected item using the index
                object selectedItem = MP3ListView.Items[selectedIndex];

                // Convert the item to a string
                string selectedItemString = selectedItem.ToString();

                // Set the source of the MediaPlayer to the selected MP3 file
                player.Source = MediaSource.CreateFromUri(new Uri(selectedItemString));

                // Play the MP3 file
                player.Play();

            }
        }

        private void LogoutBtn_Click(object sender, RoutedEventArgs e)
        {
            this.Frame.Navigate(typeof(LoginPage), null, new SuppressNavigationTransitionInfo());

        }
    }

}


//// Create a new DispatcherTimer and set its interval to 5 minutes
//DispatcherTimer timer = new DispatcherTimer();
//timer.Interval = TimeSpan.FromMinutes(5);

//// When the timer elapses, navigate to the new page
//timer.Tick += (sender, args) =>
//{
//    // Navigate to the new page here
//};

//// Start the timer
//timer.Start();
