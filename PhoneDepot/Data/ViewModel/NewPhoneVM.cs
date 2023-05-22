using PhoneDepot.Data.Base;
using PhoneDepot.Data.Enum;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace PhoneDepot.Models
{
    public class NewPhoneVM
    {
        public int Id { get; set; }

        [Display(Name = "Image")]
        [Required(ErrorMessage = "Product Image")]
        public string ImageURL { get; set; }

        [Display(Name = "Phone Model")]
        [Required(ErrorMessage = "Name is required")]
        public string PhoneName { get; set; }

        [Display(Name = "Description")]
        [Required(ErrorMessage = "Description is required")]
        public string PhoneDescription { get; set; }

        [Display(Name = "Price in SEK")]
        [Required(ErrorMessage = "Price setting is required")]
        public double Price { get; set; }


        [Display(Name = "Choose an OS")]
        [Required(ErrorMessage = "OS is required")]
        public OSCategory OSCategory { get; set; }


        //Relationships

        [Display(Name = "Choose Brand")]
        [Required(ErrorMessage = "Brandm is required")]
        public int ManufacturerId { get; set; }
       
    }
}
