using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace LUN_MVC_GROUPS.Models;

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

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning .To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Data Source=LUN-1225\\LUN; Database=ProjectX;Trusted_Connection=True; TrustServerCertificate=true");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<ActivityLike>(entity =>
        {
            entity.HasKey(e => e.LikeRecordId).HasName("PK__Activity__E89F8EF849C4C1CE");

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
            entity.HasKey(e => e.ChatId).HasName("PK__Chat__A9FBE626AB47D4D4");

            entity.ToTable("Chat");

            entity.Property(e => e.ChatId).HasColumnName("ChatID");
            entity.Property(e => e.ActivityId).HasColumnName("ActivityID");
            entity.Property(e => e.ChatTime)
                .HasDefaultValueSql("(sysdatetime())")
                .HasColumnType("datetime");
            entity.Property(e => e.UserId).HasColumnName("UserID");

            entity.HasOne(d => d.Activity).WithMany(p => p.Chats)
                .HasForeignKey(d => d.ActivityId)
                .HasConstraintName("FK__Chat__ActivityID__70DDC3D8");

            entity.HasOne(d => d.ToWhomNavigation).WithMany(p => p.InverseToWhomNavigation)
                .HasForeignKey(d => d.ToWhom)
                .HasConstraintName("FK__Chat__ToWhom__72C60C4A");

            entity.HasOne(d => d.User).WithMany(p => p.Chats)
                .HasForeignKey(d => d.UserId)
                .HasConstraintName("FK__Chat__UserID__71D1E811");
        });

        modelBuilder.Entity<Contact>(entity =>
        {
            entity.HasKey(e => e.FormId).HasName("PK__Contact__FB05B7BDD825A0AD");

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
            entity.HasKey(e => e.GroupId).HasName("PK__Group__149AF30A44B36CBC");

            entity.ToTable("Group");

            entity.HasIndex(e => e.GroupName, "UQ__Group__6EFCD434AA8214FB").IsUnique();

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
            entity.HasKey(e => e.UserId).HasName("PK__Member__1788CCACBD2C2E00");

            entity.ToTable("Member");

            entity.HasIndex(e => e.Phone, "UQ__Member__5C7E359EEFF0258C").IsUnique();

            entity.HasIndex(e => e.Email, "UQ__Member__A9D105347B95E4E4").IsUnique();

            entity.HasIndex(e => e.Account, "UQ__Member__B0C3AC4605E60F9A").IsUnique();

            entity.HasIndex(e => e.Nickname, "UQ__Member__CC6CD17E1993484F").IsUnique();

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
            entity.HasKey(e => e.ActivityId).HasName("PK__MyActivi__45F4A7F1533EE17F");

            entity.ToTable("MyActivity");

            entity.HasIndex(e => e.ActivityName, "UQ__MyActivi__1DB4FDB34CB17FD4").IsUnique();

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
            entity.HasKey(e => e.NotificationId).HasName("PK__Notifica__20CF2E3259813EAB");

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
            entity.HasKey(e => e.PhotoId).HasName("PK__Photos__21B7B582D016399D");

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
            entity.HasKey(e => e.RegistrationId).HasName("PK__Registra__6EF588308E983AC3");

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
            entity.HasKey(e => e.RecordId).HasName("PK__VoteReco__FBDF78C90C2A5DDA");

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
            entity.HasKey(e => e.VoteId).HasName("PK__VoteTime__52F015E24329171F");

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
