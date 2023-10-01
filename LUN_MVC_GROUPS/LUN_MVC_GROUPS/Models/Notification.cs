using System;
using System.Collections.Generic;

namespace LUN_MVC_GROUPS.Models;

public partial class Notification
{
    public int NotificationId { get; set; }

    public int UserId { get; set; }

    public string NotificationContent { get; set; } = null!;

    public bool IsRead { get; set; }

    public DateTime? NotificationDate { get; set; }

    public virtual Member User { get; set; } = null!;
}
