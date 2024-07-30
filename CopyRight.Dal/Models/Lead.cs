using System;
using System.Collections.Generic;

namespace CopyRight.Dal.Models;

public partial class Lead
{
    public int LeadId { get; set; }

    public string FirstName { get; set; } = null!;

    public string LastName { get; set; } = null!;

    public string? Phone { get; set; }

    public string? Email { get; set; }

    public string? Source { get; set; }

    public DateTime? CreatedDate { get; set; }

    public DateTime? LastContactedDate { get; set; }

    public string? BusinessName { get; set; }

    public string? Notes { get; set; }
}
