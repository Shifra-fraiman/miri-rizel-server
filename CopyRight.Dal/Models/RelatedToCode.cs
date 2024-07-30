using System;
using System.Collections.Generic;

namespace CopyRight.Dal.Models;

public partial class RelatedToCode
{
    public int Id { get; set; }

    public string Description { get; set; } = null!;

    public virtual ICollection<Communication> Communications { get; set; } = new List<Communication>();
}
