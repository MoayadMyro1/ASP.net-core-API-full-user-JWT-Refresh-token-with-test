using System;
using System.Collections.Generic;

namespace DriverApi.Models;

public partial class Notification
{
    public int Id { get; set; }

    public string UserId { get; set; } = null!;

    public string? NotificationTemplateId { get; set; }

    public string? Title { get; set; }

    public string? Message { get; set; }

    public int? NotificationType { get; set; }

    public bool? IsRead { get; set; }

    public DateTime? SentAt { get; set; }

    public virtual Users User { get; set; } = null!;
}
