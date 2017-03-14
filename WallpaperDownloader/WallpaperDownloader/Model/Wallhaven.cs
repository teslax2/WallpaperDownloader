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
    class Wallhaven
    {
        private WebClient webclient;
        private HtmlParser parser;
        private AngleSharp.Dom.IDocument document;

        private List<String> _imageLinks;
        public List<String> ImageLinks { get { return _imageLinks; } protected set { _imageLinks = value; } }

        public Wallhaven()
        {
            webclient = new WebClient();
            parser = new HtmlParser();
            _imageLinks = new List<string>();
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="website"></param>
        /// <returns></returns>
        public async Task LoadPageAsync(string website)
        {
            if (webclient == null || parser == null)
                return;

            try
            {
                string source = webclient.DownloadString(website);// https://alpha.wallhaven.cc/search?categories=111&purity=110&sorting=favorites&order=desc&page=2
                var config = Configuration.Default.WithDefaultLoader();
                document= await BrowsingContext.New(config).OpenAsync(website);
            }
            catch
            {
                Console.WriteLine("Cant Load Website");
            }

        }

        public List<string> GetThumbLinks()
        {
            if (document == null)
                return null;

            var selectors = document.GetElementById("thumbs").QuerySelectorAll("img");
            var elements = selectors.Select(n => (n.Attributes["data-src"].Value)).Distinct();


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
        /// changes "https://alpha.wallhaven.cc/wallpapers/thumb/small/th-116915.jpg"  to "https://wallpapers.wallhaven.cc/wallpapers/full/wallhaven-116915.jpg 
        /// </summary>
        /// <param name="thumb"></param>
        /// <returns></returns>
        public string GetHdLink(string thumb)
        {
            if (string.IsNullOrEmpty(thumb) && !thumb.Contains("thumb/small"))
                return null;
            
            return thumb.Replace("thumb/small/th", "full/wallhaven");   
        }

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
