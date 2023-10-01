using System;
using System.Collections.Generic;

namespace LUN_MVC_GROUPS.Models;

public partial class VoteTime
{
    public int VoteId { get; set; }

    public int? ActivityId { get; set; }

    public DateTime? StartDate { get; set; }

    public int? VoteCount { get; set; }

    public virtual MyActivity? Activity { get; set; }
}
