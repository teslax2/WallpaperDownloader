using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WallpaperDownloader.Model
{
    class WallhavenSettings
    {
        public string SaveFolder { get; set; }
        public string WebsiteAddress { get; set; }
        public int DefaultPage { get; set; }

        public WallhavenSettings() { }
        public WallhavenSettings(string saveFolder, string websiteAddres, int defaulPage)
        {
            SaveFolder = saveFolder;
            WebsiteAddress = websiteAddres;
            DefaultPage = defaulPage;
        }

        public static void Save(WallhavenSettings settings)
        {
            try
            {
                var text = JsonConvert.SerializeObject(settings);
                File.WriteAllText("settings.json", text);
            }
            catch (Exception e)
            {

                System.Diagnostics.Debug.WriteLine(e.Message);
            }
        }

        public static WallhavenSettings Load()
        {
            try
            {
                return JsonConvert.DeserializeObject<WallhavenSettings>(File.ReadAllText("settings.json"));
            }
            catch (Exception e)
            {
                System.Diagnostics.Debug.WriteLine(e.Message);
                return new WallhavenSettings();
            }
        }
    }
}
