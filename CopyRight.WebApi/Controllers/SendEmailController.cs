using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Net.Mail;
using System.Net;
using CopyRight.Bl;
using CopyRight.Bl.Interfaces;

using Microsoft.AspNetCore.Identity.Data;
using CopyRight.Dal.Models;
using CopyRight.Dto.Models;



namespace CopyRight.WebApi.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class SendEmailController : ControllerBase
    {
        private IUser _BlUser;
        public SendEmailController(IUser u)
        {
            this._BlUser = u;
        }

        //[Route("sign-up")]
        //[HttpPost]
        //public async Task<IActionResult> reset([FromBody] string name)
        //{  
        //    if (await _BlUser.SendEmailSignUp(name))
        //        return StatusCode(200);
        //    return StatusCode(500, $"failed on the send");
        //}


        [HttpPost]
        public async Task<IActionResult> reset([FromBody] EmailRequest emailRequest)
        {
            if (await _BlUser.SendEmailSignUp(emailRequest.Name))
                return StatusCode(200);
            return StatusCode(500, "Failed to send email");
        }

        public class EmailRequest
        {
            public string Name { get; set; }
        }



    }
}
