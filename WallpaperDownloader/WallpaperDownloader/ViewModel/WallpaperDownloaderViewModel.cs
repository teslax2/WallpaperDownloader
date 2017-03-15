using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using WallpaperDownloader.Model;
using WallpaperDownloader.View;

namespace WallpaperDownloader.ViewModel
{
    class WallpaperDownloaderViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        private readonly ObservableCollection<FrameworkElement> _sprites = new ObservableCollection<FrameworkElement>();
        public INotifyCollectionChanged Sprites { get { return _sprites; } }
        private Dictionary<AnimatedTile, string> _tiles = new Dictionary<AnimatedTile, string>();

        private WallDownloader model;
        private WallDownloaderSettings modelSettings;
        private readonly string _downloadPath;

        public WallpaperDownloaderViewModel()
        {
            if (LoadSettings())
                model = new WallDownloader(modelSettings.WebsiteAddress, modelSettings.DefaultPage);
            else
                model = new WallDownloader();

            _downloadPath = modelSettings.SaveFolder;
        }

        private void OnPropertyChanged(string property)
        {
            var propertyChanged = PropertyChanged;
            if (propertyChanged != null)
                propertyChanged(this, new PropertyChangedEventArgs(property));
        }

        public async Task LoadAsync(bool Next)
        {
            await Task.Run(()=>model.LoadPageAsync(Next));
            var links = model.GetThumbLinks();
            AddTiles(links);
        }
        public async Task LoadAsyncNext()
        {
            await LoadAsync(true);
        }

        public async Task LoadAsyncPrev()
        {
            await LoadAsync(false);
        }

        private void AddTiles(List<string> links)
        {
            if (links == null)
                return;

            if (links.Count > 0)
            {
                _sprites.Clear();
                _tiles.Clear();
                foreach(string link in links)
                {
                    var tile = new AnimatedTile(link);
                    _tiles.Add(tile, link);
                    _sprites.Add(tile);
                }
            }
        }
        public void DownloadImage(string path)
        {
            var hdLink = model.GetHdLink(path);
            model.DownloadImage(hdLink,_downloadPath);
        }

        public void SaveSettings()
        {
            WallDownloaderSettings.Save(modelSettings);
        }

        public bool LoadSettings()
        {
            modelSettings = WallDownloaderSettings.Load();

            if (modelSettings == null)
                return false;
            else
                return true;
        }
    }
}
