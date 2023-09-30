using System;
using System.Collections.Generic;

namespace MVC_Project.Models;

public partial class Photo
{
    public int PhotoId { get; set; }

    public int? ActivityId { get; set; }

    public int? GroupId { get; set; }

    public string PhotoPath { get; set; } = null!;

    public virtual MyActivity? Activity { get; set; }

    public virtual Group? Group { get; set; }
}
