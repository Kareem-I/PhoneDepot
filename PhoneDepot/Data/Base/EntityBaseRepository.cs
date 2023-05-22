using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using PhoneDepot.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PhoneDepot.Data.Base
{
    public class EntityBaseRepository<T> : IEntityBaseRepository<T> where T : class, IEntityBase, new()
    {
        private readonly AppDbContext _context;

        public EntityBaseRepository(AppDbContext context)
        {
            _context = context;
        }
        public async Task AddAsync(T entity)
        {

            await _context.Set<T>().AddAsync(entity);
            await _context.SaveChangesAsync();

        }

        public async Task DeleteAsync(int id)
        {
            var entinty = await _context.Set<T>().FirstOrDefaultAsync(n => n.Id == id);
            EntityEntry entityEntry = _context.Entry<T>(entinty);
            entityEntry.State = EntityState.Deleted;
            await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<T>> GetAllAsync()
        {
            var result = await _context.Set<T>().ToListAsync();
            return result;
        }

        public async Task<T> GetByIdAsync(int id)
        {
            var result = await _context.Set<T>().FirstOrDefaultAsync(n => n.Id == id);
            return result;

            
        }

     


        public async Task UpdateAsync(int id, T entinty)
        {
            EntityEntry entityEntry = _context.Entry<T>(entinty);
            entityEntry.State = EntityState.Modified;

            await _context.SaveChangesAsync();
        }

        public async Task<T> GetByIdWithPhonesAsync(int id)
        {
            var result = await _context.Set<T>()
                                        .Include(e => ((IEntityWithPhones)e).Phones)
                                        .FirstOrDefaultAsync(e => e.Id == id);
            return result;
        }

        public Task<Phone> GetBrandWithPhonesAsync(int id)
        {
            throw new System.NotImplementedException();
        }

        //public async Task<T> GetByIdWithPhonesAsync(int id)
        //{
        //    return await _context.Set<T>().Include(p => p.Phones).FirstOrDefaultAsync(n => n.Id == id);
        //}


        //public async Task<Phone> GetBrandWithPhonesAsync(int id)
        //{
        //    var manufacturerDetails = _context.PhoneManufacturer
        //        .Include(pm => pm.Phone)
        //        .FirstOrDefault(pm => pm.Id == id);
        //    return await manufacturerDetails;



        //public Task UpdateAsync(T entinty)
        //{
        //    throw new System.NotImplementedException();
        //}
        // }

        //Task<Phone> IEntityBaseRepository<T>.GetBrandWithPhonesAsync(int id)
        //{
        //    throw new System.NotImplementedException();
        //}
    }
}
