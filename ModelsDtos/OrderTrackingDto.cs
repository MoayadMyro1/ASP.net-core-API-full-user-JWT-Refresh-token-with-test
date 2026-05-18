using System;
using System.Collections.Generic;

namespace DriverApi.ModelsDtos;

public partial class OrderTrackingDto
{
    public int Id { get; set; }

    public int OrderId { get; set; }

    public int Status { get; set; }

    public decimal? Latitude { get; set; }

    public decimal? Longitude { get; set; }

    public string? Notes { get; set; }

    public DateTime CreatedAt { get; set; }

}
