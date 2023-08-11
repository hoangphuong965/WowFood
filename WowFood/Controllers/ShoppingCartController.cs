using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WowFood.DomainModels;
using WowFood.Repositories.ShoppingCartRepository;
using WowFood.Helper;
using WowFood.CustomFilters;
using WowFood.Repositories.CustomerRepository;
using WowFood.ServiceLayer.CustomerService;
using System.Text;

namespace WowFood.Controllers
{
    public class ShoppingCartController : Controller
    {
        private readonly ShoppingCartRepository _shoppingCartRepository;
        private readonly ICustomerService _customerService;

        public ShoppingCartController(ShoppingCartRepository shoppingCartRepository, ICustomerService customerService)
        {
            _shoppingCartRepository = shoppingCartRepository;
            _customerService = customerService;
        }
        public ActionResult Index()
        {
            var cart = Session["Cart"] as List<ShoppingCartItem>;
            return View(cart);
        }

        public ActionResult AddToCart(int id)
        {
            _shoppingCartRepository.AddToCart(id);
            return Json(new { Success = true, msg = "Đã lưu vào giỏ hàng" }, JsonRequestBehavior.AllowGet);
        }

        public ActionResult ShowCount()
        {
            var cart = Session["Cart"] as List<ShoppingCartItem>;
            if (cart != null)
            {
                var soluong = _shoppingCartRepository.GetTotalQuantity();
                return Json(new { Count = soluong }, JsonRequestBehavior.AllowGet);
            }
            return Json(new { Count = 0 }, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public ActionResult UpdateQuantity(int foodId, int quantity)
        {
            _shoppingCartRepository.UpdateQuantity(foodId, quantity);
            var cart = Session["Cart"] as List<ShoppingCartItem>;
            var food = cart.First(item => item.FoodId == foodId);
            var totalCart = _shoppingCartRepository.GetTotalCart();
            return Json(new { Success = true, totalCart = Format.FormatNumber(totalCart), quantity = food.Quantity, totalPrice = Format.FormatNumber(food.TotalPrice, 2) });
        }

        public ActionResult Delete(int foodId)
        {
            _shoppingCartRepository.Delete(foodId);
            var totalCart = _shoppingCartRepository.GetTotalCart();
            return Json(new { Success = true, msg = "Đã xóa thành công", totalCart = Format.FormatNumber(totalCart) }, JsonRequestBehavior.AllowGet);
        }

        [UserAuthorizationFilterAttribute]
        public ActionResult CheckOut()
        {
            var cart = Session["Cart"] as List<ShoppingCartItem>;
            var userID = Session["CurrentUserID"];
            var user = _customerService.GetInfo((int)userID);
            ViewBag.UserInfo = user;
            if (cart == null || cart.Count == 0) return RedirectToAction("/", "Home");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CheckOut(Order order)
        {
            int userID = (int)Session["CurrentUserID"];
            var user = _customerService.GetInfo(userID);
            ViewBag.UserInfo = user;
            if (ModelState.IsValid)
            {
                var filePathAdmin = new StringBuilder(System.IO.File.ReadAllText(Server.MapPath("~/Content/templates/send1.html")));
                var filePathCustomer = new StringBuilder(System.IO.File.ReadAllText(Server.MapPath("~/Content/templates/send2.html")));
                _shoppingCartRepository.CreateOrder(order, userID, filePathAdmin, filePathCustomer);
                _shoppingCartRepository.ClearCart();

                return RedirectToAction("Success");
            }
            return View(order);
        }


        [UserAuthorizationFilterAttribute]
        public ActionResult Partial_Item_ThanhToan()
        {
            var cart = Session["Cart"] as List<ShoppingCartItem>;
            if (cart != null)
            {
                return PartialView(cart);
            }
            return PartialView();
        }

        public ActionResult Success()
        {
            return View();
        }
    }
}