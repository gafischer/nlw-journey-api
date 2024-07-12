namespace Journey.Infrastructure.Entities;

public class Link
{
    public Guid Id { get; set; }
    public string Title { get; set; } = string.Empty;
    public string Url { get; set; } = string.Empty;
    public Guid TripId { get; set; }

    public virtual Trip Trip { get; set; } = null!;
}
