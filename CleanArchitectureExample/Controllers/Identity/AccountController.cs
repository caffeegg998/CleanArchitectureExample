
using CleanArchitectureExample.Application.DTOs;
using CleanArchitectureExample.Application.DTOs.Account;
using MediatR;
using Microsoft.AspNetCore.Authentication.BearerToken;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.Data;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.Mvc;


using System.Reflection;
using System.Security.Claims;
using System.Text;
using System.Text.Encodings.Web;
using static CleanArchitectureExample.Application.Features.Commands.UserCommands;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace SparePartStockAPI.Controllers.Identity
{
    [ApiController]
    [Route("api/[controller]")]
    public class AccountController : ControllerBase
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly IEmailSender _emailSender;
        //private readonly IUserProfileService _userProfileService;
        //private readonly IRazorLightEngine _razorLightEngine;
        private readonly IWebHostEnvironment _env;
        private readonly IMediator _mediator;

        public AccountController(UserManager<IdentityUser> userManager, IEmailSender emailSender,IWebHostEnvironment env,IMediator mediator)
        {
            _userManager = userManager;
            _emailSender = emailSender;
            //_userProfileService = userProfileService;
            //_razorLightEngine  = new RazorLightEngineBuilder().UseEmbeddedResourcesProject(Assembly.GetEntryAssembly()).Build();
            _env = env;
            _mediator = mediator;
        }

        [HttpPost("login")]
        public async Task<Results<Ok<AccessTokenResponse>, EmptyHttpResult, ProblemHttpResult>> Login([FromBody] LoginRequest login, [FromQuery] bool? useCookies, [FromQuery] bool? useSessionCookies, [FromServices] IServiceProvider sp)
        {
            var signInManager = sp.GetRequiredService<SignInManager<IdentityUser>>();
            var useCookieScheme = useCookies == true || useSessionCookies == true;
            var isPersistent = useCookies == true && useSessionCookies != true;
            signInManager.AuthenticationScheme = useCookieScheme ? IdentityConstants.ApplicationScheme : IdentityConstants.BearerScheme;

            var result = await signInManager.PasswordSignInAsync(login.Email, login.Password, isPersistent, lockoutOnFailure: true);

            if (result.RequiresTwoFactor)
            {
                if (!string.IsNullOrEmpty(login.TwoFactorCode))
                {
                    result = await signInManager.TwoFactorAuthenticatorSignInAsync(login.TwoFactorCode, isPersistent, rememberClient: isPersistent);
                }
                else if (!string.IsNullOrEmpty(login.TwoFactorRecoveryCode))
                {
                    result = await signInManager.TwoFactorRecoveryCodeSignInAsync(login.TwoFactorRecoveryCode);
                }
            }

            if (!result.Succeeded)
            {
                return TypedResults.Problem(result.ToString(), statusCode: StatusCodes.Status401Unauthorized);
            }



            // The signInManager already produced the needed response in the form of a cookie or bearer token.
            return TypedResults.Empty;
        }

        [HttpPost("register")]
        public async Task<IResult> Register([FromBody] RegisterDTO model)
        {
            if (!ModelState.IsValid)
            {
                return TypedResults.BadRequest(ModelState); 
                //BadRequest(ModelState);
            }
            string role = "";
            var user = new IdentityUser { UserName = model.username, Email = model.Email };

            var result = await _userManager.CreateAsync(user, model.Password);

            if(result.Succeeded)
            {
                

                switch (model.detectUser)
                {
                    case 1:
                        role = "TL";
                        break;
                    case 2:
                        role = "QV";
                        break;
                    case 3:
                        role = "TS";
                        break ;
                    default:
                        role = "TL";
                        break;
                }

                var result_role = await _userManager.AddToRoleAsync(user, role);
                if (!result_role.Succeeded)
                    BadRequest(result_role);
            }

            

            if (result.Succeeded)
            {
                UserProfileDTO userProfile = new UserProfileDTO
                {
                    UserId = Guid.Parse(user.Id),
                    FullName = model.FullName,
                    DateOfBirth = model.DateOfBirth,
                    Department = model.Department,
                    Factory = role

                };

                CreateUserCommand createUserCommand = new CreateUserCommand();
                createUserCommand.userProfileDTO = userProfile;

                var productId = await _mediator.Send(createUserCommand);
                


                var token = await _userManager.GenerateEmailConfirmationTokenAsync(user);
                var confirmationLink = Url.Action("ConfirmEmail", "Account", new { userId = user.Id, token }, Request.Scheme);

                string templatePath = Path.Combine(_env.ContentRootPath, "Email Template", "Active.cshtml");

                //using (var sr = new StreamReader(templatePath, Encoding.UTF8)) // what is the encoding of the text? 
                //{
                //    var mailTemplate = sr.ReadToEnd(); // read all text into memory
                //                                  // TODO: Find most frequent word in allText
                //                                  // replace the word allText.Replace(oldValue, newValue, stringComparison)
                //    string mailBody = await _razorLightEngine.CompileRenderStringAsync(
                //        "cacheKey",
                //        mailTemplate,
                //        new
                //        {
                //            Link = HtmlEncoder.Default.Encode(confirmationLink)
                //        });

                //    await _emailSender.SendEmailAsync(user.Email, "Xác nhận Email", mailBody);
                //}
                

                //_razorLightEngine.CompileRenderStringAsync()

                await _emailSender.SendEmailAsync(user.Email, "Xác nhận Email", $"Vui lòng xác nhận email của bạn bằng cách nhấn vào <a href='{HtmlEncoder.Default.Encode(confirmationLink)}'>đây</a>.");

                //return Ok("Đăng ký thành công, vui lòng kiểm tra email để xác nhận địa chỉ email của bạn");
                return TypedResults.Ok(new { msg = "Đăng ký thành công, vui lòng kiểm tra email để xác nhận địa chỉ email của bạn" ,statusCode = StatusCodes.Status200OK});

            }
            return TypedResults.BadRequest(result.Errors);
            //return BadRequest(result.Errors);
        }
        [HttpGet("confirmEmail")]
        public async Task<IActionResult> ConfirmEmail(string userId, string token)
        {
            if (userId == null || token == null)
            {
                return BadRequest("Thiếu thông tin xác nhận.");
            }
            var user = await _userManager.FindByIdAsync(userId);
            if (user == null)
            {
                return BadRequest($"Không tìm thấy người dùng với ID: {userId}");
            }

            var result = await _userManager.ConfirmEmailAsync(user, token);
            if (result.Succeeded)
            {
                return Ok("Xác nhận Email thành công!");
            }
            else
            {
                return BadRequest("Xác nhận Email không thành công");
            }
        }

        [HttpGet("info")]
        [Authorize]
        public async Task<IActionResult> GetUserInfo()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var user = await _userManager.FindByIdAsync(userId);
            //var userProfile = _userProfileService.GetUserProfile(userId);
            var roles = await _userManager.GetRolesAsync(user);

            if (user == null)
            {
                return NotFound("Không tìm thấy thông tin người dùng");
            }
            return Ok(new
            {
                UserName = user.UserName,
                Email = user.Email,
                //UserProfile = userProfile,
                Roles = roles
            });
        }

        private static async Task<InfoResponse> CreateInfoResponseAsync<TUser>(TUser user, UserManager<TUser> userManager)
        where TUser : class
        {
            return new()
            {
                Email = await userManager.GetEmailAsync(user) ?? throw new NotSupportedException("Users must have an email."),
                IsEmailConfirmed = await userManager.IsEmailConfirmedAsync(user),
            };
        }

        //[HttpPost("forgot-password")]
        //public async Task<IActionResult> ForgotPassword([FromBody] ForgotPasswordRequest request)
        //{
        //    if (!ModelState.IsValid)
        //    {
        //        return BadRequest(ModelState);

        //    }

        //    var user = await _userManager.FindByEmailAsync(request.Email);

        //    if (user == null || !await _userManager.IsEmailConfirmedAsync(user))
        //    {
        //        return Ok($"Yêu cầu đã được gửi nếu địa chỉ email tồn tại và đã xác nhận");


        //    }
        //    var token = await _userManager.GeneratePasswordResetTokenAsync(user);
        //    var resetLink = Url.Action("ResetPassword", "Account", new { email = request.Email, token }, Request.Scheme);

        //    await _emailSender.SendEmailAsync(request.Email, "Yêu cầu đặt lại mật khẩu", $"Vui lòng đặt lại mật khẩu của bạn bằng cách sử dụng đoạn code bên dưới: </br> '{token}");

        //    return Ok("Yêu cầu đã được gửi. Vui lòng kiểm tra email để đặt lại mật khẩu");
        //}
        private static readonly Dictionary<string, (int Count, DateTime LastReset)> _requestTracker = new();
        private static readonly object _lock = new();
        private const int MaxRequests = 2;
        private const int ResetIntervalMinutes = 1;

        [HttpPost("forgot-password")]
        public async Task<ActionResult<ResponseDTO<string>>> ForgotPassword([FromBody] ForgotPasswordRequest request)
        {
            string key = $"{request.Email}_{HttpContext.Connection.RemoteIpAddress}";

            if (!IsRequestAllowed(key))
            {
                return StatusCode(400, ResponseDTO<string>.CreateError("Unable to send password reset email. Please try again later.",
                    new List<string> { "Too many request" }));
            }

            if (!ModelState.IsValid)
            {
                var errors = ModelState.Values
                    .SelectMany(v => v.Errors)
                    .Select(e => e.ErrorMessage)
                    .ToList();
                return BadRequest(ResponseDTO<string>.CreateError("Invalid model state", errors));
            }

            var user = await _userManager.FindByEmailAsync(request.Email);

            if (user == null)
            {
                // For security reasons, we don't want to reveal whether the user exists or not
                return StatusCode(400, ResponseDTO<string>.CreateError("Unable to send password reset email. Please try again later.",
                    new List<string> { "Email is not register" }));
            }else if(!await _userManager.IsEmailConfirmedAsync(user))
            {
                return StatusCode(400, ResponseDTO<string>.CreateError("Unable to send password reset email. Please try again later.",
                    new List<string> { "Unverified account" }));
            }


            var token = await _userManager.GeneratePasswordResetTokenAsync(user);
            //var resetLink = Url.Action("ResetPassword", "Account", new { email = request.Email, resetCode = token }, Request.Scheme);
            var baseUrl = "http://192.168.186.22:4200";
            var resetLink = $"{baseUrl}/reset-password?email={Uri.EscapeDataString(request.Email)}&resetCode={Uri.EscapeDataString(token)}";

            try
            {
                await _emailSender.SendEmailAsync(request.Email, "Yêu cầu đặt lại mật khẩu",
                    $"Vui lòng đặt lại mật khẩu của bạn bằng cách sử dụng đoạn code bên dưới: </br> <a href='{resetLink}'>Đặt lại mật khẩu</a>");

                return Ok(ResponseDTO<string>.CreateSuccess("Yêu cầu đã được gửi. Vui lòng kiểm tra email để đặt lại mật khẩu"));
            }
            catch (Exception ex)
            {
                // Log the exception here
                return StatusCode(500, ResponseDTO<string>.CreateError("An error occurred while sending the email",
                    new List<string> { "Unable to send password reset email. Please try again later." }));
            }
        }

        private bool IsRequestAllowed(string key)
        {
            lock (_lock)
            {
                if (!_requestTracker.ContainsKey(key))
                {
                    _requestTracker[key] = (1, DateTime.UtcNow);
                    return true;
                }

                var (count, lastReset) = _requestTracker[key];
                if ((DateTime.UtcNow - lastReset).TotalMinutes >= ResetIntervalMinutes)
                {
                    _requestTracker[key] = (1, DateTime.UtcNow);
                    return true;
                }

                if (count >= MaxRequests)
                {
                    return false;
                }

                _requestTracker[key] = (count + 1, lastReset);
                return true;
            }
        }

        private IActionResult TooManyRequests(ResponseDTO<string> response)
        {
            HttpContext.Response.Headers["Retry-After"] = ResetIntervalMinutes.ToString();
            return StatusCode(StatusCodes.Status429TooManyRequests, response);
        }

        [HttpPost("reset-password")]
        public async Task<ActionResult<ResponseDTO<string>>> ResetPassword([FromBody] ResetPasswordRequest request)
        {
            if (!ModelState.IsValid)
            {
                var errors = ModelState.Values
                    .SelectMany(v => v.Errors)
                    .Select(e => e.ErrorMessage)
                    .ToList();
                return BadRequest(ResponseDTO<string>.CreateError("Invalid model state", errors));
            }

            var user = await _userManager.FindByEmailAsync(request.Email);
            if (user == null)
            {
                return Ok("Yêu cầu đã được gửi nếu địa chỉ email tồn tại.");
            }
            var result = await _userManager.ResetPasswordAsync(user, request.ResetCode, request.NewPassword);


            if (result.Succeeded)
            {
                return Ok(ResponseDTO<string>.CreateSuccess("Đặt lại mật khẩu thành công"));
            }
            else
            {
                var errors = result.Errors.Select(e => $"{e.Code}: {e.Description}").ToList();
                return BadRequest(ResponseDTO<string>.CreateError("Failed to reset password", errors));
            }


        }

        [HttpPost("/resendConfirmationEmail")]
        public async Task<IActionResult> ResendConfirmationEmail([FromBody] ResendConfirmationEmailRequest resendRequest,[FromServices] IServiceProvider sp)
        {
            var userManager = sp.GetRequiredService<UserManager<IdentityUser>>();
            IdentityUser? identityUser = await userManager.FindByEmailAsync(resendRequest.Email);

            if (identityUser.EmailConfirmed == true)
            {
                return Ok("Email đã được active!");
            }



            var token = await _userManager.GenerateEmailConfirmationTokenAsync(identityUser);
            var confirmationLink = Url.Action("ConfirmEmail", "Account", new { userId = identityUser.Id, token }, Request.Scheme);

            await _emailSender.SendEmailAsync(resendRequest.Email, "Yêu cầu xác nhận lại Email", $"Vui lòng xác nhận email của bạn bằng cách nhấn vào <a href='{HtmlEncoder.Default.Encode(confirmationLink)}'>đây</a>.");

            return Ok("Gửi yêu cầu xác thực email thành công, vui lòng kiểm tra email để xác nhận địa chỉ email của bạn");
            
        }

        [HttpGet("getAll_MasterPart")]
        public ActionResult<TotalUserProfiles> GetAllUserProfile()
        {
            try
            {
                //var userProfileDTOs = _userProfileService.GetAllUserProfile();
                //return Ok(userProfileDTOs);
                return null;
            }
            catch (Exception ex)
            {
                // Xử lý lỗi nếu cần
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        [HttpGet("check-exist-username")]
        public async Task<IActionResult> checkUsername(string username)
        {
            try
            {
                var userProfileDTOs = await _userManager.FindByNameAsync(username);
                if(userProfileDTOs != null) {
                    return StatusCode(403, $"Username đã tồn tại!");
                }
                return StatusCode(200, $"Bạn có thể sử dụng username này.");

            }
            catch (Exception ex)
            {
                // Xử lý lỗi nếu cần
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        [HttpGet("check-exist-email")]
        public async Task<IActionResult> checkEmail(string email)
        {
            try
            {
                var userProfileDTOs = await _userManager.FindByEmailAsync(email);
                if (userProfileDTOs != null)
                {
                    return StatusCode(403, $"Email này đã được đăng ký ở tài khoản khác!");
                }
                return StatusCode(200, $"Bạn có thể sử dụng email này.");

            }
            catch (Exception ex)
            {
                // Xử lý lỗi nếu cần
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }




    }
}

