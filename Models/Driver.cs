using System;
using System.Collections.Generic;

namespace DriverApi.Models;

public partial class Driver
{
    public int DriverId { get; set; }

    public int? UserId { get; set; }

    public int? VehicleId { get; set; }

    public string? LicenseNumber { get; set; }
    public string? Mobile { get; set; }
    public string? Email { get; set; }
    public string? City { get; set; }

    public int DriverStatus { get; set; }

    public bool IsOnline { get; set; }

    public bool IsAvailable { get; set; }

    public decimal? AverageRating { get; set; }

    public DateTime CreatedAt { get; set; }

    public DateTime? UpdatedAt { get; set; }

    public int? CurrentSubscriptionId { get; set; }

    public virtual ICollection<DriverSubscription> DriverSubscriptions { get; set; } = new List<DriverSubscription>();

    public virtual ICollection<Order> Orders { get; set; } = new List<Order>();
}
