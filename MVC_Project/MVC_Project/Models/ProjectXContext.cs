using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace MVC_Project.Models;

public partial class ProjectXContext : DbContext
{
    public ProjectXContext()
    {
    }

    public ProjectXContext(DbContextOptions<ProjectXContext> options)
        : base(options)
    {
    }

    public virtual DbSet<ActivityLike> ActivityLikes { get; set; }

    public virtual DbSet<Chat> Chats { get; set; }

    public virtual DbSet<Contact> Contacts { get; set; }

    public virtual DbSet<Group> Groups { get; set; }

    public virtual DbSet<Member> Members { get; set; }

    public virtual DbSet<MyActivity> MyActivities { get; set; }

    public virtual DbSet<Notification> Notifications { get; set; }

    public virtual DbSet<Photo> Photos { get; set; }

    public virtual DbSet<Registration> Registrations { get; set; }

    public virtual DbSet<VoteRecord> VoteRecords { get; set; }

    public virtual DbSet<VoteTime> VoteTimes { get; set; }


    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<ActivityLike>(entity =>
        {
            entity.HasKey(e => e.LikeRecordId).HasName("PK__Activity__E89F8EF8137BB018");

            entity.HasIndex(e => new { e.UserId, e.ActivityId }, "UQ_UserActivityLike").IsUnique();

            entity.Property(e => e.LikeRecordId).HasColumnName("LikeRecordID");
            entity.Property(e => e.ActivityId).HasColumnName("ActivityID");
            entity.Property(e => e.UserId).HasColumnName("UserID");

            entity.HasOne(d => d.Activity).WithMany(p => p.ActivityLikes)
                .HasForeignKey(d => d.ActivityId)
                .HasConstraintName("FK_ActivityID");

            entity.HasOne(d => d.User).WithMany(p => p.ActivityLikes)
                .HasForeignKey(d => d.UserId)
                .HasConstraintName("FK_UserID");
        });

        modelBuilder.Entity<Chat>(entity =>
        {
            entity.HasKey(e => e.ChatId).HasName("PK__Chat__A9FBE62642372D7C");

            entity.ToTable("Chat");

            entity.Property(e => e.ChatId).HasColumnName("ChatID");
            entity.Property(e => e.ActivityId).HasColumnName("ActivityID");
            entity.Property(e => e.ChatTime)
                .HasDefaultValueSql("(sysdatetime())")
                .HasColumnType("datetime");
            entity.Property(e => e.UserId).HasColumnName("UserID");

            entity.HasOne(d => d.Activity).WithMany(p => p.Chats)
                .HasForeignKey(d => d.ActivityId)
                .HasConstraintName("FK__Chat__ActivityID__4BAC3F29");

            entity.HasOne(d => d.ToWhomNavigation).WithMany(p => p.InverseToWhomNavigation)
                .HasForeignKey(d => d.ToWhom)
                .HasConstraintName("FK__Chat__ToWhom__4D94879B");

            entity.HasOne(d => d.User).WithMany(p => p.Chats)
                .HasForeignKey(d => d.UserId)
                .HasConstraintName("FK__Chat__UserID__4CA06362");
        });

        modelBuilder.Entity<Contact>(entity =>
        {
            entity.HasKey(e => e.FormId).HasName("PK__Contact__FB05B7BD205B7911");

            entity.ToTable("Contact");

            entity.Property(e => e.FormId).HasColumnName("FormID");
            entity.Property(e => e.Email).HasMaxLength(255);
            entity.Property(e => e.EmailTitle).HasMaxLength(255);
            entity.Property(e => e.Phone).HasMaxLength(20);
            entity.Property(e => e.SendTime)
                .HasDefaultValueSql("(sysdatetime())")
                .HasColumnType("datetime");
            entity.Property(e => e.SenderName).HasMaxLength(100);
        });

        modelBuilder.Entity<Group>(entity =>
        {
            entity.HasKey(e => e.GroupId).HasName("PK__Group__149AF30AE207B7A9");

            entity.ToTable("Group");

            entity.HasIndex(e => e.GroupName, "UQ__Group__6EFCD434F7154425").IsUnique();

            entity.Property(e => e.GroupId).HasColumnName("GroupID");
            entity.Property(e => e.EndDate).HasColumnType("date");
            entity.Property(e => e.GroupCategory).HasMaxLength(50);
            entity.Property(e => e.GroupName).HasMaxLength(255);
            entity.Property(e => e.OriginalActivityId).HasColumnName("OriginalActivityID");
            entity.Property(e => e.StartDate).HasColumnType("date");

            entity.HasOne(d => d.OrganizerNavigation).WithMany(p => p.Groups)
                .HasForeignKey(d => d.Organizer)
                .HasConstraintName("FK_Organizer");

            entity.HasOne(d => d.OriginalActivity).WithMany(p => p.Groups)
                .HasForeignKey(d => d.OriginalActivityId)
                .HasConstraintName("FK_OriginalActivity");
        });

        modelBuilder.Entity<Member>(entity =>
        {
            entity.HasKey(e => e.UserId).HasName("PK__Member__1788CCAC9FFE6197");

            entity.ToTable("Member");

            entity.HasIndex(e => e.Phone, "UQ__Member__5C7E359E2616115B").IsUnique();

            entity.HasIndex(e => e.Email, "UQ__Member__A9D1053408ADAFD7").IsUnique();

            entity.HasIndex(e => e.Account, "UQ__Member__B0C3AC46391593E6").IsUnique();

            entity.HasIndex(e => e.Nickname, "UQ__Member__CC6CD17EBC8840A0").IsUnique();

            entity.Property(e => e.UserId).HasColumnName("UserID");
            entity.Property(e => e.Account).HasMaxLength(50);
            entity.Property(e => e.Email).HasMaxLength(255);
            entity.Property(e => e.LoginMethod).HasMaxLength(50);
            entity.Property(e => e.Nickname).HasMaxLength(50);
            entity.Property(e => e.Password).HasMaxLength(255);
            entity.Property(e => e.Phone).HasMaxLength(20);
        });

        modelBuilder.Entity<MyActivity>(entity =>
        {
            entity.HasKey(e => e.ActivityId).HasName("PK__MyActivi__45F4A7F168FA9453");

            entity.ToTable("MyActivity");

            entity.HasIndex(e => e.ActivityName, "UQ__MyActivi__1DB4FDB3A61FCE48").IsUnique();

            entity.Property(e => e.ActivityId).HasColumnName("ActivityID");
            entity.Property(e => e.ActivityName).HasMaxLength(255);
            entity.Property(e => e.Category).HasMaxLength(255);
            entity.Property(e => e.ExpectedDepartureMonth).HasColumnType("date");
            entity.Property(e => e.Map).HasMaxLength(255);
            entity.Property(e => e.SuggestedAmount).HasColumnType("money");
            entity.Property(e => e.VoteDate).HasColumnType("datetime");
        });

        modelBuilder.Entity<Notification>(entity =>
        {
            entity.HasKey(e => e.NotificationId).HasName("PK__Notifica__20CF2E32414D2C87");

            entity.ToTable("Notification");

            entity.Property(e => e.NotificationId).HasColumnName("NotificationID");
            entity.Property(e => e.NotificationDate)
                .HasDefaultValueSql("(sysdatetime())")
                .HasColumnType("datetime");
            entity.Property(e => e.UserId).HasColumnName("UserID");

            entity.HasOne(d => d.User).WithMany(p => p.Notifications)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_User_Notification");
        });

        modelBuilder.Entity<Photo>(entity =>
        {
            entity.HasKey(e => e.PhotoId).HasName("PK__Photos__21B7B5822429BCE5");

            entity.Property(e => e.PhotoId).HasColumnName("PhotoID");
            entity.Property(e => e.ActivityId).HasColumnName("ActivityID");
            entity.Property(e => e.GroupId).HasColumnName("GroupID");
            entity.Property(e => e.PhotoPath).HasMaxLength(255);

            entity.HasOne(d => d.Activity).WithMany(p => p.Photos)
                .HasForeignKey(d => d.ActivityId)
                .HasConstraintName("FK_Activity_Photo");

            entity.HasOne(d => d.Group).WithMany(p => p.Photos)
                .HasForeignKey(d => d.GroupId)
                .HasConstraintName("FK_Group_Photo");
        });

        modelBuilder.Entity<Registration>(entity =>
        {
            entity.HasKey(e => e.RegistrationId).HasName("PK__Registra__6EF588309B7C90D5");

            entity.ToTable("Registration");

            entity.Property(e => e.RegistrationId).HasColumnName("RegistrationID");
            entity.Property(e => e.GroupId).HasColumnName("GroupID");
            entity.Property(e => e.ParticipantId).HasColumnName("ParticipantID");

            entity.HasOne(d => d.Group).WithMany(p => p.Registrations)
                .HasForeignKey(d => d.GroupId)
                .HasConstraintName("FK_Registration_Group");

            entity.HasOne(d => d.Participant).WithMany(p => p.Registrations)
                .HasForeignKey(d => d.ParticipantId)
                .HasConstraintName("FK_Registration_User");
        });

        modelBuilder.Entity<VoteRecord>(entity =>
        {
            entity.HasKey(e => e.RecordId).HasName("PK__VoteReco__FBDF78C9FC4C3E63");

            entity.ToTable("VoteRecord");

            entity.Property(e => e.RecordId).HasColumnName("RecordID");
            entity.Property(e => e.ActivityId).HasColumnName("ActivityID");
            entity.Property(e => e.UserId).HasColumnName("UserID");
            entity.Property(e => e.VoteResult).HasColumnType("date");

            entity.HasOne(d => d.Activity).WithMany(p => p.VoteRecords)
                .HasForeignKey(d => d.ActivityId)
                .HasConstraintName("FK_VoteRecord_ActivityID");

            entity.HasOne(d => d.User).WithMany(p => p.VoteRecords)
                .HasForeignKey(d => d.UserId)
                .HasConstraintName("FK_VoteRecord_UserID");
        });

        modelBuilder.Entity<VoteTime>(entity =>
        {
            entity.HasKey(e => e.VoteId).HasName("PK__VoteTime__52F015E260A0CEFA");

            entity.ToTable("VoteTime");

            entity.Property(e => e.VoteId).HasColumnName("VoteID");
            entity.Property(e => e.ActivityId).HasColumnName("ActivityID");
            entity.Property(e => e.StartDate).HasColumnType("date");

            entity.HasOne(d => d.Activity).WithMany(p => p.VoteTimes)
                .HasForeignKey(d => d.ActivityId)
                .HasConstraintName("FK_VoteTime_Activity");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
