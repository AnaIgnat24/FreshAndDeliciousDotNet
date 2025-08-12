using System;
using System.Collections.Generic;

namespace FreshAndDelicious.Core.Models;

public partial class Shippers
{
    public int Id { get; set; }

    public string ShipperName { get; set; } = null!;

    public string ContactName { get; set; } = null!;

    public string Address { get; set; } = null!;

    public string City { get; set; } = null!;

    public string PostalCode { get; set; } = null!;

    public string Country { get; set; } = null!;

    public string Phone { get; set; } = null!;

    public string ShippedValue { get; set; } = null!;

    public virtual ICollection<Orders> Orders { get; set; } = new List<Orders>();
}
