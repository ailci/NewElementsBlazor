using Logging;
using Microsoft.AspNetCore.Components;
using Services;
using Shared.ViewModels;

namespace BlazorUI.Components.Pages.Author;
public partial class Overview
{
    [Inject] public ILoggerManager Logger { get; set; } = null!;
    [Inject] public IQotdService QotdService { get; set; } = null!;
    public IEnumerable<AuthorViewModel>? AuthorsVm { get; set; }

    protected override async Task OnInitializedAsync()
    {
        await GetAuthors();
    }

    private async Task GetAuthors()
    {
        AuthorsVm = (await QotdService.GetAuthorsAsync()).OrderBy(c => c.Name);
    }

    private Task DeleteAuthor(Guid authorId)
    {
        Logger.LogInformation($"Autor mit der Id: {authorId} zum Löschen ausgewählt...");

        //TODO: Autor löschen => Service erweitern
    }
}
