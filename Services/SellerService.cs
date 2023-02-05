using Expire_Api.Classes;
using Expire_Api.DTOS.Seller;
using Expire_Api.Interface;
using Expire_Api.Models;
using Microsoft.AspNetCore.Identity;
using System.IdentityModel.Tokens.Jwt;
using System.Linq.Expressions;
using System.Numerics;

namespace Expire_Api.Services
{
    public class SellerService : BaseRepository<Seller> , ISellerService
    {

        private readonly UserManager<Seller> _userManager;
        private readonly ApplicationDbContext _context;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly SignInManager<Seller> _signInManager;

        public SellerService(ApplicationDbContext context, UserManager<Seller> userManager, RoleManager<IdentityRole> roleManager, SignInManager<Seller> signInManager) : base(context)
        {
            _context = context;
            _userManager = userManager;
            _roleManager = roleManager;
            _signInManager = signInManager;
        }

        public async Task<Seller> FindSellerById(string sellerId)
        {
            Expression<Func<Seller, bool>> criteria = d => d.Id == sellerId;
            var seller = await Find(criteria);
            if (seller == null) return null; 
            return seller;
        }
        public async Task<Seller> FindSellerByIdWithData(string sellerId)
        {
            Expression<Func<Seller, bool>> criteria = d => d.Id == sellerId;
            var seller = await FindWithData(criteria);
            if (seller == null) return null; 
            return seller;
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
            var result = await _userManager.CreateAsync(seller,sellerDto.Password);
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
            return new SellerReturnDto
            {

                Email = seller.Email,
                IsAuth = true,
                Roles = new List<string> { RoleNames.SellerRole },
                UserName = seller.UserName,
                Massage = "Seller Created Successfully"
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
            var log = await _signInManager.PasswordSignInAsync(user, sellerLogin.password, true, false);
            //check if email is exist
            if (user is null || !log.Succeeded)
            {
                sellerReturnDto.Massage = "Email or Password is incorrect";
                return sellerReturnDto;
            }

            var roles = await _userManager.GetRolesAsync(user);

            sellerReturnDto.IsAuth = true;
            sellerReturnDto.Email = user.Email;
            sellerReturnDto.UserName = user.UserName;
            sellerReturnDto.Roles = roles.ToList();
            sellerReturnDto.Massage = "Login Successfully";

            return sellerReturnDto;
        }
    }
}
