using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using App.Repository;
using Domain.Entities;
using Domain.Interfaces;
using Persistence.Data;

namespace App.UnitOfWork
{
    public class UnitOfWork : IUnitOfWork, IDisposable
    {
        private readonly ProyectoContext _context;
        private ICity _cities;
        private ICountry _countries;
        private ICustomer _customers;
        private IPersonType _personstypes;
        private IState _states;

        public UnitOfWork(ProyectoContext context)
        {
            _context = context;
        }

        public ICity Cities{
            get{
                if(_cities == null){
                    _cities = new CityRepository(_context);
                }
                return _cities;
            }
        }
        public ICountry Countries{
            get{
                if(_countries == null){
                    _countries = new CountryRepository(_context);
                }
                return _countries;
            }
        }
        public ICustomer Customers{
            get{
                if(_customers == null){
                    _customers = new CustomerRepository(_context);
                }
                return _customers;
            }
        }
        public IPersonType PersonsTypes{
            get{
                if(_personstypes == null){
                    _personstypes = new PersonTypeRepository(_context);
                }
                return _personstypes;
            }
        }
        public IState States{
            get{
                if(_states == null){
                    _states = new StateRepository(_context);
                }
                return _states;
            }
        }

        public void Dispose()
        {
            _context.Dispose();
        }

        public async Task<int> SaveAsync()
        {
            return await _context.SaveChangesAsync();
        }
    }
}