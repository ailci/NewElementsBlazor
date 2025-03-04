using Microsoft.AspNetCore.Components;
using Shared.ViewModels;

namespace BlazorUI.Components.Author;
public partial class AuthorTable
{
    [Parameter] public IEnumerable<AuthorViewModel>? AuthorViewModels { get; set; }
    [Parameter] public EventCallback<Guid> OnAuthorDelete { get; set; }

    private async Task ShowConfirmationDialog(Guid authorId)
    {
        //TODO: Abfrage ob man wirklich löschen möchte
        await OnAuthorDelete.InvokeAsync(authorId); //Ereignis ausgelöst
    }
}
