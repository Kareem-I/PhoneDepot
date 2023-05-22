using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PhoneDepot.Data;
using PhoneDepot.Data.Base;
using PhoneDepot.Data.Services;
using PhoneDepot.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PhoneDepot.Data.Services
{
    public class PhoneManufacturerService : EntityBaseRepository<PhoneManufacturer>, IPhoneManufacturerService
    {
        private readonly AppDbContext _context;

        public PhoneManufacturerService(AppDbContext context):base(context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }


        public async Task<PhoneManufacturer> GetByIdWithPhonesAsync(int id)
        {
            return await _context.GetByIdWithPhonesAsync(id);
        }


        //public  PhoneManufacturer GetBrandWithPhonesAsync(int id)
        //{
        //    return  _context.PhoneManufacturer
        //        .Include(pm => pm.Phone)
        //        .FirstOrDefault(pm => pm.Id == id);
        //}

        //Task<Phone> IPhoneManufacturerService.GetBrandWithPhonesAsync(int id)
        //{
        //    throw new NotImplementedException();
        //}
    }
}



//namespace PhoneDepot.Data.Services
//{
//    public class PhoneManufacturerService : EntityBaseRepository<PhoneManufacturer>, IPhoneManufacturerService
//{
//    private readonly AppDbContext _context;

//    public PhoneManufacturerService(AppDbContext context)
//    {
//        _context = context;
//    }

//    public async Task AddAsync(PhoneManufacturer phoneManufacturer)
//    {
//        await _context.PhoneManufacturer.AddAsync(phoneManufacturer);
//        await _context.SaveChangesAsync();
//    }

//    public async Task DeleteAsync(int id)
//    {
//        var result = await _context.PhoneManufacturer.FirstOrDefaultAsync(n => n.Id == id);
//        _context.PhoneManufacturer.Remove(result);
//        await _context.SaveChangesAsync();
//    }


//    public async Task<IEnumerable<PhoneManufacturer>> GetAllAsync()
//    {
//        var result = await _context.PhoneManufacturer.ToListAsync();
//        return result;
//    }



//    public async Task<PhoneManufacturer> GetByIdAsync(int id)
//    {
//        var result = await _context.PhoneManufacturer.FirstOrDefaultAsync(n => n.Id == id);
//        return result;
//    }

//    public async Task<PhoneManufacturer> UpdateAsync(PhoneManufacturer newPhoneManufacturer)
//    {
//        _context.Update(newPhoneManufacturer);
//        await _context.SaveChangesAsync();
//        return newPhoneManufacturer;
//    }
//}
//}



//    public async Task DeleteAsync(int id)
//{
//    var result = await _context.PhoneManufacturer.FindAsync(id);
//    if (result != null)
//    {
//        _context.PhoneManufacturer.Remove(result);
//        await _context.SaveChangesAsync();
//    }
//}