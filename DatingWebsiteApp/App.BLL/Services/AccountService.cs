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
        private readonly ApplicationSettings _applicationSettingsOption; 
        private readonly IEmailService _emailService;
        private readonly IFileService _fileService;
        private readonly IChatService _chatService;

        public AccountService(IUnitOfWork uow,
            UserManager<ApplicationUser> userManager, 
            IOptions<ApplicationSettings> applicationSettingsOption,
            IEmailService emailService,
            IChatService chatService,
            IFileService fileService)
        {
            _db = uow;
            _userManager = userManager; 
            _applicationSettingsOption = applicationSettingsOption.Value; 
            _emailService = emailService;
            _fileService = fileService;
            _chatService = chatService;
        }

        public async Task<string> RegisterUserAsync(RegisterVM model, string url)
        {
            try
            {
                var file_id = await _fileService.CreatePhotoAsync(null);
                var user = new ApplicationUser
                {
                    Name = model.Name,
                    Email = model.Email,
                    PasswordHash = model.Password,
                    UserName = model.Email,
                    FileId = file_id,
                    EmailConfirmed = false,
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
                var result = await _userManager.CreateAsync(user, model.Password);
                if (!result.Succeeded)
                {
                    throw new Exception("Register error");
                }
                await _chatService.SetLastOnlineAsync(user.Id);
                var code = await _userManager.GenerateEmailConfirmationTokenAsync(user);
                var encode = HttpUtility.UrlEncode(code);
                var callbackUrl = new StringBuilder("https://")
                    .AppendFormat(url)
                    .AppendFormat("/api/account/email")
                    .AppendFormat($"?user_id={user.Id}&code={encode}");

                await _emailService.SendEmailAsync(user.Email, "Confirm your account",
                    $"Confirm the registration by clicking on the link: <a href='{callbackUrl}'>link</a>");
                return user.Id;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<OperationDetails> ConfirmEmailAsync(string user_id, string code)
        {
            try
            {
                var db_user = await _userManager.FindByIdAsync(user_id);
                if (db_user == null)
                {
                    throw new Exception("User not found");
                }
                var success = await _userManager.ConfirmEmailAsync(db_user, code);
                return success.Succeeded ? new OperationDetails(true, "Success", "") : new OperationDetails(false, "Error", "");
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<object> LoginUserAsync(LoginVM model)
        {
            try
            {
                var user = await _userManager.FindByNameAsync(model.Email);
                if (user != null && await _userManager.CheckPasswordAsync(user, model.Password))
                {
                    if (!user.EmailConfirmed)
                    {
                        throw new Exception("Your Email not confirm");
                    }
                    await _chatService.SetLastOnlineAsync(user.Id);
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
                    throw new Exception("User not found or invalid email/password");
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task EditAccountInfo(UserAccountInfoEditVM model)
        {
            try
            {
                var user = await _userManager.FindByIdAsync(model.Id);
                if (!String.IsNullOrEmpty(model.NewPassword) && !String.IsNullOrEmpty(model.OldPassword))
                {
                    if (user != null && await _userManager.CheckPasswordAsync(user, model.OldPassword))
                    { 
                        string code = await _userManager.GeneratePasswordResetTokenAsync(user);
                        var result = await _userManager.ResetPasswordAsync(user, code, model.NewPassword);

                        if (!result.Succeeded)
                        { 
                            throw new Exception("Change password fail");
                        }
                    }
                    else
                    {
                        throw new Exception("User not found or invalid OldPassword");
                    }
                }
                if (!String.IsNullOrEmpty(model.Name)) { user.Name = model.Name; }
                if (model.IsAnonimus != null) { user.IsAnonimus = model.IsAnonimus.Value; }
                var edit_result = await _userManager.UpdateAsync(user);
                if (edit_result.Succeeded)
                {
                    //return new UserInfoShowVM(user, null);
                }
                else
                {
                    throw new Exception("Edit user info fail");
                }
                await _emailService.SendEmailAsync(user.Email, "Edit your account info",
                   $"Your Account Info has been updating.");
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void Dispose()
        {
            _db.Dispose();
        }
    }
}
