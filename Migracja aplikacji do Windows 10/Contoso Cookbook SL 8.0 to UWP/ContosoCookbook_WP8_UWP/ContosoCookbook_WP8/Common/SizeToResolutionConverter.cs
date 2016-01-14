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
using System.Net;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Documents;
using System.Windows.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Media.Animation;
using Windows.UI.Xaml.Data;

namespace ContosoCookbook.Common
{

   public class SizeToResolutionConverter
      : Windows.UI.Xaml.Data.IValueConverter
   {

      public object Convert(object value, Type targetType, object parameter, System.String culture)
      {
         double val = double.Parse(value.ToString());
         double retVal = val;
         #if WP7
                     //Nothing to do, return the same value
#else
                     //Adjust to giving resolution (multiply)
            //TDOD
#endif
         return retVal;
      }

      public object ConvertBack(object value, Type targetType, object parameter, System.String culture)
      {
         double val = double.Parse(value.ToString());
         double retVal = val;
         #if WP7
                     //Nothing to do, return the same value
#else
                     //Adjust to giving resolution (divide)
            //TDOD
#endif
         return retVal;
      }

   }

}