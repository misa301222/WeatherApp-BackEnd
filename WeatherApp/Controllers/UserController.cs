using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using System.IdentityModel.Tokens.Jwt;
using System.Text;
using Microsoft.IdentityModel.Tokens;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;
using Microsoft.EntityFrameworkCore;
using WeatherApp.Data;
using WeatherApp.Models;
using WeatherApp.EntityFrameworkCore;
using WeatherApp.Models.BindingModel;
using WeatherApp.Enums;
using WeatherApp.Models.DTO;

namespace Gamestore.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UserController : Controller
    {
        private readonly ILogger<UserController> _logger;
        private readonly UserManager<AppUser> _userManager;
        private readonly SignInManager<AppUser> _signInManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly JWTConfig _jwTConfig;
        private readonly AppDbContext _context;

        public UserController(ILogger<UserController> logger, UserManager<AppUser> userManager, SignInManager<AppUser> signInManager, IOptions<JWTConfig> jwtConfig, RoleManager<IdentityRole> roleManager, AppDbContext context)
        {
            _logger = logger;
            _userManager = userManager;
            _signInManager = signInManager;
            _jwTConfig = jwtConfig.Value;
            _roleManager = roleManager;
            _context = context;
        }

        [HttpPost]
        [Route("RegisterUser")]
        public async Task<object> RegisterUser([FromBody] AddUpdateRegisterUserBindingModel model)
        {
            try
            {
                if (model.Roles == null)
                {
                    return await Task.FromResult(new ResponseModel(ResponseCode.ERROR, "Roles Are Missing", null));
                }
                foreach (var role in model.Roles)
                {
                    if (!await _roleManager.RoleExistsAsync(role))
                    {
                        return await Task.FromResult(new ResponseModel(ResponseCode.ERROR, "Role doesnt exist", null));
                    }
                }
                var user = new AppUser() { FullName = model.FullName, Email = model.Email, UserName = model.Email, DateCreated = DateTime.Now, DateModified = DateTime.Now, DefaultCity = 0 };
                var result = await _userManager.CreateAsync(user, model.Password);
                if (result.Succeeded)
                {
                    var tempUser = await _userManager.FindByEmailAsync(model.Email);
                    foreach (var role in model.Roles)
                    {
                        await _userManager.AddToRoleAsync(tempUser, role);
                    }

                    return await Task.FromResult(new ResponseModel(ResponseCode.OK, "User has been registered", null));
                }
                return await Task.FromResult(new ResponseModel(ResponseCode.ERROR, "", result.Errors.Select(x => x.Description).ToArray()));
            }
            catch (Exception ex)
            {
                return await Task.FromResult(new ResponseModel(ResponseCode.ERROR, ex.Message, null));
            }
        }


        //[Authorize(Roles = "User, Admin")]
        [HttpGet("GetCurrentUser/{email}")]
        public async Task<object> GetCurrentUser(String email)
        {
            try
            {
                UserDTO userFinal;
                var currentUser = await _userManager.FindByEmailAsync(email);
                var roles = (await _userManager.GetRolesAsync(currentUser)).ToList();

                userFinal = new UserDTO(currentUser.FullName, currentUser.Email, currentUser.UserName, currentUser.DateCreated, roles, currentUser.DefaultCity);

                return await Task.FromResult(new ResponseModel(ResponseCode.OK, "", userFinal));
            }
            catch (Exception ex)
            {
                return await Task.FromResult(new ResponseModel(ResponseCode.ERROR, ex.Message, null));
            }


        }

        [Authorize(Roles = "ADMINISTRATOR")]
        [HttpGet("GetAllUsers")]
        public async Task<object> GetAllUsers()
        {
            try
            {
                List<UserDTO> alluserDTO = new List<UserDTO>();
                //DTO es utilizado para enviar solo unos campos, no todos los de la base de datos
                var users = _userManager.Users.ToList();
                foreach (var user in users)
                {
                    var roles = (await _userManager.GetRolesAsync(user)).ToList();
                    alluserDTO.Add(new UserDTO(user.FullName, user.Email, user.UserName, user.DateCreated, roles, user.DefaultCity));
                }
                //return await Task.FromResult(users);
                return await Task.FromResult(new ResponseModel(ResponseCode.OK, "", alluserDTO));
            }
            catch (Exception ex)
            {
                return await Task.FromResult(new ResponseModel(ResponseCode.ERROR, ex.Message, null));
            }
        }

        [Authorize(Roles = "User, Admin")]
        [HttpGet("GetUserList")]
        public async Task<object> GetUserList()
        {
            try
            {
                List<UserDTO> alluserDTO = new List<UserDTO>();
                //DTO es utilizado para enviar solo unos campos, no todos los de la base de datos
                var users = _userManager.Users.ToList();
                foreach (var user in users)
                {
                    var roles = (await _userManager.GetRolesAsync(user)).ToList();
                    if (roles.Any(x => x == "User"))
                    {
                        alluserDTO.Add(new UserDTO(user.FullName, user.Email, user.UserName, user.DateCreated, roles, user.DefaultCity));
                    }

                }
                //return await Task.FromResult(users);
                return await Task.FromResult(new ResponseModel(ResponseCode.OK, "", alluserDTO));
            }
            catch (Exception ex)
            {
                return await Task.FromResult(new ResponseModel(ResponseCode.ERROR, ex.Message, null));
            }
        }


        [HttpGet("GetRoles")]
        public async Task<object> GetRoles()
        {
            try
            {
                var roles = _roleManager.Roles.Select(x => x.Name).ToList();

                return await Task.FromResult(new ResponseModel(ResponseCode.OK, "", roles));
            }
            catch (Exception ex)
            {
                return await Task.FromResult(new ResponseModel(ResponseCode.ERROR, ex.Message, null));
            }
        }

        [HttpPost("Login")]
        public async Task<object> Login([FromBody] LoginBindingModel model)
        {
            try
            {

                if (ModelState.IsValid)
                {
                    var result = await _signInManager.PasswordSignInAsync(model.Email, model.Password, false, false);
                    if (result.Succeeded)
                    {
                        var appUser = await _userManager.FindByEmailAsync(model.Email);
                        var roles = (await _userManager.GetRolesAsync(appUser)).ToList();
                        var user = new UserDTO(appUser.FullName, appUser.Email, appUser.UserName, appUser.DateCreated, roles, appUser.DefaultCity);
                        //return await Task.FromResult("Login successfully");
                        user.Token = GenerateToken(appUser, roles);
                        return await Task.FromResult(new ResponseModel(ResponseCode.OK, "", user));
                    }
                }
                return await Task.FromResult(new ResponseModel(ResponseCode.ERROR, "Invalid username or password", null));
            }
            catch (Exception ex)
            {
                return await Task.FromResult(new ResponseModel(ResponseCode.ERROR, ex.Message, null));
            }
        }

        //[Authorize (Roles = "Admin")]
        [HttpPost("AddRole")]
        public async Task<Object> AddRole([FromBody] AddRoleBindingModel model)
        {
            try
            {
                if (model == null || model.Role == "")
                {
                    return await Task.FromResult(new ResponseModel(ResponseCode.ERROR, "Parameters are missing", null));
                }
                if (await _roleManager.RoleExistsAsync(model.Role))
                {
                    return await Task.FromResult(new ResponseModel(ResponseCode.OK, "Role already exist", null));
                }
                var role = new IdentityRole();
                role.Name = model.Role;
                var result = await _roleManager.CreateAsync(role);
                if (result.Succeeded)
                {
                    return await Task.FromResult(new ResponseModel(ResponseCode.OK, "Role added succesfully", null));
                }
                return await Task.FromResult(new ResponseModel(ResponseCode.ERROR, "Something went wrong, try again", null));
            }
            catch (Exception ex)
            {
                return await Task.FromResult(new ResponseModel(ResponseCode.ERROR, ex.Message, null));
            }
        }

        private string GenerateToken(AppUser user, List<string> roles)
        {
            var claims = new List<System.Security.Claims.Claim>()
            {
                new System.Security.Claims.Claim(JwtRegisteredClaimNames.NameId, user.Id),
                    new System.Security.Claims.Claim(JwtRegisteredClaimNames.Email, user.Email),
                    new System.Security.Claims.Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
            };

            foreach (var role in roles)
            {
                claims.Add(new System.Security.Claims.Claim(ClaimTypes.Role, role));
            }

            var jwtTokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_jwTConfig.Key);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new System.Security.Claims.ClaimsIdentity(claims),
                Expires = DateTime.Now.AddHours(12),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };
            var token = jwtTokenHandler.CreateToken(tokenDescriptor);
            return jwtTokenHandler.WriteToken(token);
        }

        [HttpPost]
        [Route("UpdateDefaultCity")]
        public async Task<Object> UpdateDefaultCity([FromBody] UpdateDefaultCityUserBindingModel model)
        {
            try
            {
                var currentUser = await _userManager.FindByEmailAsync(model.Email);
                currentUser.DefaultCity = model.CityId;
                var result = await _userManager.UpdateAsync(currentUser);
                if (result.Succeeded)
                {
                    return await Task.FromResult(new ResponseModel(ResponseCode.OK, "City Updated Succesfully", null));
                }

                return await Task.FromResult(new ResponseModel(ResponseCode.ERROR, "Something went wrong", null));
            }
            catch (Exception ex)
            {
                return await Task.FromResult(new ResponseModel(ResponseCode.ERROR, ex.Message, null));
            }

        }



        private bool AppUsersExists(string email)
        {
            return _context.AppUsers.Any(e => e.Email == email);
        }
    }


}
