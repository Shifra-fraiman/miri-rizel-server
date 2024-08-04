using Microsoft.AspNetCore.Mvc;
using CopyRight.Dto.Models;
using CopyRight.Bl.Interfaces;
using CopyRight.Bl.Service;
using System.Security.Claims;
using Microsoft.IdentityModel.Tokens;
using Microsoft.AspNetCore.Authorization;
using static CopyRight.Bl.Service.UserService;

namespace CopyRight.WebApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UserController : ControllerBase
    {
        private readonly IUser _userService;
        private readonly GoogleDriveService googleDriveService;
        public UserController(IUser userService, GoogleDriveService googleDriveService)
        {
            _userService = userService;
            this.googleDriveService = googleDriveService;
        }
        [HttpGet("Login")]
        public async Task<ActionResult<User>> Login([FromQuery(Name = "email")] string email, [FromQuery(Name = "password")] string password)
        {
            try
            {

                if (email == null || password == null)
                {
                    return BadRequest("Invalid email or password!"); // Return bad request if email or password is missing
                }

                User userFound = await _userService.LogInAsync(email, password);

                if (userFound == null)
                {
                    return NotFound("User not found"); // Return not found if user with the provided email is not found
                }

                if (!BCrypt.Net.BCrypt.Verify(password, userFound.Password))
                {
                    return BadRequest("Incorrect password"); // Return bad request if the password is incorrect
                }
                // System.Console.WriteLine($"User: {userFound.FirstName} Login: {userFound.Password} Admin: {userFound.Role}");
                var claims = new List<Claim>();
                claims = new List<Claim>
                {
                    new Claim("Type", "Customer"),
                        new Claim("id" , userFound.UserId.ToString()),
                    };
                if (userFound.Role.Id != 1)
                {
                    claims.Add(new Claim("Type", "Worker"));
                    new Claim("id", userFound.UserId.ToString());
                }
                if (userFound.Role.Id == 3)
                {
                    System.Console.WriteLine("Iam Admin");
                    claims.Add(new Claim("Type", "Admin"));
                    new Claim("id", userFound.UserId.ToString());
                }
                var token = TokenService.GetToken(claims);
                return Ok(new { token = TokenService.WriteToken(token), user = userFound });
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        [HttpGet("LoginGoogle")]
        public async Task<ActionResult<User>> LoginGoogle([FromQuery(Name = "email")] string email, [FromQuery(Name = "name")] string name)
        {
            try
            {
                if (email == null || name == null)
                    return BadRequest("Invalid firstName/lastName/email/password!");
                User userFound = await _userService.LogInGoogleAsync(email, name);
                if (userFound == null)
                {
                    return NotFound("User not found");
                }
                var claims = new List<Claim>();
                claims = new List<Claim>
                    {
                        new Claim("Type" , "Customer"),
                    new Claim("id", userFound.UserId.ToString())
                };
                if (userFound.Role.Id != 1)
                {
                    claims.Add(new Claim("Type", "Worker"));
                }
                if (userFound.Role.Id == 3)
                {
                    claims.Add(new Claim("Type", "Admin"));
                }
                var token = TokenService.GetToken(claims);
                return Ok(new { token = TokenService.WriteToken(token), user = userFound });
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        [HttpPost]
        public async Task<ActionResult<User>> CreateAsync([FromBody] User user)
        {
            try
            {
                User createdUser = await _userService.CreateAsync(user);
                googleDriveService.GetOrCreateUserFolderAsync(user.FirstName);
                var claims = new List<Claim>();
                claims = new List<Claim>
                    {
                        new Claim("Type" , "Customer"),
                    new Claim("id", createdUser.UserId.ToString()    )
                };
                if (createdUser.Role.Id != 1)
                {
                    claims.Add(new Claim("Type", "Worker"));
                }
                if (createdUser.Role.Id == 3)
                {
                    claims.Add(new Claim("Type", "Admin"));
                    claims.Add(new Claim("Type", "Admin"));
                }
                var token = TokenService.GetToken(claims);
                return Ok(new { token = TokenService.WriteToken(token), newuser = createdUser });
            }
            catch (Exception ex)
            {
                if (ex.Message.Contains("email is already"))
                {
                    return Conflict("User with this email already exists.");
                }
                if (ex.Message.Contains("role is not exist"))
                {
                    return BadRequest("Invalid role!");
                }
                throw new Exception(ex.Message);
            }
        }
        [Authorize(Policy = "Admin")]
        [HttpDelete("DeleteByEmail")]
        public async Task<ActionResult<User>> DeleteByEmail([FromQuery(Name = "email")] string email)
        {
            if (email == null) return BadRequest("Invalid email");
            bool deleted = await _userService.DeleteByEmailAsync(email);
            if (deleted)
                return Content(deleted.ToString());
            else
                return NotFound("User not found, The email not exist!");
        }
        [Authorize(Policy = "Admin")]
        [HttpDelete("DeleteById")]
        public async Task<ActionResult<bool>> Delete([FromQuery(Name = "id")] int id)
        {
            try
            {
                if (id < 0) return BadRequest("Invalid id!");
                bool deleted = await _userService.DeleteByIdAsync(id);
                if (deleted)
                    return deleted;
                else
                    return NotFound("User not found, The id not exist");
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        [Authorize(Policy = "Customer")]
        [HttpGet("GetById")]
        public async Task<ActionResult<User>> GetById([FromQuery(Name = "id")] int id)
        {
            try
            {
                return await _userService.GetByIdAsync(id);
            }
            catch (Exception ex)
            {
                if (ex.Message.Contains("user does not exist in DB"))
                    return NotFound(ex.Message);
                else throw new Exception(ex.Message);
            }
        }
        //[Authorize(Policy = "Worker")]
        [HttpGet("GetByEmail")]
        public async Task<ActionResult<User>> GetByEmail([FromQuery(Name = "email")] string email)
        {
            try
            {
                return await _userService.GetByEmailAsync(email);
            }
            catch (Exception ex)
            {
                if (ex.Message.Contains("user does not exist in DB"))
                    return null;
                throw new Exception(ex.Message);
            }
        }
        [Authorize(Policy = "Worker")]
        [HttpGet]
        public async Task<ActionResult<List<User>>> ReadAllAsync()
        {
            try
            {
                return await _userService.ReadAllAsync();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        [HttpPut]
        public async Task<bool> UpdateAsync([FromBody] User user)
        {
            return await _userService.UpdateAsync(user);
        }
        [HttpPut("Password")]
        public async Task<bool> UpdatePassword([FromBody] UserUpdateRequest request)
        {
            return await _userService.UpdatePassword(request.Email, request.Password);
        }
        [HttpGet("roles")]
        public async Task<List<RoleCode>> AllRoles()
        {
            return await _userService.ReadAllRoleAsync();
        }
    }
    public class UserUpdateRequest
    {
        public string Email { get; set; }
        public string Password { get; set; }
    }
}
