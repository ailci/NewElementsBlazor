using Domain.Entities;
using Microsoft.AspNetCore.Components;
using Persistence;
using Shared.ViewModels;

namespace BlazorUI.Components.Pages;
public partial class Home
{
    [Inject] public QotdContext QotdContext { get; set; } = null!;

    public QuoteOfTheDayViewModel? QotdViewModel { get; set; }

    protected override void OnInitialized()
    {
        var test = QotdContext.Authors.ToList();

        QotdViewModel = new QuoteOfTheDayViewModel
        {
            AuthorName = "Ich",
            AuthorDescription = "Dozent",
            AuthorBirthDate = new DateOnly(2000,01,01),
            QuoteText = "Larum lierum Löffelstiel"
        };
    }
}
