using IsparkDoluluk.Dto.Abstract;
using System;
using System.Collections.Generic;
using System.Text;

namespace IsparkDoluluk.Dto.Concrete
{
    public class UserRegisterDto : IDto
    {
        public string Username { get; set; }
        public string Password { get; set; }
        public string Role { get; set; }
    }
}
