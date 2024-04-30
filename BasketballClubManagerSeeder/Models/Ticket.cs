using System;
using System.Collections.Generic;

namespace BasketballClubManagerSeeder.Models;

public partial class Ticket
{
    public Guid Id { get; set; }

    public Guid MatchId { get; set; }

    public int Row { get; set; }

    public int Seat { get; set; }

    public Guid? OrderId { get; set; }

    public decimal Price { get; set; }

    public DateTimeOffset CreatedTime { get; set; }

    public DateTimeOffset? ModifiedTime { get; set; }

    public Guid CreatedById { get; set; }

    public Guid? ModifiedById { get; set; }

    public int Section { get; set; }

    public virtual Match Match { get; set; } = null!;

    public virtual Order? Order { get; set; }
}
