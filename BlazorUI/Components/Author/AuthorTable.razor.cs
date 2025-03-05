using Domain.Entities;
using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using Shared.ViewModels;

namespace BlazorUI.Components.Author;
public partial class AuthorTable
{
    [Inject] public IJSRuntime JsRuntime { get; set; } = null!;
    [Parameter] public IEnumerable<AuthorViewModel>? AuthorViewModels { get; set; }
    [Parameter] public EventCallback<Guid> OnAuthorDelete { get; set; }

    private async Task ShowConfirmationDialog(AuthorViewModel authorVm)
    {
        // 1. Version Klassik
        if (await JsRuntime.InvokeAsync<bool>("confirm", $"Wollen Sie wirklich den Autor {authorVm.Name} l�schen?"))
        {
            await OnAuthorDelete.InvokeAsync(authorVm.Id); //Ereignis ausgel�st
        }


        
    }
}
