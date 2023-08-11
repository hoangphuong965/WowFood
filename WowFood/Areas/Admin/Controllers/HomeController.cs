using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WowFood.CustomFilters;
using WowFood.Repositories.HomeRepository;

namespace WowFood.Areas.Admin.Controllers
{
    [AdminAuthorizationFilterAttribute]
    public class HomeController : Controller
    {
        private readonly HomeRepository _homeRepository;

        public HomeController(HomeRepository homeRepository)
        {
            _homeRepository = homeRepository;
        }

        // GET: Admin/Home
        public ActionResult Index()
        {
            var Customers = _homeRepository.NumberOfCustomers();
            var Foods = _homeRepository.NumberOfFoods();
            var NewOrders = _homeRepository.NumberOfNewOrders();
            var Orders = _homeRepository.NumberOfOrders();
            ViewBag.Customers = Customers;
            ViewBag.Foods = Foods;
            ViewBag.Orders = Orders;
            ViewBag.NewOrders = NewOrders;
            return View();
        }
    }
}