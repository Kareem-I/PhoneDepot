using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using PhoneDepot.Data.Services;
using PhoneDepot.Data.Static;
using PhoneDepot.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PhoneDepot.Controllers
{
    [Authorize(Roles = UserRoles.Admin)]
    public class PhoneController : Controller
    {
        private readonly IPhoneService _service;

        public PhoneController(IPhoneService service)
        {
            _service = service;
        }

        [AllowAnonymous]
        public async Task<IActionResult> Index()
        {
            var allPhones = await _service.GetAllAsync();
            return View(allPhones);
        }

        [AllowAnonymous]
        public async Task<IActionResult> Filter(string searchString)
        {
            var allPhones = await _service.GetAllAsync();

            if (!string.IsNullOrEmpty(searchString))
            {
                var filteredResult = allPhones.Where(n => n.PhoneName.Contains(searchString) || n.PhoneDescription.Contains(searchString)).ToList();
                return View("Index", filteredResult);
            }

            return View("Index", allPhones);

            
        }

        //GET: Create

        public async Task<IActionResult> Create()
        {
            var phonedropdata = await _service.GetNewPhoneDropdownVal();
            ViewBag.Brand = new SelectList(phonedropdata.Phonemanufacturer, "Id", "ManufacturerName");

            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Create(NewPhoneVM phone)
        {
            if (!ModelState.IsValid)
            {
                var phonedropdata = await _service.GetNewPhoneDropdownVal();
                ViewBag.Brand = new SelectList(phonedropdata.Phonemanufacturer, "Id", "ManufacturerName");
                return View(phone);
            }
            await _service.AddNewPhoneAsync(phone);
            return RedirectToActionPermanent(nameof(Index));
        }

        //  GET: Phone Details
        [AllowAnonymous]
        public async Task<IActionResult> Details(int id)
        {
            var phoneDetail = await _service.GetPhoneByIdAsync(id); 
            return View(phoneDetail);
        }

        //GET: Phone/Edit/1
        public async Task<IActionResult> Edit(int id)
        {
            var phoneDetails = await _service.GetPhoneByIdAsync(id);
            if (phoneDetails == null) return View("NotFound");

            var response = new NewPhoneVM()
            {
                Id = phoneDetails.Id,
                PhoneName = phoneDetails.PhoneName,
                PhoneDescription = phoneDetails.PhoneDescription,
                Price = phoneDetails.Price,
                ImageURL = phoneDetails.ImageURL,
                OSCategory = phoneDetails.OSCategory,
                ManufacturerId = phoneDetails.ManufacturerId,

            };

            var phonedropdata = await _service.GetNewPhoneDropdownVal();
            ViewBag.Brand = new SelectList(phonedropdata.Phonemanufacturer, "Id", "ManufacturerName");
            return View(response);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(int id, NewPhoneVM phone)
        {
            if (id != phone.Id) return View("NotFound");

            if (!ModelState.IsValid)
            {
                var phonedropdata = await _service.GetNewPhoneDropdownVal();

                ViewBag.Brand = new SelectList(phonedropdata.Phonemanufacturer, "Id", "ManufacturerName");

                return View(phone);
            }

            await _service.UpdatePhoneAsync(phone);
            return RedirectToAction(nameof(Index));
        }

    }
}