using System;
using System.Collections.Generic;

namespace MVC_Project.Models;

public partial class Registration
{
    public int RegistrationId { get; set; }

    public int? GroupId { get; set; }

    public int? ParticipantId { get; set; }

    public virtual Group? Group { get; set; }

    public virtual Member? Participant { get; set; }
}
