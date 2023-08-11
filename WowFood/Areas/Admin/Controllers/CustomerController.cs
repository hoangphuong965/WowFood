using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WowFood.CustomFilters;
using WowFood.DomainModels;
using WowFood.ServiceLayer.CustomerService;
using WowFood.ViewModels;
using X.PagedList;

namespace WowFood.Areas.Admin.Controllers
{
    [AdminAuthorizationFilterAttribute]
    public class CustomerController : Controller
    {
        ICustomerService customerService;
        public CustomerController(CustomerService _customerService)
        {
            customerService = _customerService;
        }

        public ActionResult Index(int? page, string find)
        {
            ViewBag.find = find;
            var listCustomers = customerService.GetListCustomersService(page, find);
            return View(listCustomers);
        }

        public int CountCustomers()
        {
            return customerService.TotalNumberOfCustomersService();
        }
    }
}