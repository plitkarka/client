namespace Plitkarka.Client.Models;

public class PaginationTextRequest : PaginationRequest
{
    public string Filter { get; set; } = string.Empty;
}
