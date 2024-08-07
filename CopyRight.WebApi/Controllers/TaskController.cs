using CopyRight.Dto.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc;
using CopyRight.Bl.Interfaces;
using CopyRight.Dto.Models;
using Microsoft.AspNetCore.Authorization;
using CopyRight.Bl.Service;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
namespace CopyRight.WebApi.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class TaskController : ControllerBase
    {
        private readonly ITasks _taskService;

        public TaskController(ITasks taskService)
        {
            _taskService = taskService;
        }
        [Authorize(Policy = "Worker")]
        [HttpPost]
        public async Task<Tasks> CreateAsync([FromBody] Tasks task)
        {
            if (task == null)
            {
                return null;
            }

            try
            {
                var createdTask = await _taskService.CreateAsync(task);
                return createdTask;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        [Authorize(Policy = "Admin")]

        [HttpDelete]
        public async Task<ActionResult> Delete(int id)
        {
            if (id == null)
            {
                return BadRequest();
            }
            return Ok(await _taskService.DeleteAsync(id));
        }

        [Authorize(Policy = "Worker")]
        [HttpGet]
        public async Task<ActionResult<List<Tasks>>> ReadAll()
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
                        List<Tasks> tasks = await _taskService.ReadTaskAsync();
                        return Ok(tasks);

                    }


                    if (typeClaim == "Admin")
                    {

                        List<Tasks> tasks = await _taskService.ReadAllAsync();
                        return Ok(tasks);

                    }
                }
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
            return BadRequest("this authorize is not auth");
        }

        [Authorize(Policy = "Worker")]
        [HttpPut()]
        public async Task<ActionResult> Update([FromBody] Tasks task)
        {
            if (task == null)
            {
                return BadRequest();
            }
            try
            {
                return Ok(await _taskService.UpdateAsync(task));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [Authorize(Policy = "Worker")]
        [HttpGet]
        [Route("GetById")]
        public async Task<ActionResult<Tasks>> GetByIdAsync([FromQuery(Name = "id")] int id)
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
                        bool checkauth = await _taskService.ReadTaskAuthAsync(id);

                        if (checkauth)
                        {
                            var tasks = await _taskService.GetById(id);
                            return Ok(tasks);
                        }
                        else if (!checkauth)
                        {
                            return BadRequest("this task is not auth");
                        }

                    }




                    if (typeClaim == "Admin")
                    {

                        var tasks = await _taskService.GetById(id);
                        return Ok(tasks);
                    }
                }

            }
            catch
            {
                return null;
            }
            return Ok("ok");
        }
       
       [Authorize(Policy = "Worker")]
        [HttpGet]
       [Route("ReadAllStatus")]
        public async Task<List<StatusCodeProject>> ReadAllStatus()
        {
            try
            {
                return await _taskService.ReadAllStatusAsync();
            }
            catch
            {
                return null;
            }
        }
       [Authorize(Policy = "Worker")]
        [HttpGet]
        [Route("ReadAllPriority")]
        public async Task<List<PriorityCode>> ReadAllPriority()
        {
            try
            {
                return await _taskService.ReadAllPriorityAsync();
            }
            catch
            {
                return null;
            }
        }

        //[Authorize(Policy = "Worker")]
        // [HttpPut]
        // [Route("googleCalendar")]
        // public async Task<bool> UpdateGoogleCalendarAsync([FromQuery(Name = "taskId")] int taskId, [FromQuery(Name = "googleId")] string googleId)
        // {
        //     return await _taskService.UpdateGoogleCalendarAsync(taskId, googleId);
        // }

        [Authorize(Policy = "Worker")]
        [HttpPost("googleCalendar")]
        public async Task<bool> UpdatePassword([FromBody] UpdateRequest request)
        {
            return await _taskService.UpdateGoogleCalendarAsync(request.taskId, request.googleId);
        }
    }
    public class UpdateRequest
    {
        public int taskId { get; set; }
        public string googleId { get; set; }
    }
}
