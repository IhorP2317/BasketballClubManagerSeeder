using System;
using System.Collections.Generic;

namespace BasketballClubManagerSeeder.Models;

public partial class Match
{
    public Guid Id { get; set; }

    public string Location { get; set; } = null!;

    public DateTime? EndTime { get; set; }

    public Guid HomeTeamId { get; set; }

    public Guid AwayTeamId { get; set; }

    public DateTimeOffset CreatedTime { get; set; }

    public DateTimeOffset? ModifiedTime { get; set; }

    public Guid CreatedById { get; set; }

    public Guid? ModifiedById { get; set; }

    public int Status { get; set; }

    public DateTime StartTime { get; set; }

    public int? HomeTeamScore { get; set; }

    public int? AwayTeamScore { get; set; }

    public int RowCount { get; set; }

    public int SeatCount { get; set; }

    public int SectionCount { get; set; }

    public virtual Team AwayTeam { get; set; } = null!;

    public virtual Team HomeTeam { get; set; } = null!;

    public virtual ICollection<Statistic> Statistics { get; set; } = new List<Statistic>();

    public virtual ICollection<Ticket> Tickets { get; set; } = new List<Ticket>();
}
