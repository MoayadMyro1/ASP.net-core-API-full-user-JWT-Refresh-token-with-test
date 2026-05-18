using System;
using System.Collections.Generic;

namespace DriverApi.ModelsDtos;

public partial class UserResponseDto
{

    public string? UserId { get; set; }

    public string UserName { get; set; } = null!;

    public string Email { get; set; } = null!;

    public string PhoneNumber { get; set; } = null!;

    public DateTime CreatedAt { get; set; }

}
