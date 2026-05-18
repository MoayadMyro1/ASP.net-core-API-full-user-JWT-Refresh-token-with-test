
using Microsoft.AspNetCore.Identity;

namespace DriverApi.Models;

public partial class Users : IdentityUser<string>
{
    public override string Id { get; set; } = Guid.NewGuid().ToString();
    public int Uid { get; set; }

    public override string? UserName { get; set; } = null!;

    public override string? Email { get; set; } = null!;

    public override string? PhoneNumber { get; set; } = null!;

    public override string? PasswordHash { get; set; } = null!;

    public string? ProfileImageUrl { get; set; }

    public bool IsActive { get; set; }

    public bool IsDeleted { get; set; }

    public DateTime? LastLoginAt { get; set; }

    public DateTime CreatedAt { get; set; }

    public DateTime? UpdatedAt { get; set; }

    public virtual ICollection<Notification> Notifications { get; set; } = new List<Notification>();

    public virtual ICollection<Order> Orders { get; set; } = new List<Order>();
}
