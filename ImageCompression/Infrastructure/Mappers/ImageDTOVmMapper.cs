
namespace ImageCompression.Infrastructure.Mappers
{
    using ImageCompression.Service.OutDTOs;
    using ImageCompression.ViewModel;
    using AutoMapper;
    public class ImageDTOVmMapper : Profile
    {
        public ImageDTOVmMapper()
        {
            CreateMap<ImageLogsOutDTO, ImageLogsVM>();
        }
    }
}
