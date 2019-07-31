using System;


namespace ImageCompression.Service.InDTOs
{
    public class ImageInDTO
    {
        public string filename { get; set; }
        public DateTime processingStart { get; set; }
        public DateTime processingEnd { get; set; }
        public float sizeBeforeCompression { get; set; }
        public float sizeAfterCompression { get; set; }
        public string blobImage { get; set; }
    }
}
