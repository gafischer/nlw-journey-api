namespace Journey.Infrastructure.Entities;
public class Trip
{
    public Guid Id { get; set; } = Guid.NewGuid();
    public string Destination { get; set; } = string.Empty;
    public DateTime StartsAt { get; set; }
    public DateTime EndsAt { get; set; }
    public bool IsConfirmed { get; set; }

    public ICollection<Activity> Activities { get; set; } = [];
    public ICollection<Link> Links { get; set; } = [];
    public ICollection<Participant> Participants { get; set; } = new List<Participant>();
}
