using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Shared.ViewModels;

namespace Services;

public interface IQotdService
{
    Task<QuoteOfTheDayViewModel> GetQuoteOfTheDayAsync();
    Task<IEnumerable<AuthorViewModel>> GetAuthorsAsync();
    Task<AuthorViewModel> DeleteAuthorAsync(Guid authorId);
}