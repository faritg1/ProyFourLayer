using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Api.Dtos
{
    public class CustomerDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public DateOnly DateRegister { get; set; }

        public string Idcustomer { get; set; }
        public int IdTipoPersonaFk { get; set; }
        public int IdcityFk { get; set; }
    }
}