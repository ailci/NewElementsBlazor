using Microsoft.AspNetCore.Components.Forms;
using Shared.ViewModels;

namespace BlazorUI.Components.Pages.Author;
public partial class AuthorNew
{
    public AuthorForCreateViewModel? AuthorForCreateVm { get; set; }

    private Task HandleValidSubmit(EditContext arg)
    {
        return Task.CompletedTask;
    }
}
