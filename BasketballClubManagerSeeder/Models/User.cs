using System;
using System.Collections.Generic;

namespace BasketballClubManagerSeeder.Models;

public partial class User
{
    public Guid Id { get; set; }

    public string LastName { get; set; } = null!;

    public string Email { get; set; } = null!;

    public decimal Balance { get; set; }

    public int Role { get; set; }

    public DateTimeOffset CreatedTime { get; set; }

    public DateTimeOffset? ModifiedTime { get; set; }

    public Guid CreatedById { get; set; }

    public Guid? ModifiedById { get; set; }

    public string? PhotoPath { get; set; }

    public bool EmailConfirmed { get; set; }

    public string FirstName { get; set; } = null!;

    public virtual ICollection<Order> Orders { get; set; } = new List<Order>();
}
