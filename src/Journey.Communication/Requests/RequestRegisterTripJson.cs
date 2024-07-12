namespace Journey.Communication.Requests;
public class RequestRegisterTripJson
{
    public string Destination { get; set; } = string.Empty;
    public string OwnerName { get; set; } = string.Empty;
    public string OwnerEmail { get; set; } = string.Empty;
    public DateTime StartsAt { get; set; }
    public DateTime EndsAt { get; set; }
    public IList<string> EmailsToInvite { get; set; } = [];

}
