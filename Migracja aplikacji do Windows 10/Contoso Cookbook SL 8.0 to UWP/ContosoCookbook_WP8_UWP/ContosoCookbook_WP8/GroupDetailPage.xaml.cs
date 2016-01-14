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

namespace ContosoCookbook
{

   public partial class GroupDetailPage
      : Windows.UI.Xaml.Controls.Page
   {
      RecipeDataGroup group;

      public GroupDetailPage()
      {
         InitializeComponent();
      }

      protected override void OnNavigatedTo(Windows.UI.Xaml.Navigation.NavigationEventArgs e)
      {
         var navigationArguments = (e.Parameter ?? "").ToString().Split('&').Where(t => !string.IsNullOrWhiteSpace(t)).Select(t => t.Split('=')).ToDictionary(t => t[0], t => t[1]);
         string UniqueId = navigationArguments["ID"];
         group = App.Recipes.FindGroup(UniqueId);
         pivot.DataContext = group;
         base.OnNavigatedTo(e);
      }

      private void lstRecipes_SelectionChanged(object sender, Windows.UI.Xaml.Controls.SelectionChangedEventArgs e)
      {
         if ( lstRecipes.SelectedItems.Count > 0 )
            this.Frame.Navigate(typeof(ContosoCookbook.RecipeDetailPage), "ID=" + (lstRecipes.SelectedItem as RecipeDataItem).UniqueId);
      }

   }

}