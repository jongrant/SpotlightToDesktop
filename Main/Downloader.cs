using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

using Newtonsoft.Json;
using System.IO;
using System.Text.RegularExpressions;

namespace SpotlightToDesktop
{
    static class Downloader
    {
        public static List<ImageInfo> GetWallpaperCandidates()
        {
            var candidates = new List<ImageInfo>();

            using (var client = new WebClient())
            {
                string json = client.DownloadString(ConfigurationManager.AppSettings["Url"]);

                dynamic jsonData = JsonConvert.DeserializeObject(json);
                var item = jsonData.batchrsp.items[0].item;

                dynamic itemData = JsonConvert.DeserializeObject(item.Value);
                dynamic img = itemData.ad.image_fullscreen_001_landscape;

                var url = (string)img.u;

                var filter = new Regex("[^a-z0-9]+", RegexOptions.IgnoreCase);
                var hash = filter.Replace((string)img.sha256, "");

                var tempFile = Path.Combine(Path.GetTempPath(), hash);
                client.DownloadFile(url, tempFile);

                candidates.Add(Inspector.Inspect(tempFile));
            }

            return candidates;
        }
    }
}
