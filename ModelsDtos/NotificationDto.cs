using System;
using System.Collections.Generic;

namespace DriverApi.ModelsDtos;

public partial class NotificationDto
{
    public int Id { get; set; }

    public int UserId { get; set; }

    public string? NotificationTemplateId { get; set; }

    public string? Title { get; set; }

    public string? Message { get; set; }

    public int? NotificationType { get; set; }

    public bool? IsRead { get; set; }

    public DateTime? SentAt { get; set; }

    public virtual UserResponseDto User { get; set; } = null!;
}
