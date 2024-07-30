using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CopyRight.Dal.Models;

namespace CopyRight.Dal.Interfaces
{
    public interface IDocument {

          //Task<string> UplodFileAsync(Stream fileStream, string fileName, string mimeType, string folderId = null);
          //Task<List<Google.Apis.Drive.v3.Data.File>> GetFilesInFolderAsync(string folderId);
          //Task<List<Document>> GetDocumentsAsync();
          //Task<List<Document>> GetDocumentsByTitleAsync(string title);
          //Task<List<Document>> GetDocumentsByRelatedIdAsync(int relatedId);
          Task<Document> AddDocumentAsync(Document document);




    }
}
