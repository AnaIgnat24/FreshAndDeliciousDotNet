using System;
using System.Collections.Generic;

namespace FreshAndDelicious.Core.Models;

public partial class OrderDetailsDiscounts
{
    public int Id { get; set; }

    public int OrderDetailId { get; set; }

    public int DiscountId { get; set; }

    public decimal AppliedValue { get; set; }

    public virtual Discounts Discount { get; set; } = null!;

    public virtual OrderDetails OrderDetail { get; set; } = null!;
}
