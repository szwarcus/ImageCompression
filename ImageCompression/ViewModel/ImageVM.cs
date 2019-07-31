using System;

namespace ImageCompression.ViewModel
{
    public class ImageVM
    {
        public string filename { get; set; }
        public DateTime processingStart { get; set; }
        public DateTime processingEnd { get; set; }
        public float sizeBeforeCompression { get; set; }
        public float sizeAfterCompression { get; set; }
        public string blobImage { get; set; }
    }
}
