using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ImageCompression.ViewModel
{
    public class ImageLogsVM
    {
        public Guid imageID { get; set; }
        public string filename { get; set; }
        public DateTime processingStart { get; set; }
        public DateTime processingEnd { get; set; }
        public float sizeBeforeCompression { get; set; }
        public float sizeAfterCompression { get; set; }
        public float compressionPercentage { get; set; }
        public double processingTime { get; set; }
    }
}
