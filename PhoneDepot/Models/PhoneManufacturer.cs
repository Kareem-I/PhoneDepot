using PhoneDepot.Data.Base;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace PhoneDepot.Models
{
    public class PhoneManufacturer:IEntityBase
    {
        [Key]
        public int Id { get; set; }

        [Display(Name = "Brandname")]
        [Required(ErrorMessage = "Brandname is required")]
        public string ManufacturerName { get; set; }
        [Display(Name = "Logo")]
        [Required(ErrorMessage = "Image is required")]
        public string ImageURL { get; set; }

        //Relationships
       // public Phone Phone { get; set; }
       // public ICollection<Phone> Phones { get; set; }
        public List<Phone> Phone { get; set; }



       // public List<Phone_PhoneManufacturer> Phone_PhoneManufacturer { get; set; }
    }
}
