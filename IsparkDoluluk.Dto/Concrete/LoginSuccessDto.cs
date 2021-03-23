using IsparkDoluluk.Dto.Abstract;
using System;
using System.Collections.Generic;
using System.Text;

namespace IsparkDoluluk.Dto.Concrete
{
    public class LoginSuccessDto : IDto
    {
        public string Token { get; set; }
        public string Username { get; set; }
    }
}
