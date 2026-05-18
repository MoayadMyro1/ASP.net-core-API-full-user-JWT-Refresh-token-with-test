using System;
using System.Collections.Generic;

namespace DriverApi.Models;

public partial class DriverSubscription
{
    public int Id { get; set; }

    public int DriverId { get; set; }

    public string? Subttxt { get; set; }

    public int SubscriptionId { get; set; }

    public DateTime StartDate { get; set; }

    public DateTime EndDate { get; set; }

    public int SubscriptionStatus { get; set; }

    public bool? AutoRenew { get; set; }

    public DateTime CreatedAt { get; set; }

    public virtual Driver Driver { get; set; } = null!;

    public virtual ICollection<Payment> Payments { get; set; } = new List<Payment>();
}
