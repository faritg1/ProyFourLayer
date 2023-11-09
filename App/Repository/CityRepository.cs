using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Domain.Entities;
using Domain.Interfaces;
using Persistence.Data;

namespace App.Repository
{
    public class CityRepository : GenericRepository<City>, ICity
    {
        private readonly ProyectoContext _context;

        public CityRepository(ProyectoContext context) : base(context)
        {
            _context = context;
        }
    }
}