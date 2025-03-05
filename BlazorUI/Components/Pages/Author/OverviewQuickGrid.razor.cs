using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.QuickGrid;
using Services;
using Shared.ViewModels;

namespace BlazorUI.Components.Pages.Author;
public partial class OverviewQuickGrid
{
    [Inject] public IQotdService QotdService { get; set; } = null!;
    public IQueryable<AuthorViewModel>? AuthorsVm { get; set; }
    private readonly PaginationState _pagination = new() { ItemsPerPage = 5 };

    string _nameFilter = string.Empty;

    IQueryable<AuthorViewModel> FilteredAuthors =>
        !string.IsNullOrEmpty(_nameFilter)
            ? AuthorsVm.Where(c => c.Name.Contains(_nameFilter, StringComparison.CurrentCultureIgnoreCase))
            : AuthorsVm;

    protected override async Task OnInitializedAsync()
    {
        await GetAuthors();
    }

    public async Task GetAuthors()
    {
        AuthorsVm = (await QotdService.GetAuthorsAsync()).AsQueryable();
    }

    private async Task DeleteAuthor(Guid authorId)
    {
        var deletedAuthor = await QotdService.DeleteAuthorAsync(authorId);

        if (deletedAuthor is not null)
        {
            await GetAuthors();
        }
        else
        {
        }
    }
}
