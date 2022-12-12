using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Threading.Tasks;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.Media.Capture;
using Windows.Media.MediaProperties;
using Windows.Storage.Streams;
using Windows.Storage;
using Windows.UI.Core;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;
using Windows.ApplicationModel;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace OralHistoryBoothApp.Views
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    /// //https://www.c-sharpcorner.com/UploadFile/2b876a/audio-recorder-in-windows-10-universal-windows-platform/
    public sealed partial class Recordingpage : Page
    {
        MediaCapture capture;
        InMemoryRandomAccessStream buffer;
        bool record;
        string audioFile = "";
        string filename;
        string fullpath;
        bool isYesChecked = false;
        bool isNoChecked = false;
        bool nameTyped = false;
        bool storyClicked = false;
        bool decadeCLicked = false;
        //timer
        private DispatcherTimer timer;
        private int time = 0;

        private static Random random = new Random();

        public static string RandomString(int length)
        {
            const string chars = "0123456789";
            return new string(Enumerable.Repeat(chars, length)
                .Select(s => s[random.Next(s.Length)]).ToArray());
        }

        public Recordingpage()
        {
            this.InitializeComponent();

            //timer
           this.NavigationCacheMode = NavigationCacheMode.Required;
            timer = new DispatcherTimer();
            timer.Interval = new TimeSpan(0, 0, 1);
            timer.Tick += timer_Tick;
            //timer.Start();
            stopBtn.IsEnabled = false;
            playBtn.IsEnabled = false;
            pauseBtn.IsEnabled = false;
            resumeBtn.IsEnabled = false;
            cancelBtn.IsEnabled = false;
            SubmitBtn.IsEnabled = false;
        }

        private async void timer_Tick(object sender, object e)
        {
            if (time > 0)
            {
                time--;
                TimerIndicator.Text = String.Format("00:0{0}:{1}", time / 60, time % 60);
            }
            else
            {
                timer.Stop();
               await capture.StopRecordAsync();
                record = false;
                playBtn.IsEnabled = true;
                stopBtn.IsEnabled = false;
                resumeBtn.IsEnabled = false;
                pauseBtn.IsEnabled = false;
                cancelBtn.IsEnabled = true;

            }

        }

        private async Task<bool> RecordProcess()
        {
            if (buffer != null)
            {
                buffer.Dispose();
            }
            buffer = new InMemoryRandomAccessStream();
            if (capture != null)
            {
                capture.Dispose();
            }
            try
            {
                MediaCaptureInitializationSettings settings = new MediaCaptureInitializationSettings
                {
                    StreamingCaptureMode = StreamingCaptureMode.Audio
                };
                capture = new MediaCapture();
                await capture.InitializeAsync(settings);
                capture.RecordLimitationExceeded += (MediaCapture sender) =>
                {
                    //Stop  
                    //await capture.StopRecordAsync();  
                    record = false;
                    throw new Exception("Record Limitation Exceeded ");
                };
                capture.Failed += (MediaCapture sender, MediaCaptureFailedEventArgs errorEventArgs) =>
                {
                    record = false;
                    throw new Exception(string.Format("Code: {0}. {1}", errorEventArgs.Code, errorEventArgs.Message));
                };

            }
            catch (Exception ex)
            {
                if (ex.InnerException != null && ex.InnerException.GetType() == typeof(UnauthorizedAccessException))
                {
                    throw ex.InnerException;
                }
                throw;
            }
          
            return true;
        }
        public async Task PlayRecordedAudio(CoreDispatcher UiDispatcher)
        {

            //await UiDispatcher.RunAsync(CoreDispatcherPriority.Normal, async () =>
            //{
            //    MediaElement playbackMediaElement = new MediaElement();
            //    StorageFolder storageFolder2 = Windows.Storage.ApplicationData.Current.LocalFolder;
            //    StorageFile storageFile = await storageFolder2.GetFileAsync(filename);
            //    IRandomAccessStream stream = await storageFile.OpenAsync(FileAccessMode.Read);
            //    playbackMediaElement.SetSource(stream, storageFile.FileType);
            //    playbackMediaElement.Play();
            //});

            MediaElement playback = new MediaElement();
            //
            IRandomAccessStream audio = buffer.CloneStream();

            if (audio == null)
                throw new ArgumentNullException("buffer");
            //Windows.Storage.ApplicationData.Current.LocalFolder;
            StorageFolder storageFolder = Windows.Storage.ApplicationData.Current.LocalFolder;
            if (!string.IsNullOrEmpty(filename))
            {
                StorageFile original = await storageFolder.GetFileAsync(filename);
                await original.DeleteAsync();
            }
            await UiDispatcher.RunAsync(CoreDispatcherPriority.Normal, async () =>
            {
                audioFile = RandomString(10);
                audioFile += ".mp3";

                StorageFile storageFile = await storageFolder.CreateFileAsync(audioFile, CreationCollisionOption.GenerateUniqueName);
                filename = storageFile.Name;

                fullpath = storageFile.Path;

                using (IRandomAccessStream fileStream = await storageFile.OpenAsync(FileAccessMode.ReadWrite))
                {
                    await RandomAccessStream.CopyAndCloseAsync(audio.GetInputStreamAt(0), fileStream.GetOutputStreamAt(0));
                    await audio.FlushAsync();
                    audio.Dispose();
                }

                //
                IRandomAccessStream stream = await storageFile.OpenAsync(FileAccessMode.Read);
                playback.SetSource(stream, storageFile.FileType);

                //Prueba de TagLib

                playback.Play();



            });
        }

        private async void recordBtn_Click(object sender, RoutedEventArgs e)
        {
            time = 600;

            if (record)
            {
                //already recored process  
            }
            else
            { 
                await RecordProcess();
                await capture.StartRecordToStreamAsync(MediaEncodingProfile.CreateMp3(AudioEncodingQuality.Auto), buffer);
                if (record)
                {
                    throw new InvalidOperationException();
                }
                record = true;
            }

            TimerIndicator.Text = "You have 10 minutes";
            timer.Start();
            recordBtn.IsEnabled = false;
            stopBtn.IsEnabled = true;
            pauseBtn.IsEnabled = true;
            cancelBtn.IsEnabled = true;

        }
        private async void stopBtn_Click(object sender, RoutedEventArgs e)
        {
            timer.Stop();
            await capture.StopRecordAsync();
            record = false;
            playBtn.IsEnabled = true;
            stopBtn.IsEnabled = false;
            resumeBtn.IsEnabled = false;
            pauseBtn.IsEnabled = false;
            //SaveAudioToFile();
        }

        private async void PlayBtn_Click(object sender, RoutedEventArgs e)
        {
            await PlayRecordedAudio(Dispatcher);
        }

        private async void PauseBtn_Click(object sender, RoutedEventArgs e)
        {
            timer.Stop();
            await capture.PauseRecordAsync(0);
            pauseBtn.IsEnabled = false;
            resumeBtn.IsEnabled = true;
        }

        private async void ResumeBtn_Click(object sender, RoutedEventArgs e)
        {
            timer.Start();
            await capture.ResumeRecordAsync();
            pauseBtn.IsEnabled = true;
            resumeBtn.IsEnabled = false;
        }

        private void CancelBtn_Click(object sender, RoutedEventArgs e)
        {
            //buffer.Dispose();
            buffer = null;
            recordBtn.IsEnabled = true;
            stopBtn.IsEnabled = false;
            playBtn.IsEnabled = false;
            resumeBtn.IsEnabled = false;
            cancelBtn.IsEnabled = false;
            pauseBtn.IsEnabled = false;
            timer.Stop();
            record = false;
        }


        private void addTags()
        {
            TagLib.File file = TagLib.File.Create(fullpath);
            file.Tag.Title = "Name: " + NameTextBox.Text;
            if (YesButton.IsChecked == true)
                file.Tag.Subtitle = "Student";
            else { file.Tag.Subtitle = "Not a student"; }
            string DecadeSelected = Decades.SelectedItem.ToString();
            file.Tag.Album = DecadeSelected;
            string StorySelected = StoryTags.SelectedItem.ToString();
            file.Tag.Copyright = StorySelected;
            file.Save();
        }

        private async void SaveAudio(CoreDispatcher UiDispatcher)
        {

            IRandomAccessStream audio = buffer.CloneStream();

            if (audio == null)
                throw new ArgumentNullException("buffer");
            //Windows.Storage.ApplicationData.Current.LocalFolder;
            StorageFolder storageFolder = Windows.Storage.ApplicationData.Current.LocalFolder;
            if (!string.IsNullOrEmpty(filename))
            {
                StorageFile original = await storageFolder.GetFileAsync(filename);
                await original.DeleteAsync();
            }
            await UiDispatcher.RunAsync(CoreDispatcherPriority.Normal, async () =>
            {
                audioFile = RandomString(10);
                audioFile += ".mp3";

                StorageFile storageFile = await storageFolder.CreateFileAsync(audioFile, CreationCollisionOption.GenerateUniqueName);
                filename = storageFile.Name;

                fullpath = storageFile.Path;

                using (IRandomAccessStream fileStream = await storageFile.OpenAsync(FileAccessMode.ReadWrite))
                {
                    await RandomAccessStream.CopyAndCloseAsync(audio.GetInputStreamAt(0), fileStream.GetOutputStreamAt(0));
                    await audio.FlushAsync();
                    audio.Dispose();
                }
            });
        }
        private void SubmitBtn_Click(object sender, RoutedEventArgs e)
        {
     
            //SaveAudio(Dispatcher);
            addTags();


        }

        private void NameTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            nameTyped = true;
            if (NameTextBox.Text.Length == 0)
            {
                SubmitBtn.IsEnabled = false;
                nameTyped = false;
            }
            else if(storyClicked == true && decadeCLicked == true && (isNoChecked == true || isYesChecked == true))
            {
                SubmitBtn.IsEnabled = true;
                
            }
        }

        private void YesButton_Click(object sender, RoutedEventArgs e)
        {
            isYesChecked = true;
            if (storyClicked == true && decadeCLicked == true && nameTyped == true)
            { SubmitBtn.IsEnabled = true; }
        }

        private void NoButton_Click(object sender, RoutedEventArgs e)
        {
            isNoChecked = true;
            if (storyClicked == true && decadeCLicked == true && nameTyped == true)
            { SubmitBtn.IsEnabled = true; }
        }

        private void Decades_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            decadeCLicked = true;
            if (storyClicked == true && nameTyped == true && (isNoChecked == true || isYesChecked == true))
            {
                SubmitBtn.IsEnabled = true;
            }

        }

        private void StoryTags_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            storyClicked = true;
            if (decadeCLicked = true && nameTyped == true && (isNoChecked == true || isYesChecked == true))
            {
                SubmitBtn.IsEnabled = true;
            }
        }


        //private async void SaveAudioToFile()
        //{
        //    IRandomAccessStream audioStream = buffer.CloneStream();
        //    StorageFolder storageFolder = Package.Current.InstalledLocation;
        //    StorageFile storageFile = await storageFolder.CreateFileAsync(
        //      "prueba", CreationCollisionOption.GenerateUniqueName);
        //    this._fileName = storageFile.Name;
        //    using (IRandomAccessStream fileStream =
        //      await storageFile.OpenAsync(FileAccessMode.ReadWrite))
        //    {
        //        await RandomAccessStream.CopyAndCloseAsync(
        //          audioStream.GetInputStreamAt(0), fileStream.GetOutputStreamAt(0));
        //        await audioStream.FlushAsync();
        //        audioStream.Dispose();
        //    }
        //}
    }
}
