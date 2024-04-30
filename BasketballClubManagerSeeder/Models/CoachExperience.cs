using System;
using System.Collections.Generic;

namespace BasketballClubManagerSeeder.Models;

public partial class CoachExperience
{
    public Guid Id { get; set; }

    public Guid CoachId { get; set; }

    public Guid TeamId { get; set; }

    public DateOnly StartDate { get; set; }

    public DateOnly? EndDate { get; set; }

    public DateTimeOffset CreatedTime { get; set; }

    public DateTimeOffset? ModifiedTime { get; set; }

    public Guid CreatedById { get; set; }

    public Guid? ModifiedById { get; set; }

    public int Status { get; set; }

    public virtual Coach Coach { get; set; } = null!;

    public virtual Team Team { get; set; } = null!;

    public virtual ICollection<Award> Awards { get; set; } = new List<Award>();
}
