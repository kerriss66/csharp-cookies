using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace Studio_4.Controllers
{
    public class CheeseController : Controller
    {
        static private List<string> Cheeses = new List<string>();

        public IActionResult Index()
        {
            ViewBag.cheeses = Cheeses;
            return View();
        }

        [HttpGet]
        public IActionResult Add()
        {
            return View();
        }

        [HttpPost]
        [Route("/Cheese/Add")]
        public IActionResult NewCheese(string name)
        {
            // Add the new cheese to existing cheese
            Cheeses.Add(name);

            return Redirect("/Cheese");
        }


    }
}
