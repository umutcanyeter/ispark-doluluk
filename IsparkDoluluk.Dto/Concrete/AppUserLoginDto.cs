using IsparkDoluluk.Dto.Abstract;

namespace IsparkDoluluk.Dto.Concrete
{
    public class AppUserLoginDto : IDto
    {
        public string Username { get; set; }
        public string Password { get; set; }
    }
}