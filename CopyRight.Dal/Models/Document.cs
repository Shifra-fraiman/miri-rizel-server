using System;
using System.Collections.Generic;

namespace CopyRight.Dal.Models;

public partial class Document
{
    public int DocumentId { get; set; }

    public string Title { get; set; } = null!;

    public string? Description { get; set; }

    public string FilePath { get; set; } = null!;

    public string RelatedTo { get; set; } = null!;

    public int RelatedId { get; set; }

    public DateTime? CreatedDate { get; set; }
}
