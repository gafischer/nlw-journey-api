namespace Journey.Communication.Requests;

public class RequestUpdateTripJson
{
    public string Destination { get; set; } = string.Empty;
    public DateTime StartsAt { get; set; }
    public DateTime EndsAt { get; set; }
}
