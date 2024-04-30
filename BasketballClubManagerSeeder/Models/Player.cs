using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace BasketballClubManagerSeeder.Models;

public partial class Player
{
    public Guid Id { get; set; }


    public string LastName { get; set; } = null!;

    public string FirstName { get; set; } = null!;

    public DateOnly DateOfBirth { get; set; }

    public string Country { get; set; } = null!;

    public decimal Height { get; set; }

    public decimal Weight { get; set; }

    public int Position { get; set; }

    public int JerseyNumber { get; set; }

    public Guid? TeamId { get; set; }

    public DateTimeOffset CreatedTime { get; set; }

    public DateTimeOffset? ModifiedTime { get; set; }

    public Guid CreatedById { get; set; }

    public Guid? ModifiedById { get; set; }

    public string? PhotoPath { get; set; }

    public virtual ICollection<PlayerExperience> PlayerExperiences { get; set; } = new List<PlayerExperience>();

    public virtual Team? Team { get; set; }
}
