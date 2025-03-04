using AutoMapper;
using Domain.Entities;
using Shared.ViewModels;

namespace BlazorUI;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<Quote, QuoteOfTheDayViewModel>();
        CreateMap<Author, AuthorViewModel>();
        CreateMap<Quote, QuoteViewModel>()
            .ForMember(dest => dest.AuthorName, opt =>
            {
                opt.PreCondition(c => c.Author is not null);
                opt.MapFrom(src => src.Author!.Name);
            });
    }
}