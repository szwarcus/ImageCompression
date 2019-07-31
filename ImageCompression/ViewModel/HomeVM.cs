using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ImageCompression.ViewModel
{
    public class HomeVM
    {
        public List<ImageLogsVM> imageLogsVM { get; set; }
        public string ImageBlob { get; set; }
    }
}
