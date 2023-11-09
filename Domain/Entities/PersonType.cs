using System;
using System.Collections.Generic;

namespace Domain.Entities;

public partial class PersonType : BaseEntity
{
    public string Name { get; set; }

    public virtual ICollection<Customer> Customers { get; set; } = new List<Customer>();
}
