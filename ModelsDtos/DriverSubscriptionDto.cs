using System;
using System.Collections.Generic;

namespace DriverApi.ModelsDtos;

public partial class DriverSubscriptionDto
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

    public virtual DriverDto Driver { get; set; } = null!;

    public virtual ICollection<PaymentDto> Payments { get; set; } = new List<PaymentDto>();
}
