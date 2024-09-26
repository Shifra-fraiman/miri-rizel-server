using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CopyRight.Dto.Models
{
    public class Communications
    {
        public int CommunicationId { get; set; }

        public string Type { get; set; } = null!;

        public DateTime? Date { get; set; }

        public string? Details { get; set; }

        public relatedToCode RelatedTo { get; set; } = null!;

        public int RelatedId { get; set; }
        public string Name { get; set; }

    }
}
