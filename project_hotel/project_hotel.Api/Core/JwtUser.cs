using project_hotel.Domain;

namespace project_hotel.Api.Core
{
    public class JwtUser : IApplicationUser
    {
        public string Identity { get; set; }
        public int Id { get; set; }
        public IEnumerable<int> UseCaseIds { get; set; } = new List<int>();
        public string Email { get; set; }
        public string Username { get; set; }
    }

}
