using AutoMapper;
using IsparkDoluluk.Api.CustomErrors;
using IsparkDoluluk.Api.CustomFilters;
using IsparkDoluluk.Business.Abstract;
using IsparkDoluluk.Business.StringInfos;
using IsparkDoluluk.Dto.Concrete;
using IsparkDoluluk.Entities.Concrete;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IsparkDoluluk.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IJwtService _jwtService;
        private readonly IAppUserService _appUserService;
        private readonly IMapper _mapper;
        private readonly IAppUserRoleService _appUserRoleService;

        public AuthController(IJwtService jwtService, IAppUserService appUserService, IMapper mapper, IAppUserRoleService appUserRoleService)
        {
            _jwtService = jwtService;
            _appUserService = appUserService;
            _mapper = mapper;
            _appUserRoleService = appUserRoleService;
        }

        [HttpPost("login")]
        [ValidModel]
        public async Task<IActionResult> Login([FromBody] AppUserLoginDto appUserLoginDto)
        {
            List<ErrorModel> errorModels = new List<ErrorModel>();
            var user = await _appUserService.FindByUserName(appUserLoginDto.Username);
            if (user == null)
            {
                var error = new ErrorModel()
                {
                    FieldName = "Username",
                    Message = $"{appUserLoginDto.Username} uyuşmuyor."
                };
                errorModels.Add(error);
                var response = new ErrorResponse()
                {
                    Errors = errorModels
                };
                return BadRequest(response);
            }
            else
            {
                if (_appUserService.CheckPassword(appUserLoginDto).Result)
                {
                    var roles = await _appUserService.GetRolesByUserName(appUserLoginDto.Username);
                    var token = _jwtService.GenerateJwt(user, roles);
                    LoginSuccessDto loginSuccessDto = new LoginSuccessDto()
                    {
                        Token = token,
                        Username = user.Username
                    };
                    return Ok(loginSuccessDto);
                }
                var error = new ErrorModel()
                {
                    FieldName = "Password",
                    Message = $"Şifre uyuşmuyor."
                };
                errorModels.Add(error);
                var response = new ErrorResponse()
                {
                    Errors = errorModels
                };
                return BadRequest(response);
            }
        }

        [HttpPost("register")]
        [ValidModel]
        [Authorize(Roles = RoleInfo.Admin)]
        public async Task<IActionResult> Register([FromBody] UserRegisterDto userRegisterDto)
        {
            List<ErrorModel> errorModels = new List<ErrorModel>();
            var user = await _appUserService.FindByUserName(userRegisterDto.Username);
            if (user != null)
            {
                var error = new ErrorModel()
                {
                    FieldName = "Username",
                    Message = $"{userRegisterDto.Username} daha önce alınmış."
                };
                errorModels.Add(error);
                var response = new ErrorResponse()
                {
                    Errors = errorModels
                };
                return BadRequest(response);
            }

            await _appUserService.Add(_mapper.Map<AppUser>(userRegisterDto));
            var userInDb = await _appUserService.FindByUserName(userRegisterDto.Username);
            var role = await _appUserRoleService.FindByName(userRegisterDto.Role);

            await _appUserRoleService.Add(new AppUserRole()
            {
                AppUserId = userInDb.Id,
                AppRoleId = role.Id
            });
            return Ok("Kullanıcı oluşturuldu.");
        }
    }
}
