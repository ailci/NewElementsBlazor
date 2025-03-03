using Domain.Entities;
using Shared.ViewModels;

namespace BlazorUI.Components.Pages;
public partial class Home
{
    public QuoteOfTheDayViewModel? QotdViewModel { get; set; }

    protected override void OnInitialized()
    {
        QotdViewModel = new QuoteOfTheDayViewModel
        {
            AuthorName = "Ich",
            AuthorDescription = "Dozent",
            AuthorBirthDate = new DateOnly(2000,01,01),
            QuoteText = "Larum lierum Löffelstiel"
        };
    }
}
