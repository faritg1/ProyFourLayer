using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Domain.Entities;
using Domain.Interfaces;
using Persistence.Data;

namespace App.Repository
{
    public class CustomerRepository : GenericRepository<Customer>, ICustomer
    {
        private readonly ProyectoContext _context;

        public CustomerRepository(ProyectoContext context) : base(context)
        {
            _context = context;
        }
    }
}