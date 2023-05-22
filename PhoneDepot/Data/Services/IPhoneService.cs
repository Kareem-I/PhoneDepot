using PhoneDepot.Data.Base;
using PhoneDepot.Data.ViewModel;
using PhoneDepot.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PhoneDepot.Data.Services
{
    public interface IPhoneService : IEntityBaseRepository<Phone>
    {
        //Task<IEnumerable<Phone>> GetAll();
        //Phone GetById(int id);
        //void Add(Phone phone);
        //Phone Update(int id, Phone newPhone);
        //void Delete(int id);

        Task<Phone> GetPhoneByIdAsync(int id);

        Task<NewPhoneDropdownVM> GetNewPhoneDropdownVal();

        Task AddNewPhoneAsync(NewPhoneVM data);

        Task UpdatePhoneAsync (NewPhoneVM data);

    }
}
 