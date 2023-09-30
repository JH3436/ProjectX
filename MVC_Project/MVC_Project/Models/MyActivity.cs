using System;
using System.Collections.Generic;

namespace MVC_Project.Models;

public partial class MyActivity
{
    public int ActivityId { get; set; }

    public string? ActivityName { get; set; }

    public string? Category { get; set; }

    public string? Map { get; set; }

    public decimal? SuggestedAmount { get; set; }

    public string? ActivityContent { get; set; }

    public int? MinAttendee { get; set; }

    public DateTime? VoteDate { get; set; }

    public DateTime? ExpectedDepartureMonth { get; set; }

    public virtual ICollection<ActivityLike> ActivityLikes { get; set; } = new List<ActivityLike>();

    public virtual ICollection<Chat> Chats { get; set; } = new List<Chat>();

    public virtual ICollection<Group> Groups { get; set; } = new List<Group>();

    public virtual ICollection<Photo> Photos { get; set; } = new List<Photo>();

    public virtual ICollection<VoteRecord> VoteRecords { get; set; } = new List<VoteRecord>();

    public virtual ICollection<VoteTime> VoteTimes { get; set; } = new List<VoteTime>();
}
