namespace Journey.Communication.Responses;

public class ResponseActivityJson
{
    public DateTime Date { get; set; }
    public IList<ResponseActivityItemJson> Activities { get; set; } = [];
}

public class ResponseActivityItemJson
{
    public Guid Id { get; set; }
    public string Title { get; set; } = string.Empty;
    public DateTime OccursAt { get; set; }
}
