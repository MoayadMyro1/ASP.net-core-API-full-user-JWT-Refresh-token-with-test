using System;
using System.Collections.Generic;

namespace DriverApi.ModelsDtos;

public partial class OrderDto
{
    public int Id { get; set; }

    public int UsrId { get; set; }

    public int? DrId { get; set; }

    public string? OrderNumber { get; set; }

    public int OrderStatus { get; set; }

    public string? PickupAddress { get; set; }

    public decimal? PickupLatitude { get; set; }

    public decimal? PickupLongitude { get; set; }

    public string? DropoffAddress { get; set; }

    public decimal? DropoffLatitude { get; set; }

    public decimal? DropoffLongitude { get; set; }

    public decimal? DistanceKm { get; set; }

    public decimal? EstimatedPrice { get; set; }

    public decimal? FinalPrice { get; set; }

    public string? Notes { get; set; }

    public DateTime? ScheduledAt { get; set; }

    public DateTime CreatedAt { get; set; }

    public DateTime? UpdatedAt { get; set; }

    public virtual DriverDto? Dr { get; set; }

    public virtual ICollection<OrderTrackingDto> OrderTrackings { get; set; } = new List<OrderTrackingDto>();

    public virtual UserResponseDto Usr { get; set; } = null!;
}
