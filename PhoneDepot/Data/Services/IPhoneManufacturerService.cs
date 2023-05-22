using Microsoft.AspNetCore.Mvc;
using PhoneDepot.Data.Base;
using PhoneDepot.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PhoneDepot.Data.Services
{
    public interface IPhoneManufacturerService:IEntityBaseRepository<PhoneManufacturer>
    {

        Task<PhoneManufacturer> GetByIdWithPhonesAsync(int id);


    }
}


//Task<IEnumerable<PhoneManufacturer>> GetAllAsync();
//Task<PhoneManufacturer> GetByIdAsync(int id);
//Task AddAsync(PhoneManufacturer phonemanufacturer);
//Task<PhoneManufacturer> UpdateAsync(PhoneManufacturer newPhoneManufacturer);

//Task DeleteAsync(int id);