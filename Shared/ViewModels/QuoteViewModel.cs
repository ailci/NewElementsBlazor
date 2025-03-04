namespace Shared.ViewModels;

public class QuoteViewModel
{
    public Guid Id { get; set; }
    public required string QuoteText { get; set; }
    public string? AuthorName { get; set; }
    public Guid AuthorId { get; set; }
}