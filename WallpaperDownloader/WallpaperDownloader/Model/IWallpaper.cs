using AngleSharp.Dom;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WallpaperDownloader.Model
{
    interface IWallpaper
    {
        IDocument Document { get; set; }
        String WebAddress { get; set; }
        int PageNumber { get; set; }

        List<String> GetThumbLinks();
        string GetHdLink(string thumb);
        string GetNextPage();
        string PreviousPage();
    }
}
