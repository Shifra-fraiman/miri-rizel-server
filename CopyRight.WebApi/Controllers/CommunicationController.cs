
using Microsoft.AspNetCore.Components.Web;
using CopyRight.Bl.Interfaces;
using CopyRight.Dto.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using CopyRight.Dal.Models;
using CopyRight.Bl;
using CopyRight.Dal.Interfaces;

namespace CopyRight.WebApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CommunicationController : ControllerBase
    {
        public Bl.Interfaces.ICommunication _CommunicationService { get; set; }
        public CommunicationController(Bl.Interfaces.ICommunication communicationService)
        {
            this._CommunicationService = communicationService;
        }

        [HttpGet]
        public async Task<ActionResult<List<Communications>>> ReadAll()
        {
            try
            {
                var tasks = await _CommunicationService.ReadAllAsync();
                return Ok(tasks);
            }

            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
        }

        [HttpGet("GetById")]
        public async Task<ActionResult<Communications>> GetByIdAsync([FromQuery(Name = "id")] int id)
        {
            try
            {
                var l = await _CommunicationService.GetByIdAsync(id);
                if (l != null)
                    return Ok(l);
                else
                    return NotFound("Communication not found!");
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }

        }

        [HttpPost]
        public async Task<ActionResult<Communications>> CreateAsync([FromBody] Communications c)
        {
            try
            {
                if (c == null)
                    return BadRequest("Invalid body request!");
                Communications createdCommunication = await _CommunicationService.CreateAsync(c);
                return Ok(c);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
        }

        [HttpDelete]
        public async Task<ActionResult<bool>> DeleteAsync([FromQuery(Name = "id")] int id)
        {
            try
            {
                if (id < 0) return BadRequest("Invalid id!");
                bool deleted = await _CommunicationService.DeleteAsync(id);
                if (deleted)
                    return true;
                else
                    return NotFound("Communication not found!");
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }

        }
    }
}
