using System;
using System.Collections.Generic;

namespace LUN_MVC_GROUPS.Models;

public partial class Chat
{
    public int ChatId { get; set; }

    public int? ActivityId { get; set; }

    public int? UserId { get; set; }

    public string ChatContent { get; set; } = null!;

    public int? ToWhom { get; set; }

    public DateTime? ChatTime { get; set; }

    public virtual MyActivity? Activity { get; set; }

    public virtual ICollection<Chat> InverseToWhomNavigation { get; set; } = new List<Chat>();

    public virtual Chat? ToWhomNavigation { get; set; }

    public virtual Member? User { get; set; }
}
