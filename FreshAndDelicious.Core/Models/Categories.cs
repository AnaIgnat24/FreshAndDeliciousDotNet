using System;
using System.Collections.Generic;

namespace FreshAndDelicious.Core.Models;

public partial class Categories
{
    public int Id { get; set; }

    public string CategoryName { get; set; } = null!;

    public string CategoryDescription { get; set; } = null!;

    public virtual ICollection<Products> Products { get; set; } = new List<Products>();
}
