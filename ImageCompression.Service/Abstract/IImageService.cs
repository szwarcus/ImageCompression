namespace ImageCompression.Service.Abstract
{
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using ImageCompression.Model.Entities;
    using ImageCompression.Service.InDTOs;
    using ImageCompression.Service.OutDTOs;

    public interface IImageService
    {
        Task<bool> InsertImageToDB(ImageInDTO model);
        Task<List<ImageLogsOutDTO>> GetAllImagesLogs();
        Task<List<ImageLogsOutDTO>> GetImageLogById(string ID);
        Task<string> GetBase64ImageById(string id);

    }
}