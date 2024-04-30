using System;
using System.Collections.Generic;

namespace BasketballClubManagerSeeder.Models;

public partial class PlayerExperience
{
    public Guid Id { get; set; }

    public Guid PlayerId { get; set; }

    public Guid TeamId { get; set; }

    public DateOnly StartDate { get; set; }

    public DateOnly? EndDate { get; set; }

    public DateTimeOffset CreatedTime { get; set; }

    public DateTimeOffset? ModifiedTime { get; set; }

    public Guid CreatedById { get; set; }

    public Guid? ModifiedById { get; set; }

    public virtual Player Player { get; set; } = null!;

    public virtual ICollection<Statistic> Statistics { get; set; } = new List<Statistic>();

    public virtual Team Team { get; set; } = null!;

    public virtual ICollection<Award> Awards { get; set; } = new List<Award>();
}
