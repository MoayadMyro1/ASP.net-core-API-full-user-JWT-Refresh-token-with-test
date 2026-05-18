using System;
using System.Collections.Generic;

namespace DriverApi.ModelsDtos;

public partial class SystemSettingDto
{
    public int Id { get; set; }

    public string? Settxt { get; set; }

    public string? Val { get; set; }

    public string? Desctxt { get; set; }
}
