using System;
using System.Collections.Generic;

namespace FreshAndDelicious.Core.Models;

public partial class OrderDiscounts
{
    public int Id { get; set; }

    public int OrderId { get; set; }

    public int DiscountId { get; set; }

    public decimal AppliedValue { get; set; }

    public virtual Discounts Discount { get; set; } = null!;

    public virtual Orders Order { get; set; } = null!;
}
