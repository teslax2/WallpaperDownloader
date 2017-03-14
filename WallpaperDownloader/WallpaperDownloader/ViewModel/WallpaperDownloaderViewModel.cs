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
        private int _pageNumber;

        private Wallhaven model = new Wallhaven();
        private WallhavenSettings modelSettings;
        private readonly string website;
        private readonly string _downloadPath;

        public WallpaperDownloaderViewModel()
        {
            LoadSettings();
            website = modelSettings.WebsiteAddress;
            _pageNumber = modelSettings.DefaultPage;
            _downloadPath = modelSettings.SaveFolder;
        }

        private void OnPropertyChanged(string property)
        {
            var propertyChanged = PropertyChanged;
            if (propertyChanged != null)
                propertyChanged(this, new PropertyChangedEventArgs(property));
        }

        public async Task LoadAsync(int page)
        {
            await Task.Run(()=>model.LoadPageAsync(website + page));
            var links = model.GetThumbLinks();
            AddLinks(links);
        }
        public async Task LoadAsyncNext()
        {
            _pageNumber++;
            await LoadAsync(_pageNumber);
        }

        public async Task LoadAsyncPrev()
        {
            if(_pageNumber>2)
                _pageNumber--;

            await LoadAsync(_pageNumber);
        }

        private void AddLinks(List<string> links)
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
            WallhavenSettings.Save(modelSettings);
        }

        public void LoadSettings()
        {
            modelSettings = WallhavenSettings.Load();
        }
    }
}
