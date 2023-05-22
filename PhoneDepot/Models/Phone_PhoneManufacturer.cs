using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace PhoneDepot.Models
{
    public class Phone_PhoneManufacturer
    {

        public int PhoneId { get; set; }
        public Phone Phone { get; set; }

        public int ManufacturerId { get; set; }
        public PhoneManufacturer PhoneManufacturer { get; set; }
    }
}
