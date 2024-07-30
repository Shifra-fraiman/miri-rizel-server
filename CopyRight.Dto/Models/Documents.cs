using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CopyRight.Dto.Models
{
    public class Documents
    {
        public int DocumentId { get; set; }

        public string Title { get; set; } = null!;

        public string? Description { get; set; }

        public string FilePath { get; set; } = null!;

        public string RelatedTo { get; set; } = null!;

        public int RelatedId { get; set; }

        public DateTime? CreatedDate { get; set; }
    }
}
