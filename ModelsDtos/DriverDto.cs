using System;
using System.Collections.Generic;

namespace DriverApi.ModelsDtos;

public partial class DriverDto
{
    public string? LicenseNumber { get; set; }

    public int DriverStatus { get; set; }

    public bool IsOnline { get; set; }

    public bool IsAvailable { get; set; }

    public decimal? AverageRating { get; set; }

    public DateTime CreatedAt { get; set; }
    public string? Mobile { get; set; }
    public string? Email { get; set; }
    public string? City { get; set; }
}
