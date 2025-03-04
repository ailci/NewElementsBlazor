using Microsoft.AspNetCore.Components;
using Shared.ViewModels;

namespace BlazorUI.Components.Author;
public partial class AuthorTable
{
    [Parameter] public IEnumerable<AuthorViewModel>? AuthorViewModels { get; set; }

    private Task ShowConfirmationDialog(Guid authorId)
    {
        
    }
}
