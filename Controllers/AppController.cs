using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using myArtShopCore.Data;
using myArtShopCore.Services;
using myArtShopCore.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace myArtShopCore.Controllers
{
    public class AppController:Controller
    {
        private readonly IMailService _mailService;
        private readonly IArtShopRepository _repostory;

        public AppController(IMailService mailService, IArtShopRepository repostory)
        {
            _mailService = mailService;
            _repostory = repostory;
        }
        public IActionResult Index()
        {
            var results = _repostory.GetAllProducts();
            return View(results);
        }

        [HttpGet("contact")]
        public IActionResult Contact()
        {
            ViewBag.Title = "Contact Us";

           // throw new InvalidOperationException("Bad behaviour");

            return View();
        }

        [HttpPost("contact")]
        public IActionResult Contact(ContactViewModel model)
        {
            ViewBag.Title = "Contact Us";

            if (ModelState.IsValid) 
            {
                //send email
                _mailService.SendMessage("romanm@mailserver.com", model.Subject, $"From:{ model.Name}-{model.Email},Message: { model.Message}");
                ViewBag.UserMessage = "Mail Sent";
                ModelState.Clear();
            }
            else
            {
                //Show Errors
            }

            return View();
        }


        public IActionResult About()
        {
            ViewBag.Title = "About Us";
            return View();
        }
        [Authorize]
        public IActionResult Shop()
        {
            //var results = _context.Products
            //    .OrderBy(p => p.Category)
            //    .ToList();

            //var result = from p in _context.Products
            //             orderby p.Category
            //             select p;
            var results = _repostory.GetAllProducts();

            return View(results);
        }
    }
}
