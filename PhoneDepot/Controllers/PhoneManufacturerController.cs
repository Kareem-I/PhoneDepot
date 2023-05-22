using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;
using PhoneDepot.Data.Services;
using PhoneDepot.Models;
using System.Linq.Expressions;
using System;
using System.Threading.Tasks;
using PhoneDepot.Data.Static;

namespace PhoneDepot.Controllers
{
    [Authorize(Roles = UserRoles.Admin)]
    public class PhoneManufacturerController : Controller
    {
        
        private readonly IPhoneManufacturerService _service;


        public PhoneManufacturerController(IPhoneManufacturerService service)
        {
            _service = service;
        }

        [AllowAnonymous]
        public async Task<IActionResult> Index()
        {
            var data = await _service.GetAllAsync();
            return View(data);
        }

        //Create func.

        public IActionResult Create()
        {
            return View();
        }


        [HttpPost]
        public async Task<IActionResult> Create([Bind("ManufacturerName, ImageURL")] PhoneManufacturer phoneManufacturer)
        {
            if (!ModelState.IsValid)
            {
                return View(phoneManufacturer);
            }
            await _service.AddAsync(phoneManufacturer);
            return RedirectToAction(nameof(Index));
        }

        //GET: Details func.
        [AllowAnonymous]
        public async Task<IActionResult> Details(int id)
        {
            var manufacturerDetails = await _service.GetByIdWithPhonesAsync(id);

            if (manufacturerDetails == null) return View("NotFound");
            return View(manufacturerDetails);
        }

        // Edit func.

       
        public async Task<IActionResult> Edit(int id)
        {
            var manufacturerDetails = await _service.GetByIdAsync(id);

            if (manufacturerDetails == null) return View("NotFound");

            return View(manufacturerDetails);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(int id, [Bind("Id,ManufacturerName,ImageURL")] PhoneManufacturer phoneManufacturer)
        {
            if (!ModelState.IsValid)
            {
                return View(phoneManufacturer);
            }


            await _service.UpdateAsync(id, phoneManufacturer);

            return RedirectToAction(nameof(Index));
        }




        // Delete func.
        public async Task<IActionResult> Delete(int id)
        {
            var manufacturerDetails = await _service.GetByIdAsync(id);
            if (manufacturerDetails == null) return View("NotFound");
            return View(manufacturerDetails);
        }

        [HttpPost, ActionName("Delete")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var manufacturerDetails = await _service.GetByIdAsync(id);
            if (manufacturerDetails == null) return View("NotFound"); 

            await _service.DeleteAsync(id);
            return RedirectToAction(nameof(Index));
        }
    }
}