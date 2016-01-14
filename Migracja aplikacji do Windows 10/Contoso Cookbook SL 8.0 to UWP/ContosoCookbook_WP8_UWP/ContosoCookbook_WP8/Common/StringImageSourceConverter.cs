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
using System.IO;
using System;
using System.Collections.Generic;
using System.IO;
using System.IO.IsolatedStorage;
using System.Linq;
using System.Text;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Media.Imaging;
using Windows.Storage;
using System.Threading.Tasks;

namespace ContosoCookbook.Common
{

   public class StringImageSourceConverter
      : Windows.UI.Xaml.Data.IValueConverter
   {


        public object Convert(object value, Type targetType, object parameter, string language)
        {

            var file = value.ToString();
            StorageFolder folder = ApplicationData.Current.LocalFolder;
            Stream stream = null;
            try
            {
                var task1 = folder.OpenStreamForReadAsync(file);
                Task.WaitAll(task1);
                stream = task1.Result;
                Windows.UI.Xaml.Media.Imaging.BitmapImage bitmap = new Windows.UI.Xaml.Media.Imaging.BitmapImage();
                bitmap.CreateOptions = BitmapCreateOptions.None;
                bitmap.SetSource(stream.AsRandomAccessStream());
                stream.Dispose();
                return bitmap;
            }
            catch
            {
                return null;
            }


        }

     

      public object ConvertBack(object value, Type targetType, object parameter, System.String culture)
      {
         throw new NotImplementedException();
      }
        
    }

}