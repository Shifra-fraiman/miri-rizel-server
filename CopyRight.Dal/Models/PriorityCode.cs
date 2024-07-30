using System;
using System.Collections.Generic;

namespace CopyRight.Dal.Models;

public partial class PriorityCode
{
    public int Id { get; set; }

    public string Description { get; set; } = null!;

    public virtual ICollection<Task> Tasks { get; set; } = new List<Task>();
}
