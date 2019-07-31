namespace ImageCompression.Service.Concrete
{
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using ImageCompression.Model.Entities;
    using ImageCompression.Repository.Abstract;
    using ImageCompression.Service.Abstract;
    using ImageCompression.Service.InDTOs;
    using ImageCompression.Service.OutDTOs;

    public class ImageService : IImageService
    {
        private readonly IRepository<Image> _imageRepository;



        public ImageService(IRepository<Image> imageRepository)
        {
            _imageRepository = imageRepository;
        }
        public async Task<bool> InsertImageToDB(ImageInDTO model)
        {
            var image = new Image
            {
                imageID = Guid.NewGuid(),
                filename = model.filename,
                processingStart = model.processingStart,
                processingEnd = model.processingEnd,
                sizeBeforeCompression = model.sizeBeforeCompression,
                sizeAfterCompression = model.sizeAfterCompression,
                blobImage = model.blobImage
            };

            await _imageRepository.InsertAsync(image);
            return true;
        }
        public async Task<List<ImageLogsOutDTO>> GetAllImagesLogs()
        {
            return await _imageRepository.GetAllAsync<ImageLogsOutDTO>(x => new ImageLogsOutDTO
            {
                imageID =x.imageID,
                filename = x.filename ,
                processingStart =x.processingStart ,
                processingEnd = x.processingEnd,
                sizeBeforeCompression = x.sizeBeforeCompression,
                sizeAfterCompression = x.sizeAfterCompression,
                compressionPercentage = (1 - (x.sizeAfterCompression / x.sizeBeforeCompression))*100,
                processingTime = (x.processingEnd - x.processingStart).TotalMilliseconds
            });
        }

        public async Task<List<ImageLogsOutDTO>> GetImageLogById(string ID)
        {
            Guid.TryParse(ID,out Guid _image_id);
            return await _imageRepository.GetAsync<ImageLogsOutDTO>(x => new ImageLogsOutDTO
            {
                imageID = x.imageID,
                filename = x.filename,
                processingStart = x.processingStart,
                processingEnd = x.processingEnd,
                sizeBeforeCompression = x.sizeBeforeCompression,
                sizeAfterCompression = x.sizeAfterCompression,
                compressionPercentage = (1 - (x.sizeAfterCompression / x.sizeBeforeCompression)) * 100,
                processingTime = (x.processingEnd - x.processingStart).TotalMilliseconds
            },x=>x.imageID== _image_id);
        }

        public async Task<string> GetBase64ImageById(string id)
        {
            Guid idToGuid;
            Guid.TryParse(id,out idToGuid);
            List<string> base64Image = await _imageRepository.GetAsync(x=>x.blobImage, x => x.imageID== idToGuid);
            return base64Image[0];
        }


    }
}