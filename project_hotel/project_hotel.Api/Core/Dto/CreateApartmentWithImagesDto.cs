using project_hotel.Application.UseCases.DTO;

namespace project_hotel.Api.Core.Dto
{
    public class CreateApartmentWithImagesDto : CreateApartmentDto
    {
        public IEnumerable<IFormFile> Images { get; set; }
    }

    public class UpdateApartmentWithImagesDto : UpdateApartmentDto
    {
        public IEnumerable<IFormFile> Images { get; set; }
    }
}
