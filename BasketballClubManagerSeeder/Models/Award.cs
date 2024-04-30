using System;
using System.Collections.Generic;

namespace BasketballClubManagerSeeder.Models;

public partial class Award
{
    public Guid Id { get; set; }

    public string Name { get; set; } = null!;

    public DateOnly Date { get; set; }

    public DateTimeOffset CreatedTime { get; set; }

    public DateTimeOffset? ModifiedTime { get; set; }

    public Guid CreatedById { get; set; }

    public Guid? ModifiedById { get; set; }

    public bool IsIndividualAward { get; set; }

    public string? PhotoPath { get; set; }

    public virtual ICollection<CoachExperience> CoachExperiences { get; set; } = new List<CoachExperience>();

    public virtual ICollection<PlayerExperience> PlayerExperiences { get; set; } = new List<PlayerExperience>();
}
