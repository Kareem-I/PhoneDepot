using PhoneDepot.Models;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace PhoneDepot.Data.Base
{
    public interface IEntityBase
     
    {
        int Id { get; set; }
        //Collection<Phone> Phones { get; set; }

    }

    public interface IEntityWithPhones : IEntityBase
    {
        ICollection<Phone> Phones { get; set; }
    }
}
