using Expire_Api.DTOS.Seller;
using Expire_Api.Interface;
using Expire_Api.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace Expire_Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;

        public AuthController(IAuthService authService)
        {
            _authService = authService;
        }

        [HttpPost("Register")]
        public async Task<IActionResult> RegisterSeller([FromBody] SellerRegister seller)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var result = await _authService.SellerRegister(seller);

            if (!result.IsAuth)
                return BadRequest(result.Massage);
            return Ok(result);
        }


        [HttpPost("Login")]
        public async Task<IActionResult> Login([FromBody] SellerLogin model)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var result = await _authService.SellerLogin(model);

            if (!result.IsAuth)
                return BadRequest(result.Massage);

            return Ok(result);
        }


        [HttpPost("AddNewRole")]
        public async Task<IActionResult> AddNewRole([FromBody, Required, StringLength(256)] string Name)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var isRoleExist = await _authService.AddRole(Name);
            if (!isRoleExist)
                return BadRequest($"Role {Name} is exist");

            return Ok($"Role {Name} added successfully");
        }
    }
}
