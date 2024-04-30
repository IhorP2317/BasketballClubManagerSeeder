using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace BasketballClubManagerSeeder.Models;

public partial class ExoftBasketballClubContext : DbContext
{
    public ExoftBasketballClubContext()
    {
    }

    public ExoftBasketballClubContext(DbContextOptions<ExoftBasketballClubContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Award> Awards { get; set; }

    public virtual DbSet<Coach> Coaches { get; set; }

    public virtual DbSet<CoachExperience> CoachExperiences { get; set; }

    public virtual DbSet<Match> Matches { get; set; }

    public virtual DbSet<Order> Orders { get; set; }

    public virtual DbSet<Player> Players { get; set; }

    public virtual DbSet<PlayerExperience> PlayerExperiences { get; set; }

    public virtual DbSet<Statistic> Statistics { get; set; }

    public virtual DbSet<Team> Teams { get; set; }

    public virtual DbSet<Ticket> Tickets { get; set; }

    public virtual DbSet<User> Users { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Server=DESKTOP-9V0LQEE\\SQLEXPRESS;Database=ExoftBasketballClub;Trusted_Connection=True;TrustServerCertificate=True;");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Award>(entity =>
        {
            entity.HasIndex(e => new { e.Name, e.Date }, "IX_Awards_Name_Date").IsUnique();

            entity.Property(e => e.Id).HasDefaultValueSql("(newid())");

            entity.HasMany(d => d.CoachExperiences).WithMany(p => p.Awards)
                .UsingEntity<Dictionary<string, object>>(
                    "CoachAward",
                    r => r.HasOne<CoachExperience>().WithMany().HasForeignKey("CoachExperienceId"),
                    l => l.HasOne<Award>().WithMany().HasForeignKey("AwardId"),
                    j =>
                    {
                        j.HasKey("AwardId", "CoachExperienceId");
                        j.ToTable("CoachAwards");
                        j.HasIndex(new[] { "CoachExperienceId" }, "IX_CoachAwards_CoachExperienceId");
                    });

            entity.HasMany(d => d.PlayerExperiences).WithMany(p => p.Awards)
                .UsingEntity<Dictionary<string, object>>(
                    "PlayerAward",
                    r => r.HasOne<PlayerExperience>().WithMany().HasForeignKey("PlayerExperienceId"),
                    l => l.HasOne<Award>().WithMany().HasForeignKey("AwardId"),
                    j =>
                    {
                        j.HasKey("AwardId", "PlayerExperienceId");
                        j.ToTable("PlayerAwards");
                        j.HasIndex(new[] { "PlayerExperienceId" }, "IX_PlayerAwards_PlayerExperienceId");
                    });
        });

        modelBuilder.Entity<Coach>(entity =>
        {
            entity.HasIndex(e => e.TeamId, "IX_Coaches_TeamId");

            entity.Property(e => e.Id).HasDefaultValueSql("(newid())");

            entity.HasOne(d => d.Team).WithMany(p => p.Coaches)
                .HasForeignKey(d => d.TeamId)
                .OnDelete(DeleteBehavior.SetNull);
        });

        modelBuilder.Entity<CoachExperience>(entity =>
        {
            entity.HasIndex(e => e.CoachId, "IX_CoachExperiences_CoachId");

            entity.HasIndex(e => e.TeamId, "IX_CoachExperiences_TeamId");

            entity.Property(e => e.Id).HasDefaultValueSql("(newid())");

            entity.HasOne(d => d.Coach).WithMany(p => p.CoachExperiences).HasForeignKey(d => d.CoachId);

            entity.HasOne(d => d.Team).WithMany(p => p.CoachExperiences).HasForeignKey(d => d.TeamId);
        });

        modelBuilder.Entity<Match>(entity =>
        {
            entity.HasIndex(e => e.AwayTeamId, "IX_Matches_AwayTeamId");

            entity.HasIndex(e => e.HomeTeamId, "IX_Matches_HomeTeamId");

            entity.Property(e => e.Id).HasDefaultValueSql("(newid())");

            entity.HasOne(d => d.AwayTeam).WithMany(p => p.MatchAwayTeams).HasForeignKey(d => d.AwayTeamId);

            entity.HasOne(d => d.HomeTeam).WithMany(p => p.MatchHomeTeams)
                .HasForeignKey(d => d.HomeTeamId)
                .OnDelete(DeleteBehavior.ClientSetNull);
        });

        modelBuilder.Entity<Order>(entity =>
        {
            entity.HasIndex(e => e.UserId, "IX_Orders_UserId");

            entity.Property(e => e.Id).HasDefaultValueSql("(newid())");
            entity.Property(e => e.TotalPrice).HasColumnType("decimal(10, 2)");

            entity.HasOne(d => d.User).WithMany(p => p.Orders).HasForeignKey(d => d.UserId);
        });

        modelBuilder.Entity<Player>(entity =>
        {
            entity.HasIndex(e => new { e.TeamId, e.JerseyNumber }, "IX_Players_TeamId_JerseyNumber")
                .IsUnique()
                .HasFilter("([TeamId] IS NOT NULL)");

            entity.Property(e => e.Id).HasDefaultValueSql("(newid())");
            entity.Property(e => e.Height).HasColumnType("decimal(5, 2)");
            entity.Property(e => e.Weight).HasColumnType("decimal(5, 2)");

            entity.HasOne(d => d.Team).WithMany(p => p.Players)
                .HasForeignKey(d => d.TeamId)
                .OnDelete(DeleteBehavior.SetNull);
        });

        modelBuilder.Entity<PlayerExperience>(entity =>
        {
            entity.HasIndex(e => e.PlayerId, "IX_PlayerExperiences_PlayerId");

            entity.HasIndex(e => e.TeamId, "IX_PlayerExperiences_TeamId");

            entity.Property(e => e.Id).HasDefaultValueSql("(newid())");

            entity.HasOne(d => d.Player).WithMany(p => p.PlayerExperiences).HasForeignKey(d => d.PlayerId);

            entity.HasOne(d => d.Team).WithMany(p => p.PlayerExperiences).HasForeignKey(d => d.TeamId);
        });

        modelBuilder.Entity<Statistic>(entity =>
        {
            entity.HasKey(e => new { e.MatchId, e.PlayerExperienceId, e.TimeUnit });

            entity.HasIndex(e => e.PlayerExperienceId, "IX_Statistics_PlayerExperienceId");

            entity.HasOne(d => d.Match).WithMany(p => p.Statistics).HasForeignKey(d => d.MatchId);

            entity.HasOne(d => d.PlayerExperience).WithMany(p => p.Statistics)
                .HasForeignKey(d => d.PlayerExperienceId)
                .OnDelete(DeleteBehavior.ClientSetNull);
        });

        modelBuilder.Entity<Team>(entity =>
        {
            entity.HasIndex(e => e.Name, "IX_Teams_Name").IsUnique();

            entity.Property(e => e.Id).HasDefaultValueSql("(newid())");
        });

        modelBuilder.Entity<Ticket>(entity =>
        {
            entity.HasIndex(e => e.MatchId, "IX_Tickets_MatchId");

            entity.HasIndex(e => e.OrderId, "IX_Tickets_OrderId");

            entity.Property(e => e.Id).HasDefaultValueSql("(newid())");
            entity.Property(e => e.Price).HasColumnType("decimal(10, 2)");

            entity.HasOne(d => d.Match).WithMany(p => p.Tickets).HasForeignKey(d => d.MatchId);

            entity.HasOne(d => d.Order).WithMany(p => p.Tickets)
                .HasForeignKey(d => d.OrderId)
                .OnDelete(DeleteBehavior.Cascade);
        });

        modelBuilder.Entity<User>(entity =>
        {
            entity.HasIndex(e => e.Email, "IX_Users_Email").IsUnique();

            entity.Property(e => e.Id).HasDefaultValueSql("(newid())");
            entity.Property(e => e.Balance).HasColumnType("decimal(10, 2)");
            entity.Property(e => e.FirstName).HasDefaultValue("");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
