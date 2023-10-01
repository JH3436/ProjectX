using System;
using System.Collections.Generic;

namespace LUN_MVC_GROUPS.Models;

public partial class Member
{
    public int UserId { get; set; }

    public string? Nickname { get; set; }

    public string? Account { get; set; }

    public string? Password { get; set; }

    public string? Email { get; set; }

    public string? Phone { get; set; }

    public string? Intro { get; set; }

    public string? LoginMethod { get; set; }

    public virtual ICollection<ActivityLike> ActivityLikes { get; set; } = new List<ActivityLike>();

    public virtual ICollection<Chat> Chats { get; set; } = new List<Chat>();

    public virtual ICollection<Group> Groups { get; set; } = new List<Group>();

    public virtual ICollection<Notification> Notifications { get; set; } = new List<Notification>();

    public virtual ICollection<Registration> Registrations { get; set; } = new List<Registration>();

    public virtual ICollection<VoteRecord> VoteRecords { get; set; } = new List<VoteRecord>();
}
