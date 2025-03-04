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
}