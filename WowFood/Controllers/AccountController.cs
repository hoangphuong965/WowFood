using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WowFood.ViewModels;
using WowFood.ServiceLayer;
using WowFood.ServiceLayer.CustomerService;
using WowFood.Repositories.CustomerRepository;
using WowFood.DomainModels;

namespace WowFood.Controllers
{
    public class AccountController : Controller
    {
        private readonly ICustomerService _customerService;

        public AccountController(ICustomerService customerService)
        {
            _customerService = customerService;
        }

        public ActionResult Login()
        {
            LoginViewModel loginViewModel = new LoginViewModel();
            if (HttpContext.Session["CurrentUserID"] != null)
            {
                return RedirectToAction("Index", "Home");
            }
            return View(loginViewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Login(LoginViewModel LoginViewModel)
        {
            if (ModelState.IsValid)
            {
                CustomerViewModel customer = _customerService.LoginService(LoginViewModel.Email, LoginViewModel.Password);
                if(customer != null)
                {
                    Session["CurrentUserID"] = customer.CustomerId;
                    Session["CurrentUserName"] = customer.UserName;
                    Session["CurrentUserEmail"] = customer.Email;
                    Session["CurrentUserIsAdmin"] = customer.IsAdmin;
                    if (customer.IsAdmin)
                    {
                        return RedirectToRoute(new { controller = "Home", action = "Index" });
                    }
                    else
                    {
                        return RedirectToAction("Index", "Home");
                    }
                }
                else
                {
                    ModelState.AddModelError("CheckEmail", "Email hoặc Password không đúng");
                    return View(LoginViewModel);
                }
            }
            else
            {
                return View(LoginViewModel);
            }
        }

        public ActionResult Register()
        {
            RegisterViewModel registerViewModel = new RegisterViewModel();
            if (HttpContext.Session["CurrentUserID"] != null)
            {
                return RedirectToAction("Index", "Home");
            }
            return View(registerViewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Register(RegisterViewModel registerViewModel)
        {
            if (ModelState.IsValid)
            {
                if (_customerService.CheckEmailService(registerViewModel.Email))
                {
                    ModelState.AddModelError("CheckEmail", "Email đã tồn tại");
                    return View(registerViewModel);
                }
                _customerService.RegisterService(registerViewModel);
                return RedirectToAction("Login", "Account");
            }
            else
            {
                ModelState.AddModelError("register", "Không thể đăng ký được");
                return View(registerViewModel);
            }
        }

        public ActionResult Logout()
        {
            Session.Abandon();
            return RedirectToAction("Index", "Home");
        }
    }
}