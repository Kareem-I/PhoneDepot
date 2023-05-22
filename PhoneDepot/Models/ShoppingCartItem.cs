using System.ComponentModel.DataAnnotations;

namespace PhoneDepot.Models
{
    public class ShoppingCartItem
    {
        
            [Key]
            public int Id { get; set; }

            public Phone Phone { get; set; }
            public int Quantity { get; set; }


            public string CartId { get; set; }
        }
    }

