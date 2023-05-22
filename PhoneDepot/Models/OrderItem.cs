using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PhoneDepot.Models
{
    public class OrderItem
    {
        [Key]
        public int Id { get; set; }
       // public string Name { get; set; }    

        public double Price { get; set; }
        public int Quantity  { get; set; }


        public int PhoneId { get; set; }
        [ForeignKey("PhoneId")]
        public  Phone Phone { get; set; }
        
        
        public int OrderId { get; set; }
        [ForeignKey("OrderId")]
        public Order Order  { get; set; }


       

    }
}
