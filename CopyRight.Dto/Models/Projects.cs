using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CopyRight.Dto.Models
{
    public class Projects
    {
        public int ProjectId { get; set; }

        public string Name { get; set; } = null!;

        public string? Description { get; set; }

        public DateTime? StartDate { get; set; }

        public DateTime? EndDate { get; set; }

        public int? Authorize { get; set; }

        public StatusCodeProject? Status { get; set; }

        public Customers? Customer { get; set; }

        public DateTime? CreatedDate { get; set; }
        public bool? IsActive { get; set; }

    }
}
