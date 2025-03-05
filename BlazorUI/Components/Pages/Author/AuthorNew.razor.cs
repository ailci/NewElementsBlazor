using Logging;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;
using Services;
using Shared.Utilities;
using Shared.ViewModels;

namespace BlazorUI.Components.Pages.Author;
public partial class AuthorNew
{
    [Inject] public ILoggerManager Logger { get; set; } = null!;
    [Inject] public IQotdService QotdService { get; set; } = null!;
    [Inject] public NavigationManager NavManager { get; set; } = null!;
    public AuthorForCreateViewModel? AuthorForCreateVm { get; set; }
    public string? ErrorMessage { get; set; }

    protected override void OnInitialized() => AuthorForCreateVm ??= new();

    private async Task HandleValidSubmit(EditContext arg)
    {
        ErrorMessage = null;

        try
        {
            var addedAuthor = await QotdService.AddAuthorAsync(AuthorForCreateVm!);

            if (addedAuthor is not null)
            {
                NavManager.NavigateTo("/authors/overviewreloaded");
            }
        }
        catch (Exception ex)
        {
            Logger.LogError(ex.Message);
            ErrorMessage = $"Es ist ein Fehler aufgetreten. Bitte melden Sie sich bei Bastian. {ex.Message}";
        }
    }

    private void OnInputFileChange(InputFileChangeEventArgs e)
    {
        AuthorForCreateVm!.Photo = e.File;
        //Logger.LogInformation($"Ausgewähltes Bild: {e.LogAsJson()}");
    }
}
