using System;
using System.Collections.Generic;

namespace CopyRight.Dal.Models;

public partial class RoleCode
{
    public int Id { get; set; }

    public string Description { get; set; } = null!;

    public virtual ICollection<Project> Projects { get; set; } = new List<Project>();

    public virtual ICollection<User> Users { get; set; } = new List<User>();
}
