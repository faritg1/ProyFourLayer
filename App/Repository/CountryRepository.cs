using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Domain.Entities;
using Domain.Interfaces;
using Microsoft.EntityFrameworkCore;
using Persistence.Data;

namespace App.Repository
{
    public class CountryRepository : GenericRepository<Country>, ICountry
    {
        private readonly ProyectoContext _context;

        public CountryRepository(ProyectoContext context) : base(context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Country>> getCountriesAndStates()
        {
            return await _context.Countries
                .Include(j=>j.States)
                .ToListAsync();
        }

        Task<Country> ICountry.GetCountriesAsync()
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<Country>> getCountriesAndStatesAndCities()
        {
                return await _context.Countries
                .Include(j=>j.States)
                .ThenInclude(c => c.Cities)
                .ToListAsync();
        }
    }
}