using Domain.Entities;
using Logging;
using Microsoft.AspNetCore.Components;
using Microsoft.EntityFrameworkCore;
using Persistence;
using Services;
using Shared.ViewModels;

namespace BlazorUI.Components.Pages;

public partial class Home : IDisposable
{
    [Inject] public IQotdService QotdService { get; set; } = null!;
    [Inject] public ILoggerManager Logger { get; set; } = null!;
    [Inject] public PersistentComponentState ApplicationState { get; set; } = null!;
    private PersistingComponentStateSubscription _persistingComponentStateSubscription;

    public QuoteOfTheDayViewModel? QotdViewModel { get; set; }

    protected override async Task OnInitializedAsync()
    {
        Logger.LogInformation("Home Componente aufgerufen...");

        _persistingComponentStateSubscription = ApplicationState.RegisterOnPersisting(PersistData);
        var statedLoaded = ApplicationState.TryTakeFromJson<QuoteOfTheDayViewModel>("qotd", out var qotd);

        QotdViewModel = statedLoaded && qotd is not null ? qotd : await QotdService.GetQuoteOfTheDayAsync();
    }

    private Task PersistData()
    {
        ApplicationState.PersistAsJson("qotd", QotdViewModel);
        return Task.CompletedTask;
    }

    //protected override async Task OnAfterRenderAsync(bool firstRender)
    //{
    //    if (firstRender)
    //    {
    //        QotdViewModel = await QotdService.GetQuoteOfTheDayAsync();
    //        StateHasChanged();
    //    }
    //}

    public void Dispose()
    {
        _persistingComponentStateSubscription.Dispose();
    }
}
