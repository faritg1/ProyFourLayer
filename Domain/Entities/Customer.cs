using System;
using System.Collections.Generic;

namespace Domain.Entities;

public partial class Customer
{
    public int Id { get; set; }

    public string Name { get; set; }

    public string Idcustomer { get; set; }

    public int IdTipoPersonaFk { get; set; }

    public DateOnly DateRegister { get; set; }

    public int IdcityFk { get; set; }

    public virtual PersonType IdTipoPersonaFkNavigation { get; set; }

    public virtual City IdcityFkNavigation { get; set; }
}
