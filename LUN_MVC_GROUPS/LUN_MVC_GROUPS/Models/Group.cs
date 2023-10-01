using System;
using System.Collections.Generic;

namespace LUN_MVC_GROUPS.Models;

public partial class Group
{
    public int GroupId { get; set; }

    public string? GroupName { get; set; }

    public string? GroupCategory { get; set; }

    public string? GroupContent { get; set; }

    public int? MinAttendee { get; set; }

    public int? MaxAttendee { get; set; }

    public DateTime? StartDate { get; set; }

    public DateTime? EndDate { get; set; }

    public int? Organizer { get; set; }

    public int? OriginalActivityId { get; set; }

    public virtual Member? OrganizerNavigation { get; set; }

    public virtual MyActivity? OriginalActivity { get; set; }

    public virtual ICollection<Photo> Photos { get; set; } = new List<Photo>();

    public virtual ICollection<Registration> Registrations { get; set; } = new List<Registration>();
}
