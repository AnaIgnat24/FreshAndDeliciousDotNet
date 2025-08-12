using System;
using System.Collections.Generic;

namespace FreshAndDelicious.Core.Models;

public partial class ProductReviews
{
    public int Id { get; set; }

    public int ProductId { get; set; }

    /// <summary>
    /// Review rate 1-5
    /// </summary>
    public int ReviewRate { get; set; }

    public string Review { get; set; } = null!;

    public DateTime CreatedAt { get; set; }

    public int CustomerId { get; set; }

    public virtual Customers Customer { get; set; } = null!;

    public virtual Products Product { get; set; } = null!;
}
