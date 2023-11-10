using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Api.Dtos;
using AutoMapper;
using Domain.Entities;

namespace Api.Profiles;
public class MappingProfiles : Profile
{
    public MappingProfiles ()
    {
        CreateMap<City, CityDto>().ReverseMap();
        CreateMap<Country, CountryDto>().ReverseMap();
        CreateMap<Customer, CustomerDto>().ReverseMap();
        CreateMap<Customer, CustomerCityDto>().ReverseMap();
        CreateMap<PersonType, PersonTypeDto>().ReverseMap();
        CreateMap<State, StateDto>().ReverseMap();
        CreateMap<State, StateListDto>().ReverseMap();
    }
}
