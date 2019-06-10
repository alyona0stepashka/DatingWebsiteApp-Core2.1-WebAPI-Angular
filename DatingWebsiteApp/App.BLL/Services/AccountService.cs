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
        private readonly IFileService _fileService;

        public AccountService(IUnitOfWork uow,
            UserManager<ApplicationUser> userManager,
            //SignInManager<ApplicationUser> signManager,
            IOptions<ApplicationSettings> applicationSettingsOption,
            IEmailService emailService, 
            IFileService fileService)
        {
            _db = uow;
            _userManager = userManager;
           // _signInManager = signManager;
            _applicationSettingsOption = applicationSettingsOption.Value; 
            _emailService = emailService;
            _fileService = fileService;
        }

        public async Task<object> RegisterUserAsync(RegisterVM model, string url)
        {
            var file_id = await _fileService.CreatePhotoAsync(null); 
            var user = new ApplicationUser
            {
                Name = model.Name, 
                Email = model.Email, 
                PasswordHash = model.Password,
                UserName = model.Email,
                FileId = file_id,
                EmailConfirmed =false,
                IsAnonimus = false,
                SexId = _db.Sexes.GetAll().FirstOrDefault().Id,
                MainGoalId = _db.MainGoals.GetAll().FirstOrDefault().Id,
                Type = new PersonalType
                { 
                    EducationId = _db.Educations.GetAll().FirstOrDefault().Id,
                    FamilyStatusId = _db.FamilyStatuses.GetAll().FirstOrDefault().Id,
                    FinanceStatusId = _db.FinanceStatuses.GetAll().FirstOrDefault().Id,
                    NationalityId = _db.Nationalities.GetAll().FirstOrDefault().Id,
                    ZodiacId = _db.Zodiacs.GetAll().FirstOrDefault().Id,
                }
            }; 
            try
            {
                var result = await _userManager.CreateAsync(user, model.Password); 
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

        public async Task<object> EditAccountInfo(UserAccountInfoEditVM model)
        {
            var user = await _userManager.FindByIdAsync(model.Id);
            if (model.NewPassword != null && model.OldPassword != null)
            { 
                if (user != null && await _userManager.CheckPasswordAsync(user, model.OldPassword))
                {
                    string code = await _userManager.GeneratePasswordResetTokenAsync(user);
                    var result = await _userManager.ResetPasswordAsync(user, code, model.NewPassword);

                    if (result.Succeeded)
                    {
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
                    {
                        return null;
                    }
                }
                else
                {
                    return null;
                }
            }
            if (model.Name != null) { user.Name = model.Name; }
            if (model.IsAnonimus!=null) { user.IsAnonimus = model.IsAnonimus.Value; }
            var edit_result = await _userManager.UpdateAsync(user);
            if (edit_result.Succeeded)
            {
                return new UserInfoShowVM(user);
            }
            else
            {
                return null;
            }

        }

        public void Dispose()
        {
            _db.Dispose();
        }
    }
}
