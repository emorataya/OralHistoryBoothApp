﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
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
    public sealed partial class AdminLogInPage : Page
    {
        
        public AdminLogInPage()
        {
            this.InitializeComponent();
           // AdminLogIn.IsEnabled = false

           // 
           requiredPassword.Visibility = Visibility.Collapsed;

        }

        private void AdminLogIn_Click(object sender, RoutedEventArgs e)
        {
            if(usernameInput.Text == "admin" && passwordBox.Password == "1234")
            {
                //AdminLogIn.IsEnabled = true;
                this.Frame.Navigate(typeof(AdminRecordingsPage));

            }

        }
    }
}
