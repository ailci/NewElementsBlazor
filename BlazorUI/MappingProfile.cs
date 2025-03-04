using AutoMapper;
using Domain.Entities;
using Shared.ViewModels;

namespace BlazorUI;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<Quote, QuoteOfTheDayViewModel>();
    }
}