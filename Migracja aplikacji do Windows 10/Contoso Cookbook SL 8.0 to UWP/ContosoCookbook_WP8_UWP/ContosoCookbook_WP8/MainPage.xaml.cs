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
using Windows.UI.Xaml.Controls;
using ContosoCookbook.Data;
using System.Linq;
using System.Threading.Tasks;

namespace ContosoCookbook
{

   public partial class MainPage
      : Windows.UI.Xaml.Controls.Page
   {
      //private Windows.UI.ViewManagement.StatusBarProgressIndicator pi;

      // Constructor
      public MainPage()
      {
         InitializeComponent();
         this.DataContext = App.Recipes;
      }

      protected async override void OnNavigatedTo(Windows.UI.Xaml.Navigation.NavigationEventArgs e)
      {
         if ( !App.Recipes.IsLoaded )
         {

                //pi = Windows.UI.ViewManagement.StatusBar.GetForCurrentView().ProgressIndicator;
                //pi.ProgressValue = null;
                //pi.Text = "Loading data, please wait...";
                //WindowsPhoneUWP.UpgradeHelpers.ProgressIndicator.ChangeVisibility(pi, true);
                //Windows.UI.ViewManagement.StatusBar.SetIsVisible(this, true);
                ////WINDOWS_PHONE_SL_TO_UWP: (1101) Microsoft.Phone.Shell.SystemTray.SetProgressIndicator was not upgraded
                //Windows.UI.ViewManagement.StatusBar.SetProgressIndicator(this, pi);
                ProgressBar.Visibility = Windows.UI.Xaml.Visibility.Visible;
                await Task.Delay(2000);
                await App.Recipes.LoadLocalDataAsync();
                ProgressBar.Visibility = Windows.UI.Xaml.Visibility.Collapsed;
                //WindowsPhoneUWP.UpgradeHelpers.ProgressIndicator.ChangeVisibility(pi, false);
                //Windows.UI.ViewManagement.StatusBar.SetIsVisible(this, false);
            }
         base.OnNavigatedTo(e);
      }

      private void lstGroups_SelectionChanged(object sender, Windows.UI.Xaml.Controls.SelectionChangedEventArgs e)
      {
         if ( lstGroups.SelectedIndex > -1 )
         {
            this.Frame.Navigate(typeof(ContosoCookbook.GroupDetailPage), "ID=" + (lstGroups.SelectedItem as RecipeDataGroup).UniqueId);
         }
      }

   }

}