using Expire_Api.Classes;
using Expire_Api.DTOS.Seller;
using Expire_Api.Helpers;
using Expire_Api.Interface;
using Expire_Api.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using NuGet.Common;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Expire_Api.Services
{
    public class AuthService : IAuthService
    {
        private readonly UserManager<Seller> _userManager;
        private readonly ApplicationDbContext _context;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly SignInManager<Seller> _signInManager;
        private readonly JWT _jwt;

        public AuthService(SignInManager<Seller> signInManager, RoleManager<IdentityRole> roleManager, UserManager<Seller> userManager, ApplicationDbContext context, IOptions<JWT> jwt)
        {
            _signInManager = signInManager;
            _roleManager = roleManager;
            _userManager = userManager;
            _context = context;
            _jwt = jwt.Value;
        }
        private async Task<JwtSecurityToken> CreateJwtToken(Seller user)
        {
            var userClaims = await _userManager.GetClaimsAsync(user);
            var roles = await _userManager.GetRolesAsync(user);
            var roleClaims = new List<Claim>();

            foreach (var role in roles)
                roleClaims.Add(new Claim("roles", role));

            var claims = new[]
            {
                new Claim(JwtRegisteredClaimNames.Sub, user.UserName),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim(JwtRegisteredClaimNames.Email, user.Email),
                new Claim("uid", user.Id)
            }
            .Union(userClaims)
            .Union(roleClaims);

            var symmetricSecurityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwt.Key));
            var signingCredentials = new SigningCredentials(symmetricSecurityKey, SecurityAlgorithms.HmacSha256);
            var jwtSecurityToken = new JwtSecurityToken(
                issuer: _jwt.Issuer,
                audience: _jwt.Audience,
                claims: claims,
                expires: DateTime.Now.AddDays(_jwt.DurationInDays),
                signingCredentials: signingCredentials);

            return jwtSecurityToken;
        }

        public async Task<SellerReturnDto> SellerRegister(SellerRegister sellerDto)
        {

            //check if email is exist
            if (await _userManager.FindByEmailAsync(sellerDto.Email) is not null)
                return new SellerReturnDto { Massage = "Email is alerdy register!" };

            //check if userName is exist
            if (await _userManager.FindByNameAsync(sellerDto.UserName) is not null)
                return new SellerReturnDto { Massage = "UserName is alerdy register!" };

            var seller = new Seller
            {
                Email = sellerDto.Email,
                FullName = sellerDto.FullName,
                Gender = sellerDto.Gender,
                PhoneNumber = sellerDto.PhoneNo,
                UserName = sellerDto.UserName,
            };
            var result = await _userManager.CreateAsync(seller, sellerDto.Password);
            if (!result.Succeeded)
            {
                var errors = string.Empty;
                foreach (var error in result.Errors)
                {
                    errors += $"{error.Description}, ";
                }
                return new SellerReturnDto { Massage = errors };
            }
            // if creation is success assign role to doctor
            await _userManager.AddToRoleAsync(seller, RoleNames.SellerRole);

            var jwtSecurityToken = await CreateJwtToken(seller);

            return new SellerReturnDto
            {
                Id = seller.Id,
                Email = seller.Email,
                IsAuth = true,
                Roles = new List<string> { RoleNames.SellerRole },
                UserName = seller.UserName,
                Massage = "Seller Created Successfully",
                Token = new JwtSecurityTokenHandler().WriteToken(jwtSecurityToken),
                ExpiresOn = jwtSecurityToken.ValidTo
               
            };
        }
        public async Task<bool> AddRole(string roleName)
        {
            var roleExist = await _roleManager.RoleExistsAsync(roleName);
            if (roleExist)
                return false;
            //if not exist add new role 
            await _roleManager.CreateAsync(new IdentityRole(roleName));
            return true;
        }
        public async Task<SellerReturnDto> SellerLogin(SellerLogin sellerLogin)
        {
            var sellerReturnDto = new SellerReturnDto();
            var user = await _userManager.FindByNameAsync(sellerLogin.username);
            //check if email is exist
            if (user is null || !await _userManager.CheckPasswordAsync(user, sellerLogin.password))
            {
                sellerReturnDto.Massage = "Email or Password is incorrect";
                return sellerReturnDto;
            }


            var jwtSecurityToken = await CreateJwtToken(user);
            var roles = await _userManager.GetRolesAsync(user);

            sellerReturnDto.Id = user.Id;
            sellerReturnDto.IsAuth = true;
            sellerReturnDto.Email = user.Email;
            sellerReturnDto.UserName = user.UserName;
            sellerReturnDto.Roles = roles.ToList();
            sellerReturnDto.Massage = "Login Successfully";
            sellerReturnDto.Token = new JwtSecurityTokenHandler().WriteToken(jwtSecurityToken);
            sellerReturnDto.ExpiresOn = jwtSecurityToken.ValidTo;
            await _signInManager.PasswordSignInAsync(user, sellerLogin.password, true, false);
            return sellerReturnDto;
        }
    }
}
