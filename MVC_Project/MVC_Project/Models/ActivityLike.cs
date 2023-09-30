using System;
using System.Collections.Generic;

namespace MVC_Project.Models;

public partial class ActivityLike
{
    public int LikeRecordId { get; set; }

    public int? UserId { get; set; }

    public int? ActivityId { get; set; }

    public virtual MyActivity? Activity { get; set; }

    public virtual Member? User { get; set; }
}
