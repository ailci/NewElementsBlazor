using Microsoft.AspNetCore.Components.Forms;
using Shared.ViewModels;

namespace BlazorUI.Components.Pages.Author;
public partial class AuthorNew
{
    public AuthorForCreateViewModel? AuthorForCreateVm { get; set; }

    protected override void OnInitialized() => AuthorForCreateVm ??= new();

    private Task HandleValidSubmit(EditContext arg)
    {
        return Task.CompletedTask;
    }
}
