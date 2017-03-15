using AngleSharp.Parser.Html;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using AngleSharp;
using System.IO;

namespace WallpaperDownloader.Model
{
    class WallDownloader
    {
        private WebClient webclient;
        private HtmlParser parser;
        private IWallpaper website;

        private List<String> _imageLinks;
        public List<String> ImageLinks { get { return _imageLinks; } protected set { _imageLinks = value; } }

        public WallDownloader()
        {
            webclient = new WebClient();
            parser = new HtmlParser();
            website = new Wallhaven();
            _imageLinks = new List<string>();
        }
        public WallDownloader(string webAddres, int pageNumber):this()
        {
            website = new Wallhaven(webAddres, pageNumber);
        }

        /// <summary>
        /// Load Website
        /// </summary>
        /// <param name="website"></param>
        /// <returns></returns>
        public async Task LoadPageAsync(bool Next)
        {
            if (webclient == null || parser == null || website == null)
                return;

            string webAddress;

            if (Next)
                webAddress = website.NextPage();
            else
                webAddress = website.PreviousPage();
               
            try
            {
                string source = webclient.DownloadString(webAddress);
                var config = Configuration.Default.WithDefaultLoader();
                website.Document= await BrowsingContext.New(config).OpenAsync(webAddress);
            }
            catch
            {
                Console.WriteLine("Cant Load Website");
            }

        }
        /// <summary>
        /// Gets all images from website
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="website"></param>
        /// <returns></returns>
        public List<string> GetThumbLinks()
        {
            if (website == null)
                return null;

            var elements = website.GetThumbLinks();

            if (elements.Count() == 0)
               return null;

            ImageLinks.Clear();
                         
            foreach(var element in elements)
            {
                ImageLinks.Add(element);
                Console.WriteLine(element);
            }
            return ImageLinks;
        }
        /// <summary>
        /// Gets HD Link
        /// </summary>
        /// <param name="thumb"></param>
        /// <returns></returns>
        public string GetHdLink(string thumb)
        {
            if (website == null)
                return null;

            return website.GetHdLink(thumb);  
        }

        /// <summary>
        /// Download Image
        /// </summary>
        /// <param name="Remotepath"></param>
        /// <param name="LocalPath"></param>
        public void DownloadImage(string Remotepath, string LocalPath)
        {
            try
            {
                System.Diagnostics.Debug.WriteLine("***********" + Remotepath);
                var lastSlash = Remotepath.LastIndexOf("/");
                var fileName = Remotepath.Substring(lastSlash + 1);
                if (!Directory.Exists(LocalPath))
                    Directory.CreateDirectory(LocalPath);
                webclient.DownloadFileAsync(new Uri(Remotepath), LocalPath+fileName);
            }
            catch (Exception e)
            {
                System.Diagnostics.Debug.WriteLine(e.Message);
            }
        }
    }
}
