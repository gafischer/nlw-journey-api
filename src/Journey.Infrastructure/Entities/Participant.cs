namespace Journey.Infrastructure.Entities;

public class Participant
{
    public Guid Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public bool IsConfirmed { get; set; } = false;
    public bool IsOwner { get; set; } = false;
    public Guid TripId { get; set; }

    public virtual Trip Trip { get; set; } = null!;
}
