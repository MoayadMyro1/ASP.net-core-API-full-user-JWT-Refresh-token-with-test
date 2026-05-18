using System;
using System.Collections.Generic;

namespace DriverApi.Models;

public partial class SystemSetting
{
    public int Id { get; set; }

    public string? Settxt { get; set; }

    public string? Val { get; set; }

    public string? Desctxt { get; set; }
}
