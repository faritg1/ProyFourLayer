using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Persistence.Data;

namespace App.UnitOfWork
{
    public class UnitOfWork
    {
        private readonly ProyectoContext _context;

        public UnitOfWork(ProyectoContext context)
        {
            _context = context;
        }
    }
}