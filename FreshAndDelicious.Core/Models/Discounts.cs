using System;
using System.Collections.Generic;

namespace FreshAndDelicious.Core.Models;

public partial class Discounts
{
    public int Id { get; set; }

    public string DiscountName { get; set; } = null!;

    public string DiscountType { get; set; } = null!;

    public decimal DiscountValue { get; set; }

    public int BuyQuantity { get; set; }

    public int FreeQuantity { get; set; }

    public string CouponCode { get; set; } = null!;

    public DateOnly StartDate { get; set; }

    public DateOnly EndDate { get; set; }

    public string AppliesTo { get; set; } = null!;

    public bool IsActive { get; set; }

    public virtual ICollection<OrderDetailsDiscounts> OrderDetailsDiscounts { get; set; } = new List<OrderDetailsDiscounts>();

    public virtual ICollection<OrderDiscounts> OrderDiscounts { get; set; } = new List<OrderDiscounts>();

    public virtual ICollection<Products> Products { get; set; } = new List<Products>();
}
