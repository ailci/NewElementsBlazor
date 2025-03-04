using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.ViewModels;

public class AuthorViewModel
{
    public Guid Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public DateOnly? BirthDate { get; set; }
    public byte[]? Photo { get; set; }
    public string? PhotoMimeType { get; set; }
    public IList<QuoteViewModel> Quotes { get; set; } = [];
}