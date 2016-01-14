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
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml.Data;
using System.Collections;
using System.Collections.ObjectModel;
using Windows.UI.Xaml;
using ContosoCookbook.Data;

namespace ContosoCookbook.Common
{

   public class UserImagesDisplayConverter
      : Windows.UI.Xaml.Data.IValueConverter
   {

      public object Convert(object value, Type targetType, object parameter, System.String culture)
      {
         var list = value as ObservableCollection<string>;
         if ( null != list )
         {
            if ( list.Count == 0 )
               return Windows.UI.Xaml.Visibility.Visible;
            else
               return Windows.UI.Xaml.Visibility.Collapsed;
         }
         else
            return Windows.UI.Xaml.Visibility.Visible;
      }

      public object ConvertBack(object value, Type targetType, object parameter, System.String culture)
      {
         throw new NotImplementedException();
      }

   }

}