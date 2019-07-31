namespace ImageCompression.Infrastructure.Mappers
{

   // using ImageCompression.Service.OutDTOs;
    using ImageCompression.Service.InDTOs;
    using AutoMapper;
    using ImageCompression.ViewModel;
    using ImageCompression.Service.OutDTOs;

    public class ImageVmDTOMapper : Profile
    {
        public ImageVmDTOMapper()
        {
            CreateMap<ImageVM, ImageInDTO>();
        }
    }
}
