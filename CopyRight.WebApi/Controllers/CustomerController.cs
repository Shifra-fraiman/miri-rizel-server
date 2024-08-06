using CopyRight.Bl.Interfaces;
using CopyRight.Dto.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using CopyRight.Bl.Service;

namespace CopyRight.WebApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CustomerController : ControllerBase
    {
        public ICustomer _customerService { get; set; }
        private readonly GoogleDriveService googleDriveService;

        public CustomerController(ICustomer customerService, GoogleDriveService googleDriveService)
        {
            this.googleDriveService = googleDriveService;
            this._customerService = customerService;
        }
        [Authorize(Policy = "Worker")]
        [HttpGet]
        public async Task<ActionResult<List<Customers>>> ReadAllAsync()
        {
            try
            {
                List<Customers> AllCustomers = await _customerService.ReadAllAsync();
                return Ok(AllCustomers);
            }
            catch (Exception ex)
            {
                throw new Exception
                    (ex.Message, ex);
            }

        }
        [Authorize(Policy = "Worker")]
        [HttpGet("GetById")]
        public async Task<ActionResult<Customers>> GetByIdAsync([FromQuery(Name = "custometId")] int customerId)
        {
            try
            {
                Customers customer = await _customerService.GetByIdAsync(customerId);
                return Ok(customer);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }


        }
        [Authorize(Policy = "Worker")]
        [HttpPost]
        public async Task<ActionResult<Customers>> CreateAsync([FromBody] Customers newCustomer)
        {
            try
            {
                Customers customer = await _customerService.CreateAsync(newCustomer);
                string fullName = newCustomer.FirstName + " " + customer.LastName;

                googleDriveService.GetOrCreateUserFolderAsync(fullName);

                return Ok(customer);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
        }
        [Authorize(Policy = "Worker")]
        [HttpPut]
        public async Task<ActionResult<bool>> UpdateAsync([FromBody] Customers editCustomer)
        {
            try
            {
                bool editCustomerSuccess = await _customerService.UpdateAsync(editCustomer);
                return Ok(editCustomerSuccess);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
        }
        [Authorize(Policy = "Admin")]
        [HttpDelete("DeletByEmail")]
        public async Task<ActionResult<bool>> DeletByEmailAsync([FromQuery(Name = "customerEmail")] string customerEmail)
        {
            try
            {
                bool deleteCustomerSuccess = await _customerService.DeletByEmailAsync(customerEmail);
                return Ok(deleteCustomerSuccess);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }


        }
        [Authorize(Policy = "Admin")]
        [HttpDelete]
        public async Task<ActionResult<bool>> DeleteByIdAsync([FromQuery(Name = "customerId")] int customerId)
        {
            try
            {
                bool deleteCustomerSuccess = await _customerService.DeleteAsync(customerId);
                return Ok(deleteCustomerSuccess);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);

            }


        } 
        [Authorize(Policy = "Worker")]
        [HttpGet("GetAllStatus")]
        public async Task<List<StatusCodeUser>> getAllStatusCodeUser()
        {
            try
            {
                return await _customerService.GetCustomerStatusAsync();
            }
            catch
            {
                return null;
            }
        }
    }
}