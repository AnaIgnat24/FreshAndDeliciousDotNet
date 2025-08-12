using System;
using System.Collections.Generic;

namespace FreshAndDelicious.Core.Models;

public partial class Products
{
    public int Id { get; set; }

    public string ProductName { get; set; } = null!;

    public string ImagesNames { get; set; } = null!;

    public string QuantityUnit { get; set; } = null!;

    public int Price { get; set; }

    public int? DiscountId { get; set; }

    public int Stock { get; set; }

    public int CategoryId { get; set; }

    public int SupplierId { get; set; }

    public string ProductDescription { get; set; } = null!;

    public string ProductIngredients { get; set; } = null!;

    public string ProductNutritionInformation { get; set; } = null!;

    public string ProductHealthBenefits { get; set; } = null!;

    public virtual Categories Category { get; set; } = null!;

    public virtual Discounts? Discount { get; set; }

    public virtual ICollection<OrderDetails> OrderDetails { get; set; } = new List<OrderDetails>();

    public virtual ICollection<ProductReviews> ProductReviews { get; set; } = new List<ProductReviews>();

    public virtual Suppliers Supplier { get; set; } = null!;
}
