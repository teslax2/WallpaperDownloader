using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace WallpaperDownloader.View
{
    /// <summary>
    /// Interaction logic for AnimatedTile.xaml
    /// </summary>
    public partial class AnimatedTile : UserControl
    {
        public string ImagePath { get; set; }
        public AnimatedTile()
        {
            InitializeComponent();
        }
        public AnimatedTile(string imagePath) : this()
        {
            try
            {
                ImagePath = imagePath;
                BitmapImage bitmap = new BitmapImage();
                bitmap.BeginInit();
                bitmap.UriSource = new Uri(imagePath);
                bitmap.EndInit();
                image.Source = bitmap;
            }
            catch (Exception e)
            {
                System.Diagnostics.Debug.WriteLine(e.Message);
            }
        }
    }
}
