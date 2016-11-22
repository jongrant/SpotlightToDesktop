using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpotlightToDesktop
{
    public static class Inspector
    {
        private const string SpotlightPath = @"%LocalAppData%\Packages\Microsoft.Windows.ContentDeliveryManager_cw5n1h2txyewy\LocalState\Assets";
        private const long MinimumSize = 51200;
        
        public static List<ImageInfo> GetWallpaperCandidates()
        {
            var candidates = new List<ImageInfo>();

            string path = Environment.ExpandEnvironmentVariables(SpotlightPath);
            if (!Directory.Exists(path))
            {
                throw new Exception("Windows Spotlight image cache folder does not exist!");
            }

            DirectoryInfo dir = new DirectoryInfo(path);
            var files = dir.GetFiles();

            foreach (var file in files)
            {
                // ignore small files
                if (file.Length < MinimumSize) continue;

                // get the full metadata
                var info = Inspector.Inspect(Path.Combine(file.DirectoryName, file.Name));

                // ignore any that are not landscape
                if (info.Orientation != Orientation.Landscape) continue;

                // seems good, save it
                candidates.Add(info);
            }

            return candidates;
        }

        public static ImageInfo Inspect(string file)
        {
            // safety check. we shouldn't be inspecting files this size anyway
            if (file.Length < 8) return null;

            // prepare a result object
            var result = new ImageInfo { FileName = Path.GetFullPath(file), Key = Path.GetFileName(file) };

            using (var image = Image.FromFile(file))
            {
                result.Dimensions = image.Size;

                if (image.Width < image.Height) result.Orientation = Orientation.Portrait;
                else result.Orientation = Orientation.Landscape;

                if (image.RawFormat.Guid == ImageFormat.Png.Guid) result.Format = FileType.Png;
                else if (image.RawFormat.Guid == ImageFormat.Jpeg.Guid) result.Format = FileType.Jpeg;
            }

            return result;
        }

        public static string ByteArrayToString(byte[] ba)
        {
            StringBuilder hex = new StringBuilder(ba.Length * 2);
            foreach (byte b in ba) hex.AppendFormat("{0:x2}", b);
            return hex.ToString();
        }
    }
}
