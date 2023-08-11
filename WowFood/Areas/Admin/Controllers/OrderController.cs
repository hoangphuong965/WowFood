using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WowFood.DomainModels;
using WowFood.Repositories.OrderRepository;
using X.PagedList;
using WowFood.Helper;
using System.Text;
using WowFood.CustomFilters;

namespace WowFood.Areas.Admin.Controllers
{
    [AdminAuthorizationFilterAttribute]
    public class OrderController : Controller
    {
        private readonly IOrderRepository _orderRepository;

        public OrderController(OrderRepository orderRepository)
        {
            _orderRepository = orderRepository;
        }

        public ActionResult Index(int? page)
        {
            var orders = _orderRepository.GetUndeliveredOrders(page);
            return View(orders);
        }

        public ActionResult GetDeliveredOrders(int? page)
        {
            var orders = _orderRepository.GetDeliveredOrders(page);
            return View(orders);
        }

        [HttpPost]
        public ActionResult UpdateDeliveryStatus(string code, bool status)
        {
            try
            {
                _orderRepository.UpdateDeliveryStatus(code, status);
                return Json(new { success = true });
            }
            catch(Exception ex)
            {
                return Json(new { success = false, msg = ex.Message });
            }
        }

        public ActionResult GetOrderDetail(string code)
        {
            try
            {
                var order = _orderRepository.GetOrderDetail(code);
                var created = order.CreatedDate.ToString("dd/MM/yyyy");
                var totalAmount = Format.FormatNumber(order.TotalAmount);
                order.Customer.PasswordHash = null;
                return Json(new { success = true, order = order, created, totalAmount }, JsonRequestBehavior.AllowGet);
            }
            catch(Exception ex)
            {
                return Json(new { success = false, msg = ex.Message }, JsonRequestBehavior.AllowGet);
            }
        }

        public ActionResult Partial_SanPham(int OrderId)
        {
            var items = _orderRepository.GetOrderDetail(OrderId);
            return Json(new { success = true, items }, JsonRequestBehavior.AllowGet);
        }
    }
}