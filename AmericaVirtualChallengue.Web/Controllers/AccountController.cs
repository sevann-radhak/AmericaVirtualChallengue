namespace AmericaVirtualChallengue.Web.Controllers
{
    using System;
    using System.IdentityModel.Tokens.Jwt;
    using System.Linq;
    using System.Security.Claims;
    using System.Text;
    using System.Threading.Tasks;
    using Helpers;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.Extensions.Configuration;
    using Microsoft.IdentityModel.Tokens;
    using Models.Data.Entities;
    using Models.ModelsView;

    public class AccountController : Controller
    {
        private readonly IUserHelper userHelper;
        private readonly IConfiguration configuration;
        private readonly Serilog.ILogger seriLog;

        public AccountController(IUserHelper userHelper, IConfiguration configuration, Serilog.ILogger seriLog)
        {
            this.userHelper = userHelper;
            this.configuration = configuration;
            this.seriLog = seriLog;
        }

        /// <summary>
        /// Login
        /// </summary>
        /// <returns></returns>
        public IActionResult Login()
        {
            if (this.User.Identity.IsAuthenticated)
            {
                return this.RedirectToAction("Index", "Home");
            }

            return this.View();
        }

        /// <summary>
        /// POST: Login
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel model)
        {
            if (this.ModelState.IsValid)
            {
                Microsoft.AspNetCore.Identity.SignInResult result = await this.userHelper.LoginAsync(model);
                if (result.Succeeded)
                {
                    // LOG: DateTime now, DateTime now London, userName, action
                    string logMessage = $"{DateTime.Now} | {DateTime.UtcNow} | {model.Username} | Log in ";
                    seriLog.Warning(logMessage);

                    if (this.Request.Query.Keys.Contains("ReturnUrl"))
                    {
                        return this.Redirect(this.Request.Query["ReturnUrl"].First());
                    }

                    return this.RedirectToAction("Index", "Home");
                }
            }

            this.ModelState.AddModelError(string.Empty, "Failed to login.");
            return this.View(model);
        }

        /// <summary>
        /// Logout
        /// </summary>
        /// <returns></returns>
        public async Task<IActionResult> Logout()
        {
            await this.userHelper.LogoutAsync();

            // LOG: DateTime now, DateTime now London, userName, action
            string logMessage = $"{DateTime.Now} | {DateTime.UtcNow} | {this.User.Identity.Name} | Log out ";
            seriLog.Warning(logMessage);

            return this.RedirectToAction("Index", "Home");
        }

        /// <summary>
        /// Register
        /// </summary>
        /// <returns></returns>
        [Authorize(Roles = "Admin")]
        public IActionResult Register()
        {
            return this.View();
        }

        /// <summary>
        /// POST: Register
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> Register(RegisterNewUserViewModel model)
        {
            if (this.ModelState.IsValid)
            {
                User user = await this.userHelper.FindByEmailAsync(model.Username);
                if (user == null)
                {
                    user = new User
                    {
                        FirstName = model.FirstName,
                        LastName = model.LastName,
                        Email = model.Username,
                        UserName = model.Username
                    };

                    IdentityResult result = await this.userHelper.CreateAsync(user, model.Password);

                    // LOG: DateTime now, DateTime now London, userName, action, newUser
                    string logMessage = $"{DateTime.Now} | {DateTime.UtcNow} | {model.Username} | Register | {user.UserName} ";
                    seriLog.Warning(logMessage);

                    bool isInRole = await this.userHelper.IsUserInRoleAsync(user, "User");
                    if (!isInRole)
                    {
                        await this.userHelper.AddUserToRoleAsync(user, "User");
                    }

                    if (result != IdentityResult.Success)
                    {
                        this.ModelState.AddModelError(string.Empty, "The user couldn't be created.");

                        return this.View(model);
                    }

                    LoginViewModel loginViewModel = new LoginViewModel
                    {
                        Password = model.Password,
                        RememberMe = false,
                        Username = model.Username
                    };

                    Microsoft.AspNetCore.Identity.SignInResult result2 = await this.userHelper.LoginAsync(loginViewModel);

                    if (result2.Succeeded)
                    {
                        // LOG: DateTime now, DateTime now London, userName, action
                        logMessage = $"{DateTime.Now} | {DateTime.UtcNow} | {this.User.Identity.Name} | Log in ";
                        seriLog.Warning(logMessage);

                        return this.RedirectToAction("Index", "Home");
                    }

                    this.ModelState.AddModelError(string.Empty, "The user couldn't be login.");
                    return this.View(model);
                }

                this.ModelState.AddModelError(string.Empty, "The username is already registered.");
            }

            return this.View(model);
        }

        /// <summary>
        /// ChangeUser
        /// </summary>
        /// <returns></returns>
        [Authorize]
        public async Task<IActionResult> ChangeUser()
        {
            User user = await this.userHelper.FindByEmailAsync(this.User.Identity.Name);
            ChangeUserViewModel model = new ChangeUserViewModel();

            if (user != null)
            {
                model.FirstName = user.FirstName;
                model.LastName = user.LastName;
            }

            return this.View(model);
        }

        /// <summary>
        /// POST: ChangeUser
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> ChangeUser(ChangeUserViewModel model)
        {
            if (this.ModelState.IsValid)
            {
                User user = await this.userHelper.FindByEmailAsync(this.User.Identity.Name);

                if (user != null)
                {
                    user.FirstName = model.FirstName;
                    user.LastName = model.LastName;
                    IdentityResult respose = await this.userHelper.UpdateUserAsync(user);

                    if (respose.Succeeded)
                    {
                        this.ViewBag.UserMessage = "User updated!";
                    }
                    else
                    {
                        this.ModelState.AddModelError(string.Empty, respose.Errors.FirstOrDefault().Description);
                    }
                }
                else
                {
                    this.ModelState.AddModelError(string.Empty, "User no found.");
                }
            }

            return this.View(model);
        }

        /// <summary>
        /// ChangePassword
        /// </summary>
        /// <returns></returns>
        [Authorize]
        public IActionResult ChangePassword()
        {
            return this.View();
        }

        /// <summary>
        /// POST: ChangePassword
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> ChangePassword(ChangePasswordViewModel model)
        {
            if (this.ModelState.IsValid)
            {
                User user = await this.userHelper.FindByEmailAsync(this.User.Identity.Name);

                if (user != null)
                {
                    IdentityResult result = await this.userHelper.ChangePasswordAsync(user, model.OldPassword, model.NewPassword);
                    if (result.Succeeded)
                    {
                        this.ViewBag.UserMessage = "User updated!";
                        return this.RedirectToAction("ChangeUser");
                    }
                    else
                    {
                        this.ModelState.AddModelError(string.Empty, result.Errors.FirstOrDefault().Description);
                    }
                }
                else
                {
                    this.ModelState.AddModelError(string.Empty, "User no found.");
                }
            }

            return this.View(model);
        }

        /// <summary>
        /// POST: CreateToken
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> CreateToken([FromBody] LoginViewModel model)
        {
            if (this.ModelState.IsValid)
            {
                User user = await this.userHelper.FindByEmailAsync(model.Username);

                if (user != null)
                {
                    Microsoft.AspNetCore.Identity.SignInResult result = await this.userHelper.ValidatePasswordAsync(
                        user,
                        model.Password);

                    if (result.Succeeded)
                    {
                        Claim[] claims = new[]
                        {
                            new Claim(JwtRegisteredClaimNames.Sub, user.Email),
                            new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
                        };

                        SymmetricSecurityKey key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(this.configuration["Tokens:Key"]));
                        SigningCredentials credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
                        JwtSecurityToken token = new JwtSecurityToken(
                            this.configuration["Tokens:Issuer"],
                            this.configuration["Tokens:Audience"],
                            claims,
                            expires: DateTime.UtcNow.AddDays(15),
                            signingCredentials: credentials);
                        var results = new
                        {
                            token = new JwtSecurityTokenHandler().WriteToken(token),
                            expiration = token.ValidTo
                        };

                        return this.Created(string.Empty, results);
                    }
                }
            }

            return this.BadRequest();
        }

        /// <summary>
        /// Index
        /// </summary>
        /// <returns></returns>
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Index()
        {
            System.Collections.Generic.List<User> users = await this.userHelper.GetAllUsersAsync();
            foreach (User user in users)
            {
                User myUser = await this.userHelper.GetUserByIdAsync(user.Id);
                if (myUser != null)
                {
                    user.IsAdmin = await this.userHelper.IsUserInRoleAsync(myUser, "Admin");
                }
            }

            return this.View(users);
        }

        /// <summary>
        /// AdminOff
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> AdminOff(string id)
        {
            if (string.IsNullOrEmpty(id))
            {
                return NotFound();
            }

            User user = await this.userHelper.GetUserByIdAsync(id);
            if (user == null)
            {
                return NotFound();
            }
            if (user.UserName == "sevann.radhak@gmail.com")
            {
                ModelState.AddModelError("Error", "You can not do this action");
                return this.RedirectToAction(nameof(Index));
            }

            await this.userHelper.RemoveUserFromRoleAsync(user, "Admin");
            return this.RedirectToAction(nameof(Index));
        }

        /// <summary>
        /// AdminOn
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> AdminOn(string id)
        {
            if (string.IsNullOrEmpty(id))
            {
                return NotFound();
            }

            User user = await this.userHelper.GetUserByIdAsync(id);
            if (user == null)
            {
                return NotFound();
            }

            await this.userHelper.AddUserToRoleAsync(user, "Admin");
            return this.RedirectToAction(nameof(Index));
        }

        /// <summary>
        /// DeleteUser
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<IActionResult> DeleteUser(string id)
        {
            if (string.IsNullOrEmpty(id))
            {
                return NotFound();
            }

            User user = await this.userHelper.GetUserByIdAsync(id);
            if (user == null)
            {
                return NotFound();
            }
            if (user.UserName == "sevann.radhak@gmail.com")
            {
                ModelState.AddModelError("Error", "You can not do this action");
                return this.RedirectToAction(nameof(Index));
            }

            try
            {
                await this.userHelper.DeleteUserAsync(user);
            }
            catch (Exception)
            {
                ModelState.AddModelError("Error", "You can not do this action");
                return this.RedirectToAction(nameof(Index));
            }
            return this.RedirectToAction(nameof(Index));
        }

        /// <summary>
        /// NotAuthorized
        /// </summary>
        /// <returns></returns>
        public IActionResult NotAuthorized()
        {
            return this.View();
        }

    }
}