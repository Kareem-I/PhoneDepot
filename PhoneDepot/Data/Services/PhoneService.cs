using Microsoft.EntityFrameworkCore;
using PhoneDepot.Data.Base;
using PhoneDepot.Data.ViewModel;
using PhoneDepot.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PhoneDepot.Data.Services
{
    public class PhoneService : EntityBaseRepository<Phone>, IPhoneService
    {
        private readonly AppDbContext _context;

        public PhoneService(AppDbContext context) : base(context)
        {
            _context = context;
        }

        public async Task AddNewPhoneAsync(NewPhoneVM data)
        {
            var newPhone = new Phone()
            {
                PhoneName = data.PhoneName,
                ImageURL = data.ImageURL,
                PhoneDescription = data.PhoneDescription,
                Price = data.Price,
                OSCategory = data.OSCategory,
                ManufacturerId = data.ManufacturerId,
            };

            await _context.Phone.AddAsync(newPhone);
            await _context.SaveChangesAsync();
        }

        public async Task<NewPhoneDropdownVM> GetNewPhoneDropdownVal()
        {
            var response = new NewPhoneDropdownVM();
            response.Phonemanufacturer = await _context.PhoneManufacturer.OrderBy(p => p.ManufacturerName).ToListAsync();
            return response;
        }

        public async Task<Phone> GetPhoneByIdAsync(int id)
        {
            var phoneDetails = _context.Phone
                .Include(p => p.PhoneManufacturer)
                .FirstOrDefaultAsync(n => n.Id == id);

            return await phoneDetails;
        }

        public async Task UpdatePhoneAsync(NewPhoneVM data)
        {
            var dbPhone = await _context.Phone.FirstOrDefaultAsync(n => n.Id == data.Id);

            if (dbPhone != null)
            {
                dbPhone.PhoneName = data.PhoneName;
                dbPhone.PhoneDescription = data.PhoneDescription;
                dbPhone.Price = data.Price;
                dbPhone.ImageURL = data.ImageURL;
                dbPhone.OSCategory = data.OSCategory;
                dbPhone.ManufacturerId = data.ManufacturerId;
                await _context.SaveChangesAsync();
            }

        }
    }

}