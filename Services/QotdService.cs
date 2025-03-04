using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Logging;
using Microsoft.EntityFrameworkCore;
using Persistence;
using Shared.ViewModels;

namespace Services;

public class QotdService(ILoggerManager logger, IDbContextFactory<QotdContext> contextFactory, IMapper mapper) 
    : IQotdService
{
    public async Task<QuoteOfTheDayViewModel> GetQuoteOfTheDayAsync()
    {
        logger.LogInformation($"{nameof(GetQuoteOfTheDayAsync)} wurde aufgerufen...");

        await using var context = await contextFactory.CreateDbContextAsync();

        var quotes = await context.Quotes.Include(c => c.Author).ToListAsync();
        var random = new Random();
        var randomQuote = quotes[random.Next(quotes.Count)];

        //return new QuoteOfTheDayViewModel
        //{
        //    AuthorName = randomQuote.Author?.Name,
        //    AuthorDescription = randomQuote.Author?.Description,
        //    AuthorBirthDate = randomQuote.Author?.BirthDate,
        //    QuoteText = randomQuote.QuoteText,
        //    AuthorPhoto = randomQuote.Author?.Photo,
        //    AuthorPhotoMimeType = randomQuote.Author?.PhotoMimeType
        //};

        return mapper.Map<QuoteOfTheDayViewModel>(randomQuote);
    }

    public async Task<IEnumerable<AuthorViewModel>> GetAuthorsAsync()
    {
        await using var context = await contextFactory.CreateDbContextAsync();
        var authors = await context.Authors.ToListAsync();

        return mapper.Map<IEnumerable<AuthorViewModel>>(authors);
    }

    public async Task<AuthorViewModel> DeleteAuthorAsync(Guid authorId)
    {
        throw new NotImplementedException();
    }
}