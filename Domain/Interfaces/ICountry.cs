using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Domain.Entities;

namespace Domain.Interfaces
{
    public interface ICountry : IGenericRepository<Country>
    {
        Task<Country>GetCountriesAsync();
        Task<IEnumerable<Country>> getCountriesAndStates();
        Task<IEnumerable<Country>> getCountriesAndStatesAndCities();
    }
}