using Microsoft.AspNetCore.Mvc;
using PhoneDepot.Data.Cart;

namespace PhoneDepot.Data.ViewComponents
{
    public class ShoppingCartCheckOut : ViewComponent
    {

        private readonly ShoppingCart _shoppingCart;

        public ShoppingCartCheckOut(ShoppingCart shoppingCart)
        {
            _shoppingCart = shoppingCart;
        }

        public IViewComponentResult Invoke()
        {
            var items = _shoppingCart.GetShoppingCartItems();

            return View(items.Count);
        }
    }
}
