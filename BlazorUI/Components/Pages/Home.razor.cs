using Domain.Entities;
using Microsoft.AspNetCore.Components;
using Microsoft.EntityFrameworkCore;
using Persistence;
using Shared.ViewModels;

namespace BlazorUI.Components.Pages;
public partial class Home
{
    [Inject] public QotdContext QotdContext { get; set; } = null!;

    public QuoteOfTheDayViewModel? QotdViewModel { get; set; }

    protected override async Task OnInitializedAsync()
    {
        var quotes = await QotdContext.Quotes.Include(c => c.Author).ToListAsync();
        var random = new Random();
        var randomQuote = quotes[random.Next(quotes.Count)];

        QotdViewModel = new QuoteOfTheDayViewModel
        {
            AuthorName = randomQuote.Author?.Name,
            AuthorDescription = randomQuote.Author?.Description,
            AuthorBirthDate = randomQuote.Author?.BirthDate,
            QuoteText = randomQuote.QuoteText,
            AuthorPhoto = randomQuote.Author?.Photo,
            AuthorPhotoMimeType = randomQuote.Author?.PhotoMimeType
        };
    }
}
