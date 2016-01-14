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
    public partial class GroupDetailPage : PhoneApplicationPage
    {
        RecipeDataGroup group;
        public GroupDetailPage()
        {
            InitializeComponent();
        }

        protected override void OnNavigatedTo(System.Windows.Navigation.NavigationEventArgs e)
        {
            string UniqueId = NavigationContext.QueryString["ID"];
            group = App.Recipes.FindGroup(UniqueId);
            pivot.DataContext = group;

            base.OnNavigatedTo(e);
        }

        private void lstRecipes_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (lstRecipes.SelectedItems.Count > 0)
                NavigationService.Navigate(new Uri("/RecipeDetailPage.xaml?ID=" + (lstRecipes.SelectedItem as RecipeDataItem).UniqueId, UriKind.Relative));
        }

    }
}