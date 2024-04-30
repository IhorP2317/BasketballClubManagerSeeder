using System;
using System.Collections.Generic;

namespace BasketballClubManagerSeeder.Models;

public partial class Team
{
    public Guid Id { get; set; }

    public string Name { get; set; } = null!;

    public DateTimeOffset CreatedTime { get; set; }

    public DateTimeOffset? ModifiedTime { get; set; }

    public Guid CreatedById { get; set; }

    public Guid? ModifiedById { get; set; }

    public string? PhotoPath { get; set; }

    public virtual ICollection<CoachExperience> CoachExperiences { get; set; } = new List<CoachExperience>();

    public virtual ICollection<Coach> Coaches { get; set; } = new List<Coach>();

    public virtual ICollection<Match> MatchAwayTeams { get; set; } = new List<Match>();

    public virtual ICollection<Match> MatchHomeTeams { get; set; } = new List<Match>();

    public virtual ICollection<PlayerExperience> PlayerExperiences { get; set; } = new List<PlayerExperience>();

    public virtual ICollection<Player> Players { get; set; } = new List<Player>();
}
