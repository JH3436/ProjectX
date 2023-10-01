using System;
using System.Collections.Generic;

namespace LUN_MVC_GROUPS.Models;

public partial class VoteRecord
{
    public int RecordId { get; set; }

    public int? UserId { get; set; }

    public int? ActivityId { get; set; }

    public DateTime? VoteResult { get; set; }

    public virtual MyActivity? Activity { get; set; }

    public virtual Member? User { get; set; }
}
