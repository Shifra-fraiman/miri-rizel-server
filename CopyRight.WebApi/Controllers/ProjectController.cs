

using CopyRight.Bl.Service;
using CopyRight.Dto.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Components.Forms;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;

namespace CopyRight.WebApi.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class projectsController : ControllerBase
    {
        private Bl.Interfaces.ITasks _BlTasks;
        private Bl.Interfaces.IProject _BlProject;
        public projectsController(Bl.Interfaces.IProject p, Bl.Interfaces.ITasks t)
        {
            this._BlProject = p;
            this._BlTasks = t;
        }

        [Authorize(Policy = "Worker")]
        [HttpPost]
        public async Task<IActionResult> Add(Projects newProject)
        {

            if (await _BlProject.IsOnTheDB(newProject.Customer.CustomerId))
            {
                if (_BlProject.IsOnlyLetters(newProject.Name))
                {
                    if (_BlProject.IsCorrectDates(newProject.StartDate, newProject.EndDate))
                    {
                        if (await _BlProject.CreateAsync(newProject) != null)
                        {
                            return StatusCode(200, newProject);
                        }
                        return BadRequest(500);
                    }
                    return StatusCode(400, $"the startDate is  after the endDate");
                }
                return StatusCode(400, $"the name isnt valid");
            }
            return StatusCode(400, $"This customer not exist on db");


        }
        [Authorize(Policy = "Admin")]
        [HttpDelete]
        public async Task<ActionResult<bool>> Delete([FromQuery(Name = "id")] int id)
        {
            Console.WriteLine( id);
            if (id < 0) return BadRequest("Invalid id!");
            bool deleted = await _BlProject.DeleteAsync(id);
            if (deleted)
                return deleted;
            else
                return NotFound("project not found!");
        }
        [Authorize(Policy = "Worker")]
        [HttpGet]
        public async Task<ActionResult<Projects[]>> GetAllActive()
        {
            string token = HttpContext.Request.Headers["Authorization"].ToString().Replace("Bearer ", "").Trim();

            if (string.IsNullOrEmpty(token))
            {
                throw new ArgumentException("Token is missing or empty.");
            }
            var tokenValidationParameters = TokenService.GetTokenValidationParameters();
            var tokenHandler = new JwtSecurityTokenHandler();
            try
            {
                

                tokenHandler.ValidateToken(token, tokenValidationParameters, out SecurityToken validatedToken);
                var jwtToken = validatedToken as JwtSecurityToken;
                var claims = jwtToken.Claims;
                Console.WriteLine(claims);
                var typeClaims = claims.Where(c => c.Type == "Type").Select(c => c.Value).ToList();
                foreach (var typeClaim in typeClaims)
                {


                    if (typeClaims.Count > 0 && typeClaims[typeClaims.Count - 1] == "Worker")

                    {
                        List<Projects> projects = await _BlProject.ReadAsync(x => x.IsActive == true && x.Authorize == 1);
                        return Ok(projects);
                    }


                    if (typeClaim == "Admin")
                    {

                        List<Projects> projects = await _BlProject.ReadAllAsync();
                        return Ok(projects);

                    }

                }



            }
            catch (Exception ex) { }


            return Ok("ok");


        }
        //[Authorize(Policy = "Admin")]
        [Route("getUnActiveProject")]
        [HttpGet]
        public async Task<ActionResult<Projects[]>> GetAll()
        {
            List<Projects> p = await _BlProject.ReadAsync(o => o.IsActive == false);
            return Ok(p);
        }

        [Authorize(Policy = "Worker")]
        [Route("getAllTasks")]
        [HttpGet]
        public async Task<ActionResult<Tasks>> GetAllTasks([FromQuery(Name = "id")] int id)
        {

            string token = HttpContext.Request.Headers["Authorization"].ToString().Replace("Bearer ", "");

            var tokenValidationParameters = TokenService.GetTokenValidationParameters();
            var tokenHandler = new JwtSecurityTokenHandler();
            try
            {
                tokenHandler.ValidateToken(token, tokenValidationParameters, out SecurityToken validatedToken);
                var jwtToken = validatedToken as JwtSecurityToken;
                var claims = jwtToken.Claims;
                Console.WriteLine(claims);
                var typeClaims = claims.Where(c => c.Type == "Type").Select(c => c.Value).ToList();
                foreach (var typeClaim in typeClaims)
                {
                    Console.WriteLine($"Type Claim Value: {typeClaim}");
                    if (typeClaims.Count > 0 && typeClaims[typeClaims.Count - 1] == "Worker")

                    {
                        Console.WriteLine("i am Worker");
                        bool checkAuth = await _BlProject.ReadTaskAuthAsync(id);

                        Console.WriteLine(checkAuth);
                        Console.WriteLine(checkAuth);
                        Console.WriteLine(checkAuth);
                    
                       

                    }
                    if (typeClaim == "Admin")
                    {
                        Console.WriteLine("i am admin");
                        List<Tasks> p = await _BlTasks.ReadAsync(o => o.Project.ProjectId == id);
                        return Ok(p);

                    }
                }

            }
            catch (Exception ex)

            {
            }


            return Ok("ok");


        }





        [Authorize(Policy = "Worker")]
        [Route("getById")]

        [HttpGet]
        //[Authorize(Policy = "Worker")]
        //[HttpGet("GetProjectById")]
        public async Task<ActionResult<Projects>> GetProjectById(int id)
        {


            string token = HttpContext.Request.Headers["Authorization"].ToString().Replace("Bearer ", "");

            var tokenValidationParameters = TokenService.GetTokenValidationParameters();
            var tokenHandler = new JwtSecurityTokenHandler();
            try
            {
                tokenHandler.ValidateToken(token, tokenValidationParameters, out SecurityToken validatedToken);
                var jwtToken = validatedToken as JwtSecurityToken;
                var claims = jwtToken.Claims;
                Console.WriteLine(claims);
                var typeClaims = claims.Where(c => c.Type == "Type").Select(c => c.Value).ToList();
                foreach (var typeClaim in typeClaims)
                {
                    { 
                    Console.WriteLine($"Type Claim Value: {typeClaim}"); }
                    if (typeClaims.Count > 0 && typeClaims[typeClaims.Count - 1] == "Worker")

                    {
                        Console.WriteLine("i am Worker");
                        bool checkAuth = await _BlProject.ReadTaskAuthAsync(id);
                        Console.WriteLine(checkAuth);
                        if (checkAuth)
                        {
                            List<Projects> p = await _BlProject.ReadAsync(o => o.ProjectId == id);
                            return Ok(p);
                        }
                        else if (!checkAuth)
                        {
                            return BadRequest("this project is not auth");
                        }

                    }
                    if (typeClaim == "Admin")
                    {
                       
                        List<Projects> p = await _BlProject.ReadAsync(o => o.ProjectId == id);
                        return Ok(p);

                    }
                }

            }
            catch (Exception ex)

            {
            }
            return Ok("ok");
        }


       
          
     
        [Authorize(Policy = "Worker")]
        [HttpPut]
        public async Task<bool> UpdateProject(Dto.Models.Projects p)
        {
            return await _BlProject.UpdateAsync(p);
        }



    }
}