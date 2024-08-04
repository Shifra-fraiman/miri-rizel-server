

using CopyRight.Dto.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Components.Forms;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis.CSharp.Syntax;

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
                        try
                        {
                            _BlProject.CreateAsync(newProject);
                            return StatusCode(200, $"the project added succeful");
                        }
                        catch (Exception ex)
                        {
                            throw new Exception(ex.Message);

                        }
                        
                    }
                    else
                        return StatusCode(400, $"the startDate is  after the endDate");
                }
                else
                    return StatusCode(400, $"the name isnt valid");
            }
            else
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
            List<Projects> p = await _BlProject.ReadAsync(o=>o.IsActive==true);
            return Ok(p);
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
            List<Tasks> p = await _BlTasks.ReadAsync(o => o.Project.ProjectId == id);
            return Ok(p);


        }
        [Authorize(Policy = "Worker")]
        [Route("getById")]

        [HttpGet]
        //[Authorize(Policy = "Worker")]
        //[HttpGet("GetProjectById")]
        public async Task<ActionResult<Projects>> GetProjectById(int id)
        {
         List< Projects >   p = await _BlProject.ReadAsync(i=>i.ProjectId==id);
            return Ok(p.First());
        }
        [Authorize(Policy = "Worker")]
        [HttpPut]
        public async Task<bool> UpdateProject(Dto.Models.Projects p)
        {
            return await _BlProject.UpdateAsync(p);
        }



    }
}