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
    public class Phone:IEntityBase
    {
        [Key]
        public int Id { get; set; }

        [Display(Name = "Image")]
        public string ImageURL { get; set; }
        [Display(Name = "Phone")]
        public string PhoneName { get; set; }
        [Display(Name = "Description")]
        public string PhoneDescription { get; set; }
        public double Price { get; set; }
        public OSCategory OSCategory { get; set; }


        //Relationships
    
        public int ManufacturerId { get; set; }
        [ForeignKey("Id")]
        public PhoneManufacturer PhoneManufacturer { get; set; }

       // public List<Phone_PhoneManufacturer> Phone_PhoneManufacturer { get; set; }



    }
}
