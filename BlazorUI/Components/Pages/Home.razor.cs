using Domain.Entities;
using Microsoft.AspNetCore.Components;
using Microsoft.EntityFrameworkCore;
using Persistence;
using Services;
using Shared.ViewModels;

namespace BlazorUI.Components.Pages;

public partial class Home
{
    [Inject] public IQotdService QotdService { get; set; } = null!;

    public QuoteOfTheDayViewModel? QotdViewModel { get; set; }

    protected override async Task OnInitializedAsync()
    {
        QotdViewModel = await QotdService.GetQuoteOfTheDayAsync();
    }
}
