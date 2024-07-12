namespace Journey.Communication.Responses;
public class ResponseParticipantJson
{
    public Guid Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public bool IsConfirmed { get; set; }
}
