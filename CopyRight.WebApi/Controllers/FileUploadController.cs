using CopyRight.Bl;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Linq;
using CopyRight.Bl.Service;
using System.IO;
using System.Threading.Tasks;

namespace CopyRight.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FileUploadController : ControllerBase
    {
        private readonly GoogleDriveService _googleDriveService;

        public FileUploadController(GoogleDriveService googleDriveService)
        {
            _googleDriveService = googleDriveService;
        }


        [HttpPost("upload")]
        [Authorize(Policy="Admin")]
        public async Task<string> UploadFile(IFormFile file,[FromQuery] string nameFolder)
        {
            if (file == null || file.Length == 0)
            {
                throw new Exception();
            }

            string fileName = file.FileName;
            string mimeType = file.ContentType;

            using (var fileStream = file.OpenReadStream())
            {
                try
                {


                    string id= await _googleDriveService.UploadUserFileAsync(fileStream, fileName, mimeType, nameFolder);
                    return id;
                }
                catch (Exception ex)
                {
                    throw new Exception(ex.Message);
                }
            }
        }
        [HttpGet("folders")]
        [Authorize(Policy="Admin")]

        public async Task<ActionResult> GetFolders()
        {
            string parentFolderId = "1h99QmpROSgDnlrUckbAWnhWzskCMXUxQ";
            try
            {
                var folders = await _googleDriveService.GetFolderInParentFolderAsync(parentFolderId);
                var folderList = folders.Where(f => f.MimeType == "application/vnd.google-apps.folder").ToList();
                return Ok(folderList);
            }
            catch (Exception ex)
            {
                // טיפול בשגיאות
                return StatusCode(StatusCodes.Status500InternalServerError, new { message = ex.Message });
            }
        }
        [Authorize(Policy = "Admin")]
        [HttpGet("folders/{folderId}/files")]
        public async Task<ActionResult> GetFilesInFolder(string folderId)
        {
            var files = await _googleDriveService.GetFilesInChildFolderAsync(folderId);
            return Ok(files);
        }
    }
}

    public class CreateFolderRequest
    {
        public string UserName { get; set; }
        public string ParentFolderId { get; set; }
    }


