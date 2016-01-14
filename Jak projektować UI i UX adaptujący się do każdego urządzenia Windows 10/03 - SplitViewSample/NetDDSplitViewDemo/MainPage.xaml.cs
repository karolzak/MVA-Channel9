using System;
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

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace NetDDSplitViewDemo
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        public MainPage()
        {
            this.InitializeComponent();
        }

        private void AppBarButton_Click(object sender, RoutedEventArgs e)
        {
            this.splitView1.DisplayMode = SplitViewDisplayMode.Inline;
        }

        private void AppBarButton_Click_1(object sender, RoutedEventArgs e)
        {

            this.splitView1.DisplayMode = SplitViewDisplayMode.Overlay;
        }

        private void AppBarButton_Click_2(object sender, RoutedEventArgs e)
        {
            this.splitView1.DisplayMode = SplitViewDisplayMode.CompactInline;
        }

        private void AppBarButton_Click_3(object sender, RoutedEventArgs e)
        {
            this.splitView1.DisplayMode = SplitViewDisplayMode.CompactOverlay;
            
        }

        private void AppBarButton_Click_4(object sender, RoutedEventArgs e)
        {
            splitView1.IsPaneOpen = !splitView1.IsPaneOpen;
        }
    }
}
