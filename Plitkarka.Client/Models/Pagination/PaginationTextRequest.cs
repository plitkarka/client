namespace Plitkarka.Client.Models;

public record PaginationTextRequest : PaginationRequest
{
    public string Filter { get; set; } = string.Empty;
}
