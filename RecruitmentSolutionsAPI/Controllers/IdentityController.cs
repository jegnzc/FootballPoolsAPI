using System.Net;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using RecruitmentSolutionsAPI.Areas.Identity.Data;
using RecruitmentSolutionsAPI.Data;
using RecruitmentSolutionsAPI.Data.Context;
using RecruitmentSolutionsAPI.Models;
using RecruitmentSolutionsAPI.Models.Candidate;
using RecruitmentSolutionsAPI.Models.ExceptionHandlers;
using RecruitmentSolutionsAPI.Models.Responses;
using Microsoft.AspNetCore.Identity.UI.V4.Pages.Account.Internal;
using Microsoft.AspNetCore.WebUtilities;
using System.Text.Encodings.Web;
using System.Text;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.IdentityModel.Tokens;

namespace RecruitmentSolutionsAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class IdentityController : ControllerBase
    {
        private readonly SignInManager<User> _signInManager;
        private readonly UserManager<User> _userManager;
        private readonly IUserStore<User> _userStore;
        private readonly IUserEmailStore<User> _emailStore;
        private readonly ILogger<Register> _logger;
        private readonly ApplicationDbContext _context;

        public IdentityController(
             UserManager<User> userManager,
             IUserStore<User> userStore,
             SignInManager<User> signInManager,
             ILogger<Register> logger)
        {
            _userManager = userManager;
            _userStore = userStore;
            _emailStore = GetEmailStore();
            _signInManager = signInManager;
            _logger = logger;
        }

        private IUserEmailStore<User> GetEmailStore()
        {
            if (!_userManager.SupportsUserEmail)
            {
                throw new NotSupportedException("The default UI requires a user store with email support.");
            }
            return (IUserEmailStore<User>)_userStore;
        }

        private User CreateUser()
        {
            try
            {
                return Activator.CreateInstance<User>();
            }
            catch
            {
                throw new InvalidOperationException($"Can't create an instance of '{nameof(Areas.Identity.Data.User)}'. " +
                    $"Ensure that '{nameof(Areas.Identity.Data.User)}' is not an abstract class and has a parameterless constructor, or alternatively " +
                    $"override the register page in /Areas/Identity/Pages/Account/Register.cshtml");
            }
        }

        [HttpPost("login")]
        public async Task<JWTTokenResponse> Post(Login request)
        {
            // This doesn't count login failures towards account lockout
            // To enable password failures to trigger account lockout, set lockoutOnFailure: true
            var result = await _signInManager.PasswordSignInAsync(request.UserName, request.Password, true, lockoutOnFailure: false);
            if (result.Succeeded)
            {
                var secretKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("JWTAuthentication@777"));
                var signinCredentials = new SigningCredentials(secretKey, SecurityAlgorithms.HmacSha256);
                var tokeOptions = new JwtSecurityToken(
                    issuer: "http://localhost:7200",
                    audience: "http://localhost:7200",
                    //claims: new List<Claim>(),
                    expires: DateTime.Now.AddMinutes(6),
                    signingCredentials: signinCredentials
                );
                var tokenString = new JwtSecurityTokenHandler().WriteToken(tokeOptions);
                _logger.LogInformation("User logged in.");
                return new JWTTokenResponse { Token = tokenString };
            }

            if (result.IsLockedOut)
            {
                _logger.LogWarning("User account locked out.");
            }
            else
            {
                throw new HttpResponseException(500, "Invalid login attempt");
            }

            // If we got this far, something failed, redisplay form
            return new JWTTokenResponse();
        }

        public class JWTTokenResponse
        {
            public string? Token { get; set; }
        }

        [HttpGet("denied")]
        public IActionResult Get()
        {
            throw new HttpResponseException(500, "Unauthorized");
        }

        [HttpPost("register")]
        public async Task<RegisterResponse> Post(Register request)
        {
            var user = CreateUser();

            await _userStore.SetUserNameAsync(user, request.UserName, CancellationToken.None);
            await _emailStore.SetEmailAsync(user, request.Email, CancellationToken.None);
            var result = await _userManager.CreateAsync(user, request.Password);

            if (result.Succeeded)
            {
                _logger.LogInformation("User created a new account with password.");

                var userId = await _userManager.GetUserIdAsync(user);
            }
            foreach (var error in result.Errors)
            {
                throw new HttpResponseException(500, error.Description);
            }
            return new RegisterResponse()
            {
                User = user
            };
        }
    }
}