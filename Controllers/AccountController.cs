using Day_1.DTO;
using HelperPlan.Models;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using HelperPlan.DTO.Account;
using Admin = HelperPlan.DTO.Account.Admin;
using Microsoft.VisualBasic;
using HelperPlan.Repository;
using System.Data;

namespace HelperPlan.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly UserManager<ApplicationUser> UserManager;
        private readonly RoleManager<ApplicationRole> RoleManager;
        private readonly IUnitOFWork unitOFWork;
        private readonly IConfiguration configuration;

        public AccountController(UserManager<ApplicationUser> UserManager, RoleManager<ApplicationRole> RoleManager, IUnitOFWork unitOFWork, IConfiguration configuration)
        {
            this.UserManager = UserManager;
            this.RoleManager = RoleManager;
            this.unitOFWork = unitOFWork;
            this.configuration = configuration;
        }
        [HttpPost("Register")]
        public async Task<IActionResult> Register(UserRegister userRegister, string role)
        {
            if (ModelState.IsValid)
            {
                if (!await RoleManager.RoleExistsAsync(role))
                {
                    ModelState.AddModelError("Role", "Role does not exist.");
                    return BadRequest(ModelState);
                }

                ApplicationUser user =  role switch
                {
                    "Employer" => new Employer
                    {
                        UserName = userRegister.UserName,
                        Fname = userRegister.FirstName,
                        Lname = userRegister.LastName,
                        Email = userRegister.Email,
                    },
                    "Candidate" => new Candidate
                    {
                        UserName = userRegister.UserName,
                        Fname = userRegister.FirstName,
                        Lname = userRegister.LastName,
                        Email = userRegister.Email,
                    },
                    _ => new ApplicationUser
                    {
                        UserName = userRegister.UserName,
                        Fname = userRegister.FirstName,
                        Lname = userRegister.LastName,
                        Email = userRegister.Email,
                    }
                };

                IdentityResult result = await UserManager.CreateAsync(user, userRegister.Password);

                if (result.Succeeded)
                {
                    await UserManager.AddToRoleAsync(user, role);

                    // Detach user from the context if necessary
                    // dbContext.Entry(user).State = EntityState.Detached;

                    return Ok(new { Msg = "Account Created" });
                }

                return BadRequest(new { Msg = result.Errors });
            }

            return BadRequest(ModelState);
        }
        [HttpPost("LoginUsingFaceBook")]
        public async Task<IActionResult> LoginUsingFaceBook(FaceBookUser FaceBookUser)
        {
            AspNetFaceBookUsers Found = this.unitOFWork.AspNetFaceBookUsersRepository.Get(F => F.FaceBookUserId == FaceBookUser.Id);
            ApplicationUser User = null;
            if (Found != null)
            {
                User = await UserManager.FindByIdAsync(Found.OriginalApplicationUserId.ToString());
            }
            else
            {
                return NotFound("Not Found");
            }
            JwtSecurityToken MyToken = await GenerateAndReturnToken(User);
            return Ok(new
            {
                Token = new JwtSecurityTokenHandler().WriteToken(MyToken),
                Expired = MyToken.ValidTo
            });
        }
        [HttpPost("RegisterUsingFaceBook")]
        public async Task<IActionResult> RegisterUsingFaceBook(FaceBookUser FaceBookUser, string Role)
        {
                ApplicationUser User = Role switch
                {
                    "Employer" => new Employer
                    {
                        UserName = FaceBookUser.first_name+FaceBookUser.last_name,
                        Fname = FaceBookUser.first_name,
                        Lname = FaceBookUser.last_name,
                        Email = FaceBookUser.email,
                    },
                    "Candidate" => new Candidate
                    {
                        UserName = FaceBookUser.first_name + FaceBookUser.last_name,
                        Fname = FaceBookUser.first_name,
                        Lname = FaceBookUser.last_name,
                        Email = FaceBookUser.email,
                    },
                    _ => new ApplicationUser
                    {
                        UserName = FaceBookUser.first_name + FaceBookUser.last_name+FaceBookUser.Id.ToString(),
                        Fname = FaceBookUser.first_name,
                        Lname = FaceBookUser.last_name,
                        Email = FaceBookUser.email,
                    }
                };
                IdentityResult result = await UserManager.CreateAsync(User, FaceBookUser.first_name.ToUpper() + FaceBookUser.last_name.ToLower() + FaceBookUser.email + FaceBookUser.Id);
                if (result.Succeeded)
                {
                    await UserManager.AddToRoleAsync(User, Role);
                    this.unitOFWork.AspNetFaceBookUsersRepository.add(new AspNetFaceBookUsers { FaceBookUserId = FaceBookUser.Id, OriginalApplicationUserId = User.Id });
                    this.unitOFWork.AspNetFaceBookUsersRepository.save();
                }
                else
                {
                    return BadRequest(new { Msg = "Failed To create Account" });
                }
            JwtSecurityToken MyToken = await GenerateAndReturnToken(User);
            return Ok(new
            {
                Token = new JwtSecurityTokenHandler().WriteToken(MyToken),
                Expired = MyToken.ValidTo
            });
        }

        [HttpPost("Login")]
        public async Task<IActionResult> Login(UserLogin UserLogin)
        {
            if (ModelState.IsValid)
            {
                ApplicationUser? User = await UserManager.FindByEmailAsync(UserLogin.Email);
                if (User == null)
                {
                    return Unauthorized(new { Msg = "Invalid Account" });
                }
                else
                {
                    bool Found = await UserManager.CheckPasswordAsync(User, UserLogin.Password);
                    if (Found)
                    {
                        JwtSecurityToken MyToken = await GenerateAndReturnToken(User);
                        return Ok(new
                        {
                            Token = new JwtSecurityTokenHandler().WriteToken(MyToken),
                            Expired = MyToken.ValidTo
                        });
                    }
                }
            }
            return BadRequest(new { Msg = "Invalid Account" });
        }
        [HttpGet("GenerateAndReturnToken")]
        private async Task<JwtSecurityToken> GenerateAndReturnToken(ApplicationUser User)
        {
            List<Claim> MyClaims = new List<Claim>();
            MyClaims.Add(new Claim(ClaimTypes.Name, User.UserName));
            MyClaims.Add(new Claim(ClaimTypes.NameIdentifier, User.Id.ToString()));
            MyClaims.Add(new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()));
            var Roles = await UserManager.GetRolesAsync(User);
            foreach (var Role in Roles)
            {
                MyClaims.Add(new Claim(ClaimTypes.Role, Role));
            }
            var SecretKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["JWT:SecretKey"]));
            SigningCredentials SigningCredentials = new SigningCredentials(SecretKey, SecurityAlgorithms.HmacSha256);
            JwtSecurityToken MyToken = new JwtSecurityToken(
                    issuer: configuration["JWT:ValidIssuer"],//Provider
                    audience: configuration["JWT:ValidAudience"],//consumer url
                    expires: DateTime.Now.AddHours(1),
                    signingCredentials: SigningCredentials,
                    claims: MyClaims
                );
            return MyToken;
        }
        [Authorize]
        [HttpPost("Logout")]
        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return Ok(new {Msg = "Logged out successfully" });
        }

        [HttpPatch("UpdateUser")]
        public async Task<IActionResult> UpdateUser(int UserId, [FromBody] DTO.Account.Changes changes)
        {
            ApplicationUser? User = await UserManager.FindByIdAsync(UserId.ToString());
            if (User == null)
            {
                return Unauthorized(new {Msg = "Invalid Account" });
            }
            else
            {
                bool Found = await UserManager.CheckPasswordAsync(User, changes.Password);
                if (Found)
                {
                    User.Email = changes.Email;
                    User.Location = changes.Location;
                    User.PhoneNumber = changes.MoileNumber;
                    User.Fname = changes.Name;
                    List<string> roles = (List<string>)await UserManager.GetRolesAsync(User);
                    foreach (var role in roles)
                    {
                        await UserManager.RemoveFromRoleAsync(User, role);
                    }
                    await UserManager.AddToRoleAsync(User, changes.Role);
                    IdentityResult result = await UserManager.UpdateAsync(User);
                    if (result.Succeeded)
                    {
                        return await Login(new UserLogin { Email = User.Email, Password = changes.Password });
                    }
                    else
                    {
                        return BadRequest(new {Msg = "Update Failed" });
                    }
                }
                else
                {
                    return BadRequest(new {Msg = "Invalid Password" });
                }
            }
        }

        [HttpPatch("UpdateUserPassword")]
        public async Task<IActionResult> UpdateUserPassword(int UserId, PasswordChanges passwordChanges)
        {
            ApplicationUser? User = await UserManager.FindByIdAsync(UserId.ToString());
            if (User == null)
            {
                return Unauthorized(new {Msg = "Invalid Account" });
            }
            else
            {
                bool Found = await UserManager.CheckPasswordAsync(User, passwordChanges.OldPassword);
                if (Found)
                {
                    IdentityResult result = await UserManager.ChangePasswordAsync(User, passwordChanges.OldPassword, passwordChanges.NewPassword);
                    if (result.Succeeded)
                    {
                        return Ok(new {Msg = "Password Changed" });
                    }
                    else
                    {
                        return BadRequest(new {Msg = "Failed To Update Password" });
                    }
                }
                else
                {
                    return BadRequest(new {Msg = "Invalid Password" });
                }
            }
        }

        [HttpGet("GetUserDetails")]
        public async Task<IActionResult> GetUserDetails(int UserId)
        {
            ApplicationUser? User = await UserManager.FindByIdAsync(UserId.ToString());
            if (User == null)
            {
                return Unauthorized(new {Msg = "Invalid Account" });
            }
            else
            {
                return Ok(new { Email = User.Email, MobileNumber = User.PhoneNumber, Location = User.Location });
            }
        }


        #region AdminDashboard
        // Endpoint to get all admins
        [HttpGet("GetAdmins")]
        public async Task<IActionResult> GetAdmins()
        {
            var admins = (await UserManager.GetUsersInRoleAsync("Admin")).Select(user => new
            {
                user.Id,
                user.UserName,
                user.Email
            });

            return Ok(admins);
        }

        // Endpoint to update an admin
        [HttpPut("UpdateAdmin/{id}")]
        public async Task<IActionResult> UpdateAdmin(string id, [FromBody] Admin model)
        {
            var user = await UserManager.FindByIdAsync(id);
            if (user == null)
            {
                return NotFound(new {Msg = "Admin not found." });
            }

            user.UserName = model.UserName;
            user.Email = model.Email;

            if (!string.IsNullOrEmpty(model.Password))
            {
                var token = await UserManager.GeneratePasswordResetTokenAsync(user);
                var passwordChangeResult = await UserManager.ResetPasswordAsync(user, token, model.Password);
                if (!passwordChangeResult.Succeeded)
                {
                    return BadRequest(passwordChangeResult.Errors);
                }
            }

            var result = await UserManager.UpdateAsync(user);
            if (result.Succeeded)
            {
                return Ok(user);
            }

            return BadRequest(result.Errors);
        }

        // Endpoint to delete an admin
        [HttpDelete("DeleteAdmin/{id}")]
        public async Task<IActionResult> DeleteAdmin(string id)
        {
            var user = await UserManager.FindByIdAsync(id);
            if (user == null)
            {
                return NotFound(new {Msg = "Admin not found." });
            }

            var result = await UserManager.DeleteAsync(user);
            if (result.Succeeded)
            {
                return Ok(user);
            }

            return BadRequest(result.Errors);
        }

        // Endpoint to add a new admin
        [HttpPost("AddAdmin")]
        public async Task<IActionResult> AddAdmin([FromBody] Admin model)
        {
            // Creating a new ApplicationUser object
            var user = new ApplicationUser
            {
                UserName = model.UserName,
                Email = model.Email,
                PasswordHash = model.Password,
            };

            // Creating the new admin with a password
            var result = await UserManager.CreateAsync(user, model.Password);
            if (result.Succeeded)
            {
                // Assigning the "Admin" role to the new user
                var roleResult = await UserManager.AddToRoleAsync(user, "Admin");
                if (roleResult.Succeeded)
                {
                    return Ok(user);
                }
                return BadRequest(roleResult.Errors);
            }

            return BadRequest(result.Errors);
        }

        [HttpGet("GetAdmin/{id}")]
        public async Task<IActionResult> GetAdmin(string id)
        {
            var user = await UserManager.FindByIdAsync(id);
            if (user == null)
            {
                return NotFound(new {Msg = "Admin not found." });
            }

            var admin = new
            {
                user.Id,
                user.UserName,
                user.Email
            };

            return Ok(admin);
        }
        #endregion

        #region Helper Methods
        [HttpPost("AddRole")]
        public async Task<IActionResult> SeedRoles(RoleManager<ApplicationRole> roleManager, string RoleName)
        {
            if (!await roleManager.RoleExistsAsync(RoleName))
            {
                ApplicationRole role = new ApplicationRole()
                {
                    Name = RoleName
                };

                IdentityResult roleResult = await roleManager.CreateAsync(role);

                if (roleResult.Succeeded)
                {
                    return Ok(new {Msg = "role created successfully" });
                }
                else
                {
                    // Handle role creation failure
                    return BadRequest(new {Msg = "Failed to create role" });
                }
            }

            // Role already exists
            return BadRequest(new {Msg = "role already exists" });
        }

        #endregion




    }
}
