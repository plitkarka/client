namespace Plitkarka.Client.Models;

public class PaginationIdRequest : PaginationRequest
{
    public Guid Id { get; set; } = Guid.Empty;
}
