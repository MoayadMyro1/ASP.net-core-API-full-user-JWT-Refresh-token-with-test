using System;
using System.Collections.Generic;

namespace DriverApi.Models;

public partial class DriverLocation
{
    public int Locid { get; set; }

    public decimal Latitude { get; set; }

    public decimal Longitude { get; set; }

    public decimal? Heading { get; set; }

    public decimal? Speed { get; set; }

    public DateTime LastUpdatedAt { get; set; }

    public int? DriverId { get; set; }
}
