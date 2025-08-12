using System;
using System.Collections.Generic;

namespace FreshAndDelicious.Core.Models;

public partial class OrderStatus
{
    public int Id { get; set; }

    public string StatusType { get; set; } = null!;

    public virtual ICollection<Orders> Orders { get; set; } = new List<Orders>();
}
