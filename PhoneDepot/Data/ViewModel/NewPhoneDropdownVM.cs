using PhoneDepot.Models;
using System.Collections.Generic;

namespace PhoneDepot.Data.ViewModel
{
    public class NewPhoneDropdownVM
    {
        public NewPhoneDropdownVM()
        {
            Phonemanufacturer = new List<PhoneManufacturer>();
        }

         public List<PhoneManufacturer> Phonemanufacturer { get; set; }
    }
}
