using System;
using System.Collections.Generic;

namespace FreshAndDelicious.Core.Models;

public partial class Orders
{
    public int Id { get; set; }

    public DateOnly OrderDate { get; set; }

    public int CustomerId { get; set; }

    public int StatusId { get; set; }

    public DateOnly? ShippedDate { get; set; }

    public int ShipperId { get; set; }

    public virtual ICollection<OrderDetails> OrderDetails { get; set; } = new List<OrderDetails>();

    public virtual ICollection<OrderDiscounts> OrderDiscounts { get; set; } = new List<OrderDiscounts>();

    public virtual Shippers Shipper { get; set; } = null!;

    public virtual OrderStatus Status { get; set; } = null!;
}
