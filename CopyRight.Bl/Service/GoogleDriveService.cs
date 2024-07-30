using Google.Apis.Auth.OAuth2;
using Google.Apis.Drive.v3;
using Google.Apis.Services;
using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using System.Linq;

namespace CopyRight.Bl.Service
{
    public class GoogleDriveService
    {
        private static readonly string[] Scopes = { DriveService.Scope.DriveFile };
        private readonly DriveService _driveService;

        public GoogleDriveService()
        {
            var credential = GoogleCredential.FromFile("Resources/credentials.json")
                .CreateScoped(Scopes);

            _driveService = new DriveService(new BaseClientService.Initializer()
            {
                HttpClientInitializer = credential,
                ApplicationName = "copyRightDrive",
            });
        }

        public async Task<string> UploadFileAsync(Stream fileStream, string fileName, string mimeType, string folderId)
        {
            var fileMetadata = new Google.Apis.Drive.v3.Data.File()
            {
                Name = fileName,
                Parents = new List<string> { folderId }
            };

            var request = _driveService.Files.Create(fileMetadata, fileStream, mimeType);
            request.Fields = "id, name, parents";

            var file = await request.UploadAsync();

            if (file.Status != Google.Apis.Upload.UploadStatus.Completed)
            {
                throw new Exception($"Upload failed: {file.Exception?.Message}");
            }

            var uploadedFile = request.ResponseBody;
            Console.WriteLine($"Uploaded file: {uploadedFile.Name} with ID: {uploadedFile.Id} in folder: {uploadedFile.Parents}");

            return uploadedFile.Id;
        }

        public async Task<List<Google.Apis.Drive.v3.Data.File>> GetFolderInParentFolderAsync(string folderId)
        {
            var request = _driveService.Files.List();
            request.Q = $"'{folderId}' in parents";
            request.Fields = "files(id, name, mimeType, parents)";

            var result = await request.ExecuteAsync();

            if (result == null || result.Files == null)
            {
                throw new Exception("Failed to retrieve files from Google Drive.");
            }

            return result.Files.ToList();
        }
        public async Task<List<Google.Apis.Drive.v3.Data.File>> GetFilesInChildFolderAsync(string childFolderId)
        {
            var request = _driveService.Files.List();
            request.Q = $"'{childFolderId}' in parents and mimeType != 'application/vnd.google-apps.folder'";
            request.Fields = "files(id, name, mimeType, parents, webViewLink, thumbnailLink)";

            var result = await request.ExecuteAsync();

            if (result == null || result.Files == null)
            {
                throw new Exception("Failed to retrieve files from Google Drive.");
            }

            return result.Files.ToList();
        }


        public async Task<string> CreateFolderAsync(string folderName, string parentFolderId)
        {
            var fileMetadata = new Google.Apis.Drive.v3.Data.File()
            {
                Name = folderName,
                MimeType = "application/vnd.google-apps.folder",
                Parents = new List<string> { parentFolderId }
            };

            var request = _driveService.Files.Create(fileMetadata);
            request.Fields = "id, name, parents";

            var file = await request.ExecuteAsync();

            Console.WriteLine($"Created folder: {file.Name} with ID: {file.Id} in parent folder: {file.Parents}");
            return file.Id;
        }

        public async Task<string> GetOrCreateUserFolderAsync(string userName)
        {
            string parentFolderId = "165l3OCzzhcYUrg3-P4uj0EvXg8iTFEvv";
            var request = _driveService.Files.List();
            request.Q = $"mimeType='application/vnd.google-apps.folder' and '{parentFolderId}' in parents and name='{userName}'";
            request.Fields = "files(id, name)";
            var result = await request.ExecuteAsync();
            if (result.Files.Any())
            {
                var existingFolder = result.Files.First();
                return existingFolder.Id;
            }
            else
            {
                return await CreateFolderAsync(userName, parentFolderId);
            }
        }
        public async Task<string> UploadUserFileAsync(Stream fileStream, string fileName, string mimeType, string userName)
            
        {
            string parentFolderId = "165l3OCzzhcYUrg3-P4uj0EvXg8iTFEvv";

            var userFolderId = await GetOrCreateUserFolderAsync(userName);

          string idFolder=  await UploadFileAsync(fileStream, fileName, mimeType, userFolderId);
            return idFolder;
        }

    }
}
