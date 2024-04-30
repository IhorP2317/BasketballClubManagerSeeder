using System;
using System.Collections.Generic;

namespace BasketballClubManagerSeeder.Models;

public partial class Statistic
{
    public Guid MatchId { get; set; }

    public Guid PlayerExperienceId { get; set; }

    public int TimeUnit { get; set; }

    public int OnePointShotHitCount { get; set; }

    public int OnePointShotMissCount { get; set; }

    public int TwoPointShotHitCount { get; set; }

    public int TwoPointShotMissCount { get; set; }

    public int ThreePointShotHitCount { get; set; }

    public int ThreePointShotMissCount { get; set; }

    public int AssistCount { get; set; }

    public int OffensiveReboundCount { get; set; }

    public int DefensiveReboundCount { get; set; }

    public int StealCount { get; set; }

    public int BlockCount { get; set; }

    public int TurnoverCount { get; set; }

    public TimeOnly CourtTime { get; set; }

    public virtual Match Match { get; set; } = null!;

    public virtual PlayerExperience PlayerExperience { get; set; } = null!;
}
