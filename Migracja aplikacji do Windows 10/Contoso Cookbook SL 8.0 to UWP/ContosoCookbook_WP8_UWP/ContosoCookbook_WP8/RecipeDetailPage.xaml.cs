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
using System.Runtime.InteropServices.WindowsRuntime;
using System.IO;
using System;
using Windows.UI.Xaml;
using ContosoCookbook.Data;
using System.IO.IsolatedStorage;
using Windows.UI.Xaml.Media.Imaging;
using ContosoCookbook.Common;
using System.Linq;
using Windows.Media.Capture;
using Windows.Storage;
using Windows.ApplicationModel.DataTransfer;
using System.Collections.Generic;

namespace ContosoCookbook
{

   public partial class RecipeDetailPage
      : Windows.UI.Xaml.Controls.Page
   {

        string uri;
      //Windows.Storage.Pickers.FileOpenPicker camera;
      private RecipeDataItem item;
      private const string removeAlarmUri = "/Assets/Icons/alarmRemove.png";
      private const string AlarmUri = "/Assets/Icons/alarm.png";
      private const string removeFavUri = "/Assets/Icons/unlike.png";
      private const string FavUri = "/Assets/Icons/like.png";

      public RecipeDetailPage()
      {
         InitializeComponent();
         //camera = new Windows.Storage.Pickers.FileOpenPicker()
         //   {
         //      SuggestedStartLocation = Windows.Storage.Pickers.PickerLocationId.PicturesLibrary, FileTypeFilter =
         //      {
         //         ".jpg",
         //         ".png",
         //         ".bmp"
         //      }
         //   };
         //            camera.Completed += camera_Completed;
         ;
      }

      public Windows.UI.Xaml.Controls.AppBarButton alarmBtn
      {
         get
         {
            var appBar = (Windows.UI.Xaml.Controls.CommandBar)((Windows.UI.Xaml.Controls.CommandBar)BottomAppBar);
            var count = appBar.PrimaryCommands.Count;
            for ( var i = 0; i < count; i++ )
            {
               Windows.UI.Xaml.Controls.AppBarButton btn = appBar.PrimaryCommands[i] as Windows.UI.Xaml.Controls.AppBarButton;
               if ( ((Windows.UI.Xaml.Controls.BitmapIcon)btn.Icon).UriSource.OriginalString.Contains("alarm") )
                  return btn;
            }
            return null;
         }
      }

      public Windows.UI.Xaml.Controls.AppBarButton pinBtn
      {
         get
         {
            var appBar = (Windows.UI.Xaml.Controls.CommandBar)((Windows.UI.Xaml.Controls.CommandBar)BottomAppBar);
            var count = appBar.PrimaryCommands.Count;
            for ( var i = 0; i < count; i++ )
            {
               Windows.UI.Xaml.Controls.AppBarButton btn = appBar.PrimaryCommands[i] as Windows.UI.Xaml.Controls.AppBarButton;
               if ( ((Windows.UI.Xaml.Controls.BitmapIcon)btn.Icon).UriSource.OriginalString.Contains("like") )
                  return btn;
            }
            return null;
         }
      }

      async System.Threading.Tasks.Task SetPinBar()
      {
         //var uri = ((Windows.UI.Xaml.Controls.Frame)Windows.UI.Xaml.Window.Current.Content).Source.ToString();
         if ( await Features.Tile.TileExists(uri) )
         {
            pinBtn.Icon = new Windows.UI.Xaml.Controls.BitmapIcon()
               {
                  UriSource = new Uri(new Uri("ms-appx://"), removeFavUri)
               };
            pinBtn.Label = "Unpin";
         }
         else
         {
            pinBtn.Icon = new Windows.UI.Xaml.Controls.BitmapIcon()
               {
                  UriSource = new Uri(new Uri("ms-appx://"), FavUri)
               };
            pinBtn.Label = "Pin";
         }
      }

      void SetScheduleBar(string name)
      {
         var isScheduled = Features.Notifications.IsScheduled(name);
         if ( isScheduled )
         {
            alarmBtn.Icon = new Windows.UI.Xaml.Controls.BitmapIcon()
               {
                  UriSource = new Uri(new Uri("ms-appx://"), removeAlarmUri)
               };
            alarmBtn.Label = "Remove Alarm";
         }
         else
         {
            alarmBtn.Icon = new Windows.UI.Xaml.Controls.BitmapIcon()
               {
                  UriSource = new Uri(new Uri("ms-appx://"), AlarmUri)
               };
            alarmBtn.Label = "Set Alarm";
         }
      }

      async void btnPinToStart_Click(object sender, Windows.UI.Xaml.RoutedEventArgs e)
      {
         //var uri = ((Windows.UI.Xaml.Controls.Frame)Windows.UI.Xaml.Window.Current.Content).Source.ToString();
         if ( await Features.Tile.TileExists(uri) )
            await Features.Tile.DeleteTile(uri);
         else
            await Features.Tile.SetTile(item, uri);
         await SetPinBar();
      }

      async void btnTakePicture_Click(object sender, Windows.UI.Xaml.RoutedEventArgs e)
      {

            try
            {
                CameraCaptureUI cameraUI = new CameraCaptureUI();

                Windows.Storage.StorageFile capturedMedia =
                    await cameraUI.CaptureFileAsync(CameraCaptureUIMode.Photo);

                if (capturedMedia != null)
                {
                    StorageFolder folder = ApplicationData.Current.LocalFolder;

                    await folder.CreateFolderAsync(item.Group.Title, CreationCollisionOption.OpenIfExists);

                    string fileName = string.Format("{0}\\{1}.jpg", item.Group.Title, DateTime.Now.ToString("dd-MM-yyyy HH-mm-ss"));
                    StorageFile file = await folder.CreateFileAsync(fileName);
                    await capturedMedia.CopyAndReplaceAsync(file);

                    if (null == item.UserImages)
                        item.UserImages = new System.Collections.ObjectModel.ObservableCollection<string>();
                    item.UserImages.Add(fileName);

                }
            }
            catch (Exception ex)
            {
                await (new Windows.UI.Popups.MessageDialog("An error occurred.")).ShowAsync();
            }
            
      }
        
      void btnShareShareTask_Click(object sender, Windows.UI.Xaml.RoutedEventArgs e)
      {
           
            DataTransferManager.GetForCurrentView().DataRequested += RecipeDetailPage_DataRequested;

            DataTransferManager.ShowShareUI();
        }

        

       

        private async void RecipeDetailPage_DataRequested(DataTransferManager sender, DataRequestedEventArgs args)
        {
            DataRequest request = args.Request;
            request.Data.Properties.Title = "Share Example";
            request.Data.Properties.Description = "A demonstration on how to share";

            if (null != item.UserImages && item.UserImages.Count > 0)
            {
                var file = await ApplicationData.Current.LocalFolder.GetFileAsync(item.UserImages[0]);
                request.Data.SetStorageItems(new List<StorageFile>() { file });
            }
        }

        void btnStartCooking_Click(object sender, Windows.UI.Xaml.RoutedEventArgs e)
      {
         Features.Notifications.SetReminder(item);
         SetScheduleBar(item.UniqueId);
      }

      protected async override void OnNavigatedTo(Windows.UI.Xaml.Navigation.NavigationEventArgs e)
      {
            uri = this.BaseUri.AbsolutePath + "?" + e.Parameter;


         var navigationArguments = (e.Parameter ?? "").ToString().Split('&').Where(t => !string.IsNullOrWhiteSpace(t)).Select(t => t.Split('=')).ToDictionary(t => t[0], t => t[1]);
         string UniqueId = "";
         UniqueId = navigationArguments["ID"];
         if ( !App.Recipes.IsLoaded )
            await App.Recipes.LoadLocalDataAsync();
         await NavigateToRecipe(UniqueId);
         base.OnNavigatedTo(e);
      }

      private async System.Threading.Tasks.Task NavigateToRecipe(string UniqueId)
      {
         item = App.Recipes.FindRecipe(UniqueId);
         pivot.DataContext = item;
         SetScheduleBar(item.UniqueId);
         await SetPinBar();
      }

   }

}