using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WowFood.ServiceLayer.FoodService;

namespace WowFood.Controllers
{
    public class HomeController : Controller
    {
        IFoodService FoodService;

        public HomeController(FoodService _foodService)
        {
            FoodService = _foodService;
        }

        public ActionResult Index()
        {
            var food = FoodService.GetAllShow();
            return View(food);
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}