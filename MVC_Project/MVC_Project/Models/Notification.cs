﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
#nullable disable
using System;
using System.Collections.Generic;

namespace MVC_Project.Models;

public partial class Notification
{
    public int NotificationID { get; set; }

    public int UserID { get; set; }

    public string NotificationContent { get; set; }

    public bool IsRead { get; set; }

    public DateTime? NotificationDate { get; set; }

    public string NotificationType { get; set; } // 添加通知類型屬性

    public virtual Member User { get; set; }
}