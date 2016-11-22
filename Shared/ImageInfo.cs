using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpotlightToDesktop
{
    public class ImageInfo
    {
        public string FileName
        {
            get;
            set;
        }

        public FileType Format
        {
            get;
            set;
        }

        public Size Dimensions
        {
            get;
            set;
        }

        public Orientation Orientation
        {
            get;
            set;
        }
    }

    public enum FileType
    {
        Unknown = 0,
        Png,
        Jpeg
    }

    public enum Orientation
    {
        Landscape,
        Portrait
    }
}
