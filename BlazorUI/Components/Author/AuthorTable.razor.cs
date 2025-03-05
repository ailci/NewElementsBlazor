using ComponentsLibrary;
using ComponentsLibrary.Components;
using Domain.Entities;
using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using Shared.ViewModels;

namespace BlazorUI.Components.Author;
public partial class AuthorTable
{
    [Inject] public IJSRuntime JsRuntime { get; set; } = null!;
    [Inject] public DialogService DialogService { get; set; } = null!;
    [Parameter] public IEnumerable<AuthorViewModel>? AuthorViewModels { get; set; }
    [Parameter] public EventCallback<Guid> OnAuthorDelete { get; set; }
    private ConfirmDialog? _confirmDialogComponent; //Referenz zur Componente
    private Guid _authorIdToDelete;

    private async Task ShowConfirmationDialog(AuthorViewModel authorVm)
    {
        _authorIdToDelete = authorVm.Id;

        // 1. Version Klassik
        //if (await JsRuntime.InvokeAsync<bool>("confirm", $"Wollen Sie wirklich den Autor {authorVm.Name} löschen?"))
        //{
        //    await OnAuthorDelete.InvokeAsync(authorVm.Id); //Ereignis ausgelöst
        //}

        // 2. Version mit DialogService
        //if (await DialogService.ConfirmAsync($"Wollen Sie wirklich den Autor {authorVm.Name} löschen?"))
        //{
        //    await OnAuthorDelete.InvokeAsync(authorVm.Id); //Ereignis ausgelöst
        //}

        // 3. Version als Componente
        _confirmDialogComponent?.Show($"Wollen Sie wirklich den Autor {authorVm.Name} löschen?");
    }

    private async Task ConfirmDeleteClicked()
    {
        //TODO: Authorid übergeben
        await OnAuthorDelete.InvokeAsync(_authorIdToDelete);
    }
}
