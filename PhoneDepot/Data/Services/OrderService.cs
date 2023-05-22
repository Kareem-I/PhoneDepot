using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PhoneDepot.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PhoneDepot.Data.Services
{
    public class OrderService : IOrdersService
    {
        private readonly AppDbContext _context;

        public OrderService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<List<Order>> GetOrdersByUserIdnRoleAsync(string userId, string userRole)
        {
            var orders = await _context.Order.Include(n => n.OrderItems).ThenInclude(n => n.Phone).Include(n => n.UserName).ToListAsync();
            if(userRole != "Admin")
            {
                orders = orders.Where(n => n.UserId == userId).ToList();
            }

            return orders;


        }


        //public async Task<List<Order>> GetOrdersByUserIdnRoleAsync(string userId)
        //{
        //    var orders = await _context.Order.Include(n => n.OrderItems).ThenInclude(n => n.Phone).Where(n => n.UserId == userId).ToListAsync();

        //      return orders;
        //}

        public async Task ShopOrderAsync(List<ShoppingCartItem> items, string userId, string userEmailAddress)
        {
            var order = new Order()
            {
                UserId = userId,
                Email = userEmailAddress
            };
            await _context.Order.AddAsync(order);
            await _context.SaveChangesAsync();

            foreach (var item in items)
            {
                var orderItem = new OrderItem()
                {
                    Quantity = item.Quantity,
                    PhoneId = item.Phone.Id,
                    OrderId = order.Id,
                    Price = item.Phone.Price
                };
                await _context.OrderItems.AddAsync(orderItem);
            }
            await _context.SaveChangesAsync();
        }
    }
}
