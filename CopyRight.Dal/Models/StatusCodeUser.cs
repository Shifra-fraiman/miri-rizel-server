using System;
using System.Collections.Generic;

namespace CopyRight.Dal.Models;

public partial class StatusCodeUser
{
    public int Id { get; set; }

    public string Description { get; set; } = null!;

    public virtual ICollection<Customer> Customers { get; set; } = new List<Customer>();
}
