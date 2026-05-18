using System;
using System.Collections.Generic;

namespace DriverApi.ModelsDtos;

public partial class Userlogin
{

    public string Email { get; set; } = null!;
    public string Password { get; set; } = null!;
    public DateTime? LastLoginAt { get; set; }

}
