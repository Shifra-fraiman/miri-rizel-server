using System.Collections.Generic;
using System.Threading.Tasks;
using CopyRight.Bl.Service;
using CopyRight.Dal.Models;
using Microsoft.AspNetCore.Mvc;
using CopyRight.Bl;
using CopyRight.Bl.Interfaces;
using CopyRight.Dto.Models;
using Microsoft.AspNetCore.Authorization;
using CopyRight.Dal.Service;

namespace CopyRight.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DocumentController : ControllerBase
    {
        private readonly IDocument _documentService;

        public DocumentController(IDocument documentService)
        {
            _documentService = documentService;
        }


        [HttpPost]

        public async Task<ActionResult<Documents>> AddDocument([FromBody] Documents document, [FromQuery(Name = "name")] string name)
        {
            if (document == null)
            {
                throw new Exception();
            }

            var addedDocument = await _documentService.AddDocumentAsync(document);
            try
            {
                if (await _documentService.SendEmail(name))
                    return StatusCode(200, addedDocument); ;
                return StatusCode(500, "Failed to send email");
            }
            catch (Exception ex)
            {
                return StatusCode(400, $"Error sending email: {ex.Message}");
            }

        }



    }

}

