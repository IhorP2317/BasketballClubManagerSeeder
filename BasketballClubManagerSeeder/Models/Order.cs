using System;
using System.Collections.Generic;

namespace BasketballClubManagerSeeder.Models;

public partial class Order
{
    public Guid Id { get; set; }

    public decimal TotalPrice { get; set; }

    public Guid UserId { get; set; }

    public DateTimeOffset CreatedTime { get; set; }

    public DateTimeOffset? ModifiedTime { get; set; }

    public Guid CreatedById { get; set; }

    public Guid? ModifiedById { get; set; }

    public virtual ICollection<Ticket> Tickets { get; set; } = new List<Ticket>();

    public virtual User User { get; set; } = null!;
}
