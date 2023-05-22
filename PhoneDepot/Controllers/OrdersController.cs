using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PhoneDepot.Data.Cart;
using PhoneDepot.Data.Services;
using PhoneDepot.Data.ViewModel;
using System.Security.Claims;
using System.Threading.Tasks;

namespace PhoneDepot.Controllers
{

    [Authorize]
    public class OrdersController : Controller
    {
        private readonly IPhoneService _phoneService;
        private readonly ShoppingCart _shoppingCart;
        private readonly IOrdersService _ordersService;

        public OrdersController(IPhoneService phoneService, ShoppingCart shoppingCart, IOrdersService ordersService)
        {
            _phoneService = phoneService;
            _shoppingCart = shoppingCart;
            _ordersService = ordersService;
            _ordersService = ordersService;
        }


        public async Task<IActionResult> Index()
        {
            string userId = User.FindFirst(ClaimTypes.NameIdentifier).Value;
            string userRole = User.FindFirst(ClaimTypes.Role).Value; ;

            var orders = await _ordersService.GetOrdersByUserIdnRoleAsync(userId, userRole);
            return View(orders);
        }


        //Ändra sedan till ShoppingCart()
        public IActionResult ShoppingCart()
        {
            var items = _shoppingCart.GetShoppingCartItems();
            _shoppingCart.ShoppingCartItems = items;

            var response = new ShoppingCartVM()
            {
                ShoppingCart = _shoppingCart,
                ShoppingCartTotal = _shoppingCart.GetShoppingCartTotal()
            };

            return View(response);
        }


        public async Task<IActionResult> AddItemToShoppingCart(int id)
        {
            var item = await _phoneService.GetPhoneByIdAsync(id);

            if (item != null)
            {
                _shoppingCart.AddItemToCart(item);
            }
            return RedirectToAction(nameof(ShoppingCart));
        }


        public async Task<IActionResult> RemoveItemFromShoppingCart(int id)
        {
            var item = await _phoneService.GetPhoneByIdAsync(id);

            if (item != null)
            {
                _shoppingCart.RemoveItemFromCart(item);
            }
            return RedirectToAction(nameof(ShoppingCart));
        }



        public async Task<IActionResult> CompleteOrder()
        {
            var items = _shoppingCart.GetShoppingCartItems();
            string userId = "";
            string userEmailAddress = "";

            await _ordersService.ShopOrderAsync(items, userId, userEmailAddress);
            await _shoppingCart.ClearCartAsync();

            return View("OrderFinito");
        }


        public async Task<IActionResult> CompleteOrder1()
        {
            var items = _shoppingCart.GetShoppingCartItems();
      

           string userId = User.FindFirst(ClaimTypes.NameIdentifier).Value;
            string userEmailAddress = User.FindFirst(ClaimTypes.Email).Value; ;


            await _ordersService.ShopOrderAsync(items, userId, userEmailAddress);
            await _shoppingCart.ClearCartAsync();

            return View("OrderFinito");
        }



        //public async Task<IActionResult> CompleteOrder()
        //{
        //    var items = _shoppingCart.GetShoppingCartItems();
        //    string userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
        //    string userEmail = User.FindFirstValue(ClaimTypes.Email);

        //    await _ordersService.ShopOrderAsync(items, userId, userEmail);
        //    await _shoppingCart.ClearCartAsync();

        //    //return View("OrderDone");

        //    return View("OrderDone");

        //}


    }
}
