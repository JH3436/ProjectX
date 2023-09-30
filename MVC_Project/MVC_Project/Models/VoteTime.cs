using System;
using System.Collections.Generic;

namespace MVC_Project.Models;

public partial class VoteTime
{
    public int VoteId { get; set; }

    public int? ActivityId { get; set; }

    public DateTime? StartDate { get; set; }

    public int? VoteCount { get; set; }

    public virtual MyActivity? Activity { get; set; }
}
