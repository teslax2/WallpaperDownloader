using AngleSharp.Parser.Html;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using AngleSharp;
using System.IO;
using AngleSharp.Dom;

namespace WallpaperDownloader.Model
{
    class Wallhaven:IWallpaper
    {
        public IDocument Document { get; set; }
        public string WebAddress { get; set; }
        public int PageNumber { get; set; }

        public Wallhaven()
        {
            WebAddress = "https://alpha.wallhaven.cc/search?categories=111&purity=110&sorting=favorites&order=desc&page=";
            PageNumber = 1;
        }

        public Wallhaven(string webAddress, int pageNumber)
        {
            WebAddress = webAddress;
            PageNumber = pageNumber;
        }
        /// <summary>
        /// Gets all thumbs from website
        /// </summary>
        /// <returns></returns>
        public List<string> GetThumbLinks()
        {
            if (Document == null)
                return null;

            var selectors = Document.GetElementById("thumbs").QuerySelectorAll("img");
            var elements = selectors.Select(n => (n.Attributes["data-src"].Value)).Distinct().ToList<string>();

            return elements;
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

        public string NextPage()
        {
            PageNumber++;
            return WebAddress + PageNumber;
        }

        public string PreviousPage()
        {
            if(PageNumber>1)
                PageNumber--;
            return WebAddress + PageNumber;
        }
    }
}
