using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CopyRight.Dal.Interfaces;
using CopyRight.Dal.Models;
using Microsoft.EntityFrameworkCore;

namespace CopyRight.Dal.Service
{
    public class DocumentService:IDocument
    {
        private readonly CopyRightContext db;

        public DocumentService(CopyRightContext context)
        {
            db = context;
        }

        //public async Task<List<Document>> GetDocumentsAsync()
        //{
        //    return await db.Documents.ToListAsync();
        //}

        //public async Task<List<Document>> GetDocumentsByTitleAsync(string title)
        //{
        //    return await db.Documents.Where(d => d.Title.Contains(title)).ToListAsync();
        //}

        //public async Task<List<Document>> GetDocumentsByRelatedIdAsync(int relatedId)
        //{
        //    return await db.Documents.Where(d => d.RelatedId == relatedId).ToListAsync();
        //}

        public async Task<Document> AddDocumentAsync(Document document)
        {
            db.Documents.Add(document);
            await db.SaveChangesAsync();
            return document;
        }
    }


}
