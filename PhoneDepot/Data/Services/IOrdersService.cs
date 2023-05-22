using PhoneDepot.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PhoneDepot.Data.Services
{
    public interface IOrdersService
    {
        Task ShopOrderAsync(List<ShoppingCartItem> items, string userId, string userEmailAddress);
        Task<List<Order>> GetOrdersByUserIdnRoleAsync(string userId, string userRole); //string userRole
    }
}

