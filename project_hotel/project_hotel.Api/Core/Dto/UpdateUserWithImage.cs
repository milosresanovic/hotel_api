using project_hotel.Application.UseCases.DTO;

namespace project_hotel.Api.Core.Dto
{
    public class UpdateUserWithImage : UpdateUserDto
    {
        public IFormFile? Image { get; set; }
    }
}
