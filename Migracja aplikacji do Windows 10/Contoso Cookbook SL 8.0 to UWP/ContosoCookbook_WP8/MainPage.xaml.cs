// ----------------------------------------------------------------------------------
// Microsoft Developer & Platform Evangelism
// 
// Copyright (c) Microsoft Corporation. All rights reserved.
// 
// THIS CODE AND INFORMATION ARE PROVIDED "AS IS" WITHOUT WARRANTY OF ANY KIND, 
// EITHER EXPRESSED OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE IMPLIED WARRANTIES 
// OF MERCHANTABILITY AND/OR FITNESS FOR A PARTICULAR PURPOSE.
// ----------------------------------------------------------------------------------
// The example companies, organizations, products, domain names,
// e-mail addresses, logos, people, places, and events depicted
// herein are fictitious.  No association with any real company,
// organization, product, domain name, email address, logo, person,
// places, or events is intended or should be inferred.
// ----------------------------------------------------------------------------------

using System;
using System.Windows.Controls;
using Microsoft.Phone.Controls;
using ContosoCookbook.Data;

namespace ContosoCookbook
{
    public partial class MainPage : PhoneApplicationPage
    {
        private Microsoft.Phone.Shell.ProgressIndicator pi;
       
        // Constructor
        public MainPage()
        {
            InitializeComponent();
            this.DataContext = App.Recipes;
        }

        protected async override void OnNavigatedTo(System.Windows.Navigation.NavigationEventArgs e)
        {
            if (!App.Recipes.IsLoaded)
            {
                pi = new Microsoft.Phone.Shell.ProgressIndicator();
                pi.IsIndeterminate = true;
                pi.Text = "Loading data, please wait...";
                pi.IsVisible = true;

                Microsoft.Phone.Shell.SystemTray.SetIsVisible(this, true);
                Microsoft.Phone.Shell.SystemTray.SetProgressIndicator(this, pi);

                await App.Recipes.LoadLocalDataAsync();                

                pi.IsVisible = false;
                Microsoft.Phone.Shell.SystemTray.SetIsVisible(this, false);    
            }
            base.OnNavigatedTo(e);
        }

        private void lstGroups_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (lstGroups.SelectedIndex > -1)
            {
                NavigationService.Navigate(new Uri("/GroupDetailPage.xaml?ID=" + (lstGroups.SelectedItem as RecipeDataGroup).UniqueId, UriKind.Relative));
            }
        }
    }
}