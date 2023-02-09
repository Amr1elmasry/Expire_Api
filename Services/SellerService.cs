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
        
    }
}
