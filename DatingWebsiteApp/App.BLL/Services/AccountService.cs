using App.BLL.Infrastructure;
using App.BLL.Interfaces;
using App.BLL.Models;
using App.BLL.ViewModels;
using App.DAL.Interfaces;
using App.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Linq;

namespace App.BLL.Services
{
    public class AccountService:IAccountService
    {
        private IUnitOfWork _db { get; set; }
        private readonly UserManager<ApplicationUser> _userManager;
        //private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly ApplicationSettings _applicationSettingsOption; 
        private readonly IEmailService _emailService;

        public AccountService(IUnitOfWork uow,
            UserManager<ApplicationUser> userManager,
            //SignInManager<ApplicationUser> signManager,
            IOptions<ApplicationSettings> applicationSettingsOption,
            IEmailService emailService,
            //IMapper mapper,
            IFileService fileService)
        {
            _db = uow;
            _userManager = userManager;
           // _signInManager = signManager;
            _applicationSettingsOption = applicationSettingsOption.Value; 
            _emailService = emailService; 
        }

        public async Task<object> RegisterUserAsync(RegisterVM model, string url)
        { 
            var user = new ApplicationUser
            {
                Name = model.Name, 
                Email = model.Email, 
                PasswordHash = model.Password,
                UserName = model.Email
            }; 
            try
            {
                var result = await _userManager.CreateAsync(user, model.Password);
                await _userManager.AddToRoleAsync(user, "user");
                var code = await _userManager.GenerateEmailConfirmationTokenAsync(user);
                var encode = HttpUtility.UrlEncode(code);
                var callbackUrl = new StringBuilder("https://")
                    .AppendFormat(url)
                    .AppendFormat("/api/account/email")
                    .AppendFormat($"?user_id={user.Id}&code={encode}");

                //await _emailService.SendEmailAsync(user.Email, "Confirm your account",   !!!
                //    $"Confirm the registration by clicking on the link: <a href='{callbackUrl}'>link</a>");
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<OperationDetails> ConfirmEmailAsync(string user_id, string code)
        {
            var db_user = await _userManager.FindByIdAsync(user_id);
            var success = await _userManager.ConfirmEmailAsync(db_user, code);
            return success.Succeeded ? new OperationDetails(true, "Success", "") : new OperationDetails(false, "Error", ""); 
        }

        public async Task<object> LoginUserAsync(LoginVM model)
        {
            var user = await _userManager.FindByNameAsync(model.Email);
            if (user != null && await _userManager.CheckPasswordAsync(user, model.Password))
            {
                var role = await _userManager.GetRolesAsync(user);
                var options = new IdentityOptions();

                var tokenDescriptor = new SecurityTokenDescriptor
                {
                    Subject = new ClaimsIdentity(new Claim[]
                    {
                        new Claim("UserID", user.Id), 
                    }),
                    Expires = DateTime.UtcNow.AddDays(1),
                    SigningCredentials =
                        new SigningCredentials(
                            new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_applicationSettingsOption.JwT_Secret)),
                            SecurityAlgorithms.HmacSha256Signature)
                };
                var tokenHandler = new JwtSecurityTokenHandler();
                var securityToken = tokenHandler.CreateToken(tokenDescriptor);
                var token = tokenHandler.WriteToken(securityToken);
                return token;
            }
            else
                return null;
        } 

        public void Dispose()
        {
            _db.Dispose();
        }
    }
}
