using System;
using System.Collections.Generic;

namespace FreshAndDelicious.Core.Models;

public partial class OrderDetails
{
    public int Id { get; set; }

    public int OrderId { get; set; }

    public int ProductId { get; set; }

    public int OrderValue { get; set; }

    public int Quantity { get; set; }

    public virtual Orders Order { get; set; } = null!;

    public virtual ICollection<OrderDetailsDiscounts> OrderDetailsDiscounts { get; set; } = new List<OrderDetailsDiscounts>();

    public virtual Products Product { get; set; } = null!;
}
