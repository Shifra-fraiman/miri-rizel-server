using System;
using System.Collections.Generic;

namespace CopyRight.Dal.Models;

public partial class Customer
{
    public int CustomerId { get; set; }

    public string FirstName { get; set; } = null!;

    public string LastName { get; set; } = null!;

    public string? Phone { get; set; }

    public string Email { get; set; } = null!;

    public string? BusinessName { get; set; }

    public string? Source { get; set; }

    public int? Status { get; set; }

    public DateTime? CreatedDate { get; set; }

    public virtual ICollection<Project> Projects { get; set; } = new List<Project>();

    public virtual StatusCodeUser? StatusNavigation { get; set; }
}
