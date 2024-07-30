using System;
using System.Collections.Generic;

namespace CopyRight.Dal.Models;

public partial class Task
{
    public int TaskId { get; set; }

    public string Title { get; set; } = null!;

    public string? Description { get; set; }

    public int Status { get; set; }

    public int? Priority { get; set; }

    public DateTime? DueDate { get; set; }

    public int? AssignedTo { get; set; }

    public int? ProjectId { get; set; }

    public string? GoogleId { get; set; }

    public DateTime? CreatedDate { get; set; }

    public virtual User? AssignedToNavigation { get; set; }

    public virtual PriorityCode? PriorityNavigation { get; set; }

    public virtual Project? Project { get; set; }

    public virtual StatusCodeProject StatusNavigation { get; set; } = null!;
}
