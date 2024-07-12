namespace Journey.Communication.Responses;
public class ResponseTripJson
{
    public Guid Id { get; set; }
    public string Destination { get; set; } = string.Empty;
    public DateTime StartsAt { get; set; }
    public DateTime EndsAt { get; set; }
}
