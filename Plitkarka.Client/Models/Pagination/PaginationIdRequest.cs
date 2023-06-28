namespace Plitkarka.Client.Models;

public record PaginationIdRequest : PaginationRequest
{
    public Guid Id { get; set; } = Guid.Empty;
}
