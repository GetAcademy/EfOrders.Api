using System;
using System.Collections.Generic;

namespace EfOrders.Api.Models;

public partial class Order
{
    public int Id { get; set; }

    public int CustomerId { get; set; }

    public DateTime CreatedUtc { get; set; }

    public decimal TotalAmount { get; set; }

    public virtual Customer Customer { get; set; } = null!;
}
