using System;
using System.Collections.Generic;

namespace CopyRight.Dal.Models;

public partial class Communication
{
    public int CommunicationId { get; set; }

    public string Type { get; set; } = null!;

    public DateTime? Date { get; set; }

    public string? Details { get; set; }

    public int RelatedTo { get; set; }

    public int RelatedId { get; set; }

    public virtual RelatedToCode RelatedToNavigation { get; set; } = null!;
}
