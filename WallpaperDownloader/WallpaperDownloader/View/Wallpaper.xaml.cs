using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace WallpaperDownloader.View
{
    /// <summary>
    /// Interaction logic for WallpaperDownloader.xaml
    /// </summary>
    public partial class Wallpaper : Window
    {
        ViewModel.WallpaperDownloaderViewModel viewModel;

        public Wallpaper()
        {
            InitializeComponent();
            viewModel = FindResource("ViewModel") as ViewModel.WallpaperDownloaderViewModel;
        }

        private void ItemsControl_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.OriginalSource is Image)
                viewModel.DownloadImage((e.OriginalSource as Image).Source.ToString());
        }

        private async void Window_Loaded(object sender, RoutedEventArgs e)
        {
           await viewModel.LoadAsyncNext();
        }

        private void ItemsControl_MouseMove(object sender, MouseEventArgs e)
        {
            var positionX = e.GetPosition(null).X;

            prevButton.Opacity = (100.0 - positionX)/100.0;
            nextButton.Opacity = (100.0 - (this.Width - positionX))/100.0;

            var tile = e.OriginalSource as UIElement;
            
        }

        private async void nextButton_Click(object sender, RoutedEventArgs e)
        {
            await viewModel.LoadAsyncNext();
        }

        private async void prevButton_Click(object sender, RoutedEventArgs e)
        {
            await viewModel.LoadAsyncPrev();
        }
    }
}
