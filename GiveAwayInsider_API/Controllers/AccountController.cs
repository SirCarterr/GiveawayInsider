using GiveAwayInsider_API.Helper;
using GiveAwayInsider_API.Service.IService;
using GiveAwayInsider_Business.Repository.IRepository;
using GiveAwayInsider_DataAccess;
using GiveAwayInsider_Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace GiveAwayInsider_API.Controllers
{
    [ApiController]
    [Route("api/[controller]/[action]")]
    public class AccountController : Controller
    {
        private readonly SignInManager<AppUser> _signInManager;
        private readonly UserManager<AppUser> _userManager;
        private readonly APISettings _aPISettings;
        private readonly HttpClient _httpClient;
        private readonly IEmailService _emailService;
        private readonly ISettingsRepository _settingsRepository;

        public AccountController(SignInManager<AppUser> signInManager, UserManager<AppUser> userManager, IOptions<APISettings> options, HttpClient httpClient,
            IEmailService emailService, ISettingsRepository settingsRepository)  
        {
            _signInManager = signInManager;
            _userManager = userManager;
            _aPISettings = options.Value;
            _httpClient = httpClient;
            _emailService = emailService;
            _settingsRepository = settingsRepository;
        }

        [HttpPost]
        [ActionName("signup")]
        public async Task<IActionResult> SignUp([FromBody] SignUpRequestDTO signUpRequestDTO)
        {
            if (signUpRequestDTO == null || !ModelState.IsValid)
            {
                return BadRequest();
            }

            var response = await _httpClient.GetAsync($"https://api.mailcheck.ai/domain/{signUpRequestDTO.Email.Split('@')[1]}");
            var content = await response.Content.ReadAsStringAsync();
            if (response.IsSuccessStatusCode)
            {
                ValidatorResponseDTO? validatorResponse = JsonConvert.DeserializeObject<ValidatorResponseDTO>(content);
                if (validatorResponse == null)
                {
                    return BadRequest(new SignUpResponseDTO()
                    {
                        IsRegistrationSuccessful = false,
                        Errors = new List<string>() { "Email validation failed. Try later..." }
                    });
                }

                if (validatorResponse.Status >= 400)
                {
                    return BadRequest(new SignUpResponseDTO()
                    {
                        IsRegistrationSuccessful = false,
                        Errors = new List<string>() { "Email validation failed. Try later..." }
                    });
                }

                if (validatorResponse.Disposable || !string.IsNullOrEmpty(validatorResponse.DidYouMean))
                {
                    return BadRequest(new SignUpResponseDTO()
                    {
                        IsRegistrationSuccessful = false,
                        Errors = new List<string>() { "Email domain do not exist" }
                    });
                }
            }
            else
            {
                return BadRequest(new SignUpResponseDTO()
                {
                    IsRegistrationSuccessful = false,
                    Errors = new List<string>(){ "Email validation failed. Try later..." }
                });
            }

            var user = new AppUser
            {
                NickName = signUpRequestDTO.NickName,
                UserName = signUpRequestDTO.Email,
                Email = signUpRequestDTO.Email,
                EmailConfirmed = true
            };

            var result = await _userManager.CreateAsync(user, signUpRequestDTO.Password);

            if (!result.Succeeded)
            {
                return BadRequest(new SignUpResponseDTO()
                {
                    IsRegistrationSuccessful = false,
                    Errors = result.Errors.Select(u => u.Description)
                });
            }

            var created_user = await _userManager.FindByEmailAsync(user.Email);

            await _settingsRepository.CreateSettings(new() { UserId = created_user.Id });

            return StatusCode(201);
        }

        [HttpPost]
        [ActionName("signin")]
        public async Task<IActionResult> SignIn([FromBody] SignInRequestDTO signInRequestDTO)
        {
            if (signInRequestDTO == null || !ModelState.IsValid)
            {
                return BadRequest();
            }

            var result = await _signInManager.PasswordSignInAsync(signInRequestDTO.Username, signInRequestDTO.Password, true, false);
            if (result.Succeeded)
            {
                var user = await _userManager.FindByNameAsync(signInRequestDTO.Username);
                if (user == null)
                {
                    return Unauthorized(new SignInResponseDTO
                    {
                        IsAuthSuccessful = false,
                        ErrorMessage = "Use not found"
                    });
                }

                var signingCredentials = GetSigningCredentials();
                var claims = await GetClaims(user);

                var tokenOptions = new JwtSecurityToken(
                    issuer: _aPISettings.ValidIssuer,
                    audience: _aPISettings.ValidAudience,
                    claims: claims,
                    expires: DateTime.Now.AddDays(30),
                    signingCredentials: signingCredentials);

                var token = new JwtSecurityTokenHandler().WriteToken(tokenOptions);

                return Ok(new SignInResponseDTO()
                {
                    IsAuthSuccessful = true,
                    Token = token,
                    //UserDTO = new UserDTO()
                    //{
                    //    Id = user.Id,
                    //    Email = user.Email,
                        
                    //}
                });
            }
            else
            {
                return Unauthorized(new SignInResponseDTO
                {
                    IsAuthSuccessful = false,
                    ErrorMessage = "Authentication failed. Invaild email or password"
                });
            }
        }

        [HttpPost]
        [ActionName("recovery")]
        public async Task<IActionResult> SendRecovery([FromBody] string email)
        {
            ResponseModelDTO response = await _emailService.SendPasswordRecovery(email);

            if (response.IsSuccess)
                return Ok(response);
            else
                return BadRequest(response);
        }

        [HttpPost]
        [ActionName("resetpassword")]
        public async Task<IActionResult> ResetPassword([FromBody] ResetPasswordDTO resetPasswordDTO)
        {
            var user = await _userManager.FindByNameAsync(resetPasswordDTO.Email);
            if (user == null)
            {
                return BadRequest(new ResponseModelDTO()
                {
                    StatusCode = 404,
                    IsSuccess = false,
                    Errors = "User not found"
                });
            }

            var resetResult = await _userManager.ResetPasswordAsync(user, resetPasswordDTO.Token, resetPasswordDTO.Password);
            if (!resetResult.Succeeded)
            {
                ResponseModelDTO responseModelDTO = new() { StatusCode = 500, IsSuccess = false };
                foreach (var e in resetResult.Errors)
                {
                    responseModelDTO.Errors += e.Description + '\n';
                }

                return BadRequest(responseModelDTO);
            }

            return Ok(new ResponseModelDTO()
            {
                StatusCode = 200,
                IsSuccess = true
            });
        }

        private SigningCredentials GetSigningCredentials()
        {
            var secret = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_aPISettings.SecretKey));

            return new SigningCredentials(secret, SecurityAlgorithms.HmacSha256);
        }

        private async Task<List<Claim>> GetClaims(AppUser user)
        {
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, user.NickName),
                new Claim(ClaimTypes.Email, user.Email),
                new Claim("Id", user.Id),
            };

            return claims;
        }
    }
}
