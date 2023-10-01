using System;
using System.Collections.Generic;

namespace LUN_MVC_GROUPS.Models;

public partial class Contact
{
    public int FormId { get; set; }

    public string SenderName { get; set; } = null!;

    public string Email { get; set; } = null!;

    public string Phone { get; set; } = null!;

    public string EmailTitle { get; set; } = null!;

    public string FormContent { get; set; } = null!;

    public DateTime? SendTime { get; set; }
}
