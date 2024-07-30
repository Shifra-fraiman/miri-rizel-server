using System.Collections.Generic;
using System.Threading.Tasks;
using CopyRight.Dto.Models;


namespace CopyRight.Bl.Interfaces
{
    public interface IDocument
    {
        
        Task<Documents> AddDocumentAsync(Documents document);
        Task<bool> SendEmail(string nameCustomer);



    }
}
