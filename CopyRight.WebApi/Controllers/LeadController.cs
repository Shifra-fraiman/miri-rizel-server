
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
using Microsoft.AspNetCore.Authorization;

namespace CopyRight.WebApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class LeadController : ControllerBase
    {
        public ILead _leadService { get; set; }
        public LeadController(ILead leadService)
        {
            this._leadService = leadService;
        }
        [Authorize(Policy = "Worker")]
        [HttpGet]
        public async Task<ActionResult<List<Leads>>> ReadAllAsync()
        {

            try
            {
                List<Leads> lead = await _leadService.ReadAllAsync();
                if (lead.Count == 0) { return NotFound("Lead  Not exsist "); }
                return lead;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.ToString(), ex);
            }
        }
        [Authorize(Policy = "Worker")]
        [HttpGet("GetById")]
        public async Task<ActionResult<Leads>> GetByIdAsync([FromQuery(Name = "id")] int id)
        {
            try
            {
                ActionResult<Leads> l = await _leadService.GetByIdAsync(id);
                if (l != null)
                    return l;
                else
                    return NotFound("Lead not found!");
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }

        }
        [Authorize(Policy = "Worker")]
        [HttpPost]
        public async Task<ActionResult<Leads>> CreateAsync([FromBody] Leads lead)
        {
            try
            {
                if (lead == null)
                    return BadRequest("Invalid body request!");
                Leads createdLead = await _leadService.CreateAsync(lead);
                return Ok(createdLead);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }

        }
        [Authorize(Policy = "Worker")]
        [HttpGet("existsEmail")]
        public async Task<ActionResult<bool>> existsEmail([FromQuery(Name = "Email")] string customerEmail)
        {
            try
            {
                bool existsEmail = await _leadService.existsEmailAsync(customerEmail);
                return Ok(existsEmail);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }

        }
        [Authorize(Policy = "Worker")]
        [HttpPut]
        public async Task<ActionResult<Lead>> UpdateAsync([FromBody] Leads lead)
        {
            try
            {
                bool UpdateLead = await _leadService.UpdateAsync(lead);
                if (UpdateLead)
                    return Content(UpdateLead.ToString());
                else
                    return NotFound("Lead not found!");
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }

        }
        [Authorize(Policy = "Admin")]
        [HttpDelete]
        public async Task<ActionResult<bool>> DeleteAsync([FromQuery(Name = "id")] int id)
        {
            try
            {
                if (id < 0) return BadRequest("Invalid id!");
                bool deleted = await _leadService.DeleteAsync(id);
                if (deleted)
                    return true;
                else
                    return NotFound("Lead not found!");
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }

        }

        [Authorize(Policy = "Worker")]
        [HttpPost("Replace")]
        public async Task<ActionResult<Customers>> replaceToCustomer([FromBody]Leads lead)
        {
            try
            {
                Customers customer = await _leadService.replaceToCustomer(lead);
                if (customer != null)
                    return Ok(customer);
                else
                    return NotFound("The replace faild!");
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
        }
    }
}
