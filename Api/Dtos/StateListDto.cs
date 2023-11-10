using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Api.Dtos
{
    public class StateListDto
    {
        public string Name { get; set; }
        public List<CityDto> Cities { get;set;}
    }
}