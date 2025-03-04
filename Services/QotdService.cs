﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Domain.Entities;
using Logging;
using Microsoft.EntityFrameworkCore;
using Persistence;
using Shared.Utilities;
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

    public async Task<AuthorViewModel?> DeleteAuthorAsync(Guid authorId)
    {
        logger.LogInformation($"{nameof(DeleteAuthorAsync)} mit AuthorId {authorId} aufgerufen...");
        await using var context = await contextFactory.CreateDbContextAsync();
        var author = await context.Authors.FindAsync(authorId);

        if (author is null) return null;

        context.Authors.Remove(author);
        await context.SaveChangesAsync();

        return mapper.Map<AuthorViewModel>(author);
    }

    public async Task<AuthorViewModel> AddAuthorAsync(AuthorForCreateViewModel authorForCreateViewModel)
    {
        logger.LogInformation($"{nameof(AddAuthorAsync)} mit AuthorForCreate {authorForCreateViewModel.LogAsJson()} aufgerufen...");
        await using var context = await contextFactory.CreateDbContextAsync();

        var authorEntity = mapper.Map<Author>(authorForCreateViewModel);

        //Falls Bild ausgewählt
        if (authorForCreateViewModel.Photo is not null)
        {
            var (fileContent, contentType) = await authorForCreateViewModel.Photo.GetFile();
            authorEntity.Photo = fileContent;
            authorEntity.PhotoMimeType = contentType;
        }

        await context.Authors.AddAsync(authorEntity);
        await context.SaveChangesAsync();

        return mapper.Map<AuthorViewModel>(authorEntity);
    }
}