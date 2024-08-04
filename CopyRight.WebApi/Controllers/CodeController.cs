using CopyRight.Dto.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CopyRight.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CodeController : ControllerBase
    {
        public Bl.Interfaces.IPriorityCode _priority { get; set; }
        public Bl.Interfaces.IRelatedToCode _related { get; set; }
        public Bl.Interfaces.IRoleCode _roles { get; set; }
        public Bl.Interfaces.IStatusCodeProject _statusP { get; set; }
        public Bl.Interfaces.ITasks _statusU { get; set; }
        public CodeController
        (Bl.Interfaces.ITasks _statusU, Bl.Interfaces.IStatusCodeProject _statusP, Bl.Interfaces.IRoleCode _roles, Bl.Interfaces.IRelatedToCode _related, Bl.Interfaces.IPriorityCode _priority)
        {
            this._priority=_priority;
            this._related = _related;
            this._roles = _roles;
            this._statusP = _statusP;
            this._statusU = _statusU;
        }
        [Route("getAllPriorities")]
        [HttpGet]
        [Authorize(Policy = "Worker")]

        public async Task<ActionResult<List<PriorityCode>>> ReadAll()
        {
            try
            {
                var tasks = await _priority.ReadAllAsync();
                return Ok(tasks);
            }

            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
        }
        [Route("getAllStatusProject")]
        [HttpGet]
        [Authorize(Policy = "Worker")]

        public async Task<ActionResult<List<StatusCodeProject>>> ReadAllsp()
        {
            try
            {
                var tasks = await _statusP.ReadAllAsync();
                return Ok(tasks);
            }

            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
        }
        [Route("getAllStatusUser")]
        [HttpGet]
        [Authorize(Policy = "Worker")]

        public async Task<ActionResult<List<StatusCodeUser>>> ReadAllsu()
        {
            try
            {
                var tasks = await _statusU.ReadAllAsync();
                return Ok(tasks);
            }

            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
        }
        [Route("getAllRoles")]
        [HttpGet]
        [Authorize(Policy = "Worker")]

        public async Task<ActionResult<List<RoleCode>>> ReadAllRoles()
        {
            try
            {
                var tasks = await _roles.ReadAllAsync();
                return Ok(tasks);
            }

            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
        }
        [Route("getAllReletedCode")]
        [HttpGet]
        [Authorize(Policy = "Worker")]

        public async Task<ActionResult<List<relatedToCode>>> ReadAllReleted()
        {
            try
            {
                var tasks = await _related.ReadAllAsync();
                return Ok(tasks);
            }

            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
        }
        [Route("CreateRole")]
        [HttpPost]
        [Authorize(Policy = "Worker")]

        public async Task<ActionResult<RoleCode>> CreateAsync([FromBody] RoleCode c)
        {
            try
            {
                if (c == null)
                    return BadRequest("Invalid body request!");
                RoleCode created = await _roles.CreateAsync(c);
                return Ok(c);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
        }

        [Route("CreatePriority")]
        [HttpPost]
        [Authorize(Policy = "Worker")]

        public async Task<ActionResult<PriorityCode>> CreateAsyncp([FromBody] PriorityCode c)
        {
            try
            {
                if (c == null)
                    return BadRequest("Invalid body request!");
                PriorityCode created = await _priority.CreateAsync(c);
                return Ok(c);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
        }
        [Route("CreateReleted")]
        [HttpPost]
        [Authorize(Policy = "Worker")]

        public async Task<ActionResult<relatedToCode>> CreateAsync([FromBody] relatedToCode c)
        {
            try
            {
                if (c == null)
                    return BadRequest("Invalid body request!");
                relatedToCode created = await _related.CreateAsync(c);
                return Ok(c);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
        }
        [Route("CreateStatusProject")]
        [HttpPost]
        [Authorize(Policy = "Worker")]

        public async Task<ActionResult<StatusCodeProject>> CreateAsync([FromBody] StatusCodeProject c)
        {
            try
            {
                if (c == null)
                    return BadRequest("Invalid body request!");
                StatusCodeProject created = await _statusP.CreateAsync(c);
                return Ok(c);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
        }
        //[Route("CreateStatusCodeUser")]
        //[HttpPost]
        //public async Task<ActionResult<StatusCodeUser>> CreateAsync([FromBody] StatusCodeUser c)
        //{
        //    try
        //    {
        //        if (c == null)
        //            return BadRequest("Invalid body request!");
        //        StatusCodeUser created = await _statusU.CreateAsync(c);
        //        return Ok(c);
        //    }
        //    catch (Exception ex)
        //    {
        //        throw new Exception(ex.Message, ex);
        //    }
        //}
    }
}
