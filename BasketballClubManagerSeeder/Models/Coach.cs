using System;
using System.Collections.Generic;

namespace BasketballClubManagerSeeder.Models;

public partial class Coach
{
    public Guid Id { get; set; }

    public string LastName { get; set; } = null!;

    public string FirstName { get; set; } = null!;

    public DateOnly DateOfBirth { get; set; }

    public string Country { get; set; } = null!;

    public int CoachStatus { get; set; }

    public Guid? TeamId { get; set; }

    public int Specialty { get; set; }

    public DateTimeOffset CreatedTime { get; set; }

    public DateTimeOffset? ModifiedTime { get; set; }

    public Guid CreatedById { get; set; }

    public Guid? ModifiedById { get; set; }

    public string? PhotoPath { get; set; }

    public virtual ICollection<CoachExperience> CoachExperiences { get; set; } = new List<CoachExperience>();

    public virtual Team? Team { get; set; }
}
