using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using System.IO;
using Microsoft.AspNetCore.Http;
using ImageCompression.Models;
using AutoMapper;
using ImageCompression.Service.Abstract;
using ImageCompression.ViewModel;
using Newtonsoft.Json;
using ImageCompression.Service.InDTOs;

using System.Web;
using Microsoft.AspNetCore.Hosting.Server;
using System.Net.Mail;
using System.Net;

namespace ImageCompression.Controllers
{
    public class HomeController : Controller
    {

        private  IMapper _mapper;
        private  IImageService _imageService;

        public HomeController(IMapper mapper, IImageService imageService)
        {
            _mapper = mapper;
            _imageService = imageService;
        }

        public IActionResult Index()
        {

            return View();

        }
        public IActionResult Settings()
        {
            return View();
        }
        
        public async Task<IActionResult> Logs()
        {
            List<ImageLogsVM> imageLogs = new List<ImageLogsVM>();
            ViewData["Message"] = "Wyświetl logi";
           var images =await _imageService.GetAllImagesLogs();
            foreach (var image in images)
            {
                var logs = _mapper.Map<ImageLogsVM>(image);
                imageLogs.Add(logs);
            }
            
           
            HomeVM homeVM = new HomeVM();
            homeVM.imageLogsVM = imageLogs;
            return View(homeVM);
        }
        [HttpGet]
        public async Task<IActionResult> Download(string imageID)
        {
            var base64Image=await _imageService.GetBase64ImageById(imageID);

            byte[] imageBytes = Convert.FromBase64String(base64Image);
            var imageLog = await _imageService.GetImageLogById(imageID);


            return File(imageBytes, "application/force-download", imageLog.FirstOrDefault().filename);
        }
        [HttpGet]
        public async Task<IActionResult> DownloadMetadata(string imageID)
        {
            var imageLog = await _imageService.GetImageLogById(imageID);

            string output = JsonConvert.SerializeObject(imageLog, Formatting.Indented);
            string base64json = Base64Encode(output);
            byte[] jsonBytes = Convert.FromBase64String(base64json);

            return File(jsonBytes, "application/force-download", "plik.json");
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
        public void SendEmailLog(string toEmail)
        {
            SmtpClient client = new SmtpClient("SMTP.office365.com", 587);
            client.UseDefaultCredentials = false;
            client.EnableSsl = true;
            client.Credentials = new NetworkCredential("szwarcus1994@gmail.com", "Texas Rangers!2");

            MailMessage mailMessage = new MailMessage();
            mailMessage.From = new MailAddress("jakub.szwarc@outlook.com");
            mailMessage.To.Add("szwarcus1994@gmail.com");
            mailMessage.Body = "body";
            mailMessage.Subject = "subject";
            client.Send(mailMessage);

        }

        [HttpPost("UploadFiles")]
        public async Task<IActionResult> Post(List<IFormFile> files)
        {
            var imageVM = new ImageVM();
            long size = files.Sum(f => f.Length);
            foreach (var formFile in files)
            {
                imageVM.filename = formFile.FileName;
                imageVM.sizeBeforeCompression = formFile.Length;
                imageVM.processingStart = DateTime.Now;
                var stream = new MemoryStream();
                var outputStream = new MemoryStream();
                
              


                if (formFile.Length > 0)
                {
                        await formFile.CopyToAsync(stream);
                        stream.Position = 0;
                        string base64Image=LoselessCompression(stream, outputStream);
                        imageVM.processingEnd = DateTime.Now;
                        imageVM.sizeAfterCompression = outputStream.Length;
                        imageVM.blobImage = base64Image;

                }
               var image= _mapper.Map<ImageInDTO>(imageVM);

               await _imageService.InsertImageToDB(image);
            }


            // process uploaded files
            // Don't rely on or trust the FileName property without validation.

            return RedirectToAction("Index", "Home");
        }
        public string LoselessCompression(MemoryStream _inStream, MemoryStream _outStream)
        {

            byte[] jpegHeader = new byte[2];
            jpegHeader[0] = (byte)_inStream.ReadByte();
            jpegHeader[1] = (byte)_inStream.ReadByte();
            if (jpegHeader[0] == 255 && jpegHeader[1] == 216) //check if it's a jpeg file
            {
                RemoveHeader(_inStream);
            }
            _outStream.WriteByte(0xff);
            _outStream.WriteByte(0xd8);

            int readCount;
            byte[] readBuffer = new byte[4096];
            while ((readCount = _inStream.Read(readBuffer, 0, readBuffer.Length)) > 0)
                _outStream.Write(readBuffer, 0, readCount);

            _outStream.Position = 0; //    <-- Add this line
            byte[] imageBytes = _outStream.ToArray();

            // Convert byte[] to Base64 String
            string base64String = Convert.ToBase64String(imageBytes);
            return base64String;
        }

        private void RemoveHeader(Stream inStream)
        {
            byte[] header = new byte[2];
            header[0] = (byte)inStream.ReadByte();
            header[1] = (byte)inStream.ReadByte();

            while (header[0] == 0xff && (header[1] >= 0xe0 && header[1] <= 0xef))
            {
                int exifLength = inStream.ReadByte();
                exifLength = exifLength << 8;
                exifLength |= inStream.ReadByte();

                for (int i = 0; i < exifLength - 2; i++)
                {
                    inStream.ReadByte();
                }
                header[0] = (byte)inStream.ReadByte();
                header[1] = (byte)inStream.ReadByte();
            }
            inStream.Position -= 2; //skip back two bytes
        }
        public static string Base64Encode(string plainText)
        {
            var plainTextBytes = System.Text.Encoding.UTF8.GetBytes(plainText);
            return System.Convert.ToBase64String(plainTextBytes);
        }
        public static string Base64Decode(string base64EncodedData)
        {
            var base64EncodedBytes = System.Convert.FromBase64String(base64EncodedData);
            return System.Text.Encoding.UTF8.GetString(base64EncodedBytes);
        }
    }
}

