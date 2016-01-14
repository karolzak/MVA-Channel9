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
using WindowsPhoneUWP.UpgradeHelpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Navigation;
using ContosoCookbook.Data;
using Windows.UI.Notifications;

namespace ContosoCookbook.Common
{

   public class Features
   {

      public class Notifications
      {

         public static void SetReminder(RecipeDataItem item)
         {


                var toastNotifier = Windows.UI.Notifications.ToastNotificationManager.CreateToastNotifier();

                
                if (!IsScheduled(item.UniqueId))
                {
                    Windows.Data.Xml.Dom.XmlDocument doc = ToastNotificationManager.GetTemplateContent(ToastTemplateType.ToastText02);
                    var textLines = doc.GetElementsByTagName("text");
                    textLines[0].InnerText = item.Title;
                    textLines[1].InnerText = "Have you finished cooking?";
                    ScheduledToastNotification reminder = new ScheduledToastNotification(doc, DateTime.Now.AddSeconds(10));
                    reminder.Id = item.UniqueId;
                    toastNotifier.AddToSchedule(reminder);
                }
                else
                {

                    var schedule = toastNotifier.GetScheduledToastNotifications().FirstOrDefault(x => x.Id == item.UniqueId);
                    if (schedule != null)
                        toastNotifier.RemoveFromSchedule(schedule);
                }

                
            }

         public static bool IsScheduled(string name)
         {

                var toastNotifier = Windows.UI.Notifications.ToastNotificationManager.CreateToastNotifier();

                var schedule = toastNotifier.GetScheduledToastNotifications().FirstOrDefault(x => x.Id == name);

                if (schedule == null)
                {
                    return false;
                }
                else
                {
                    return true;
                }
            }

      }

      public class Tile
      {

         public static async System.Threading.Tasks.Task<bool> TileExists(string NavSource)
         {
            Windows.UI.StartScreen.SecondaryTile tile = (await WindowsPhoneUWP.UpgradeHelpers.TilesHelper.GetActiveTiles()).FirstOrDefault(o => o.GetAsNavigationUri().ToString().Contains(NavSource));
            return tile == null ? false : true;
         }

         public static async System.Threading.Tasks.Task DeleteTile(string NavSource)
         {
            Windows.UI.StartScreen.SecondaryTile tile = (await WindowsPhoneUWP.UpgradeHelpers.TilesHelper.GetActiveTiles()).FirstOrDefault(o => o.GetAsNavigationUri().ToString().Contains(NavSource));
            if ( tile == null )
               return ;
            await tile.RequestDeleteAsync();
         }

         public static async System.Threading.Tasks.Task SetTile(RecipeDataItem item, string NavSource)
         {
            WindowsPhoneUWP.UpgradeHelpers.StandardTileData tileData = new StandardTileData
               {
                  Title = "Contoso Cookbook", BackTitle = item.Group.Title, BackContent = item.Title, BackBackgroundImage = new Uri(new Uri("ms-appx://"), item.Group.GetImageUri()), BackgroundImage = new Uri(new Uri("ms-appx://"), item.GetImageUri())
               };
            await WindowsPhoneUWP.UpgradeHelpers.TilesHelper.CreateHelper(new Uri(new Uri("ms-appx://"), NavSource), tileData);
         }

      }

   }

}