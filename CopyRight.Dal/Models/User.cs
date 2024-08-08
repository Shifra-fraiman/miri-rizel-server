using System;
using System.Collections.Generic;

namespace CopyRight.Dal.Models;

public partial class User
{
    public int UserId { get; set; }

    public string FirstName { get; set; } = null!;

    public string LastName { get; set; } = null!;

    public string Email { get; set; } = null!;

    public int Role { get; set; }

    public string Password { get; set; } = null!;

    public DateTime? CreatedDate { get; set; }

    public bool? IsActive { get; set; }

    public virtual RoleCode RoleNavigation { get; set; } = null!;

    public virtual ICollection<Task> Tasks { get; set; } = new List<Task>();
}
