using project_hotel.Domain;

namespace project_hotel.Api.Core
{
    public class AnonimousUser : IApplicationUser
    {
        public string Username => "Anonimous";

        public int Id => 1;

        public IEnumerable<int> UseCaseIds => new List<int> { 6, 8, 9};

        public string Email => "anonimous@gmail.com";
    }
}
