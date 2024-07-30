using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CopyRight.Dto.Models
{
    public class Customers
    {
        public int? CustomerId { get; set; }

        public string FirstName { get; set; } = null!;

        public string LastName { get; set; } = null!;

        public string? Phone { get; set; }

        public string Email { get; set; } = null!;

        public string? BusinessName { get; set; }

        public string? Source { get; set; }

        public StatusCodeUser? Status { get; set; }

        public DateTime? CreatedDate { get; set; }

       
    }
}
