using System;
using System.Collections.Generic;

namespace DriverApi.ModelsDtos;

public partial class DriverLocationDto
{
    public decimal Latitude { get; set; }

    public decimal Longitude { get; set; }

    public decimal? Heading { get; set; }

    public decimal? Speed { get; set; }
}
