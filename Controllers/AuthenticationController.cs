using System;
using System.Threading.Tasks;
using api.Domain.Models;
using api.Domain.Services;
using api.Resources;
using api.Util.Extensions;
using api.Util.Security;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;

namespace api.Controllers
{
    [Route("/api/[controller]")]
    [AllowAnonymous]
    public class AuthenticationController : ControllerBase
    {
        private readonly IUserService _userService;
        private readonly IMapper _mapper;
        private readonly IConfiguration _configuration;

        public AuthenticationController(IUserService userService, IMapper mapper, IConfiguration configuration)
        {
            _userService = userService;
            _mapper = mapper;
            _configuration = configuration;
        }

        [HttpPost]
        public async Task<ActionResult> VerifyLogin([FromBody] UserResource resource)
        {
            try
            {
                if (!ModelState.IsValid)
                    return BadRequest(ModelState.GetErrorMessages());
                
                var user = _mapper.Map<UserResource, User>(resource);
                var result = await _userService.FirstOrDefaultAsync(user.Login, user.Password);

                if (result.IsNull())
                    return BadRequest("Erro ao tentar realizar login");
                
                string token = CryptoFunctions.GenerateToken(_configuration, user);

                return Ok(new
                {
                    error = false,
                    result = new
                    {
                        token,
                        user = new
                        {
                            user.Id,
                            user.Login,
                            user.CreateDate,
                            user.UpdateDate 
                        }
                    }
                });
            }
            catch (Exception)
            {
                return BadRequest(new
                {
                    error = true,
                    result = "Erro ao tentar realizar login"
                });
            }
        }
    }
}