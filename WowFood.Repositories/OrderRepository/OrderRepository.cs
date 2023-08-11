using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WowFood.DomainModels;
using X.PagedList;

namespace WowFood.Repositories.OrderRepository
{
    public class OrderRepository : IOrderRepository
    {
        private readonly WowFoodDatabaseDbContext db;

        public OrderRepository(WowFoodDatabaseDbContext wowFoodDatabaseDbContext)
        {
            db = wowFoodDatabaseDbContext;
        }

        public IPagedList<Order> GetDeliveredOrders(int? page)
        {
            var orders = db.Orders.Where(o => o.Delivered == true).OrderByDescending(o => o.CreatedDate).ToPagedList();
            int pageSize = 10;
            var pageNumber = page ?? 1;
            orders = orders.ToPagedList(pageNumber, pageSize);
            return orders;
        }

        public IPagedList<Order> GetUndeliveredOrders(int? page)
        {
            var orders = db.Orders.Where(o => o.Delivered == false).OrderByDescending(o => o.CreatedDate).ToPagedList();
            int pageSize = 10;
            var pageNumber = page ?? 1;
            orders = orders.ToPagedList(pageNumber, pageSize);
            return orders;
        }

        public void UpdateDeliveryStatus(string code, bool status)
        {
            var order = db.Orders.Where(o => o.Code == code).FirstOrDefault();
            if (order != null)
            {
                db.Orders.Attach(order);
                order.Delivered = status;
                db.Entry(order).Property(x => x.Delivered).IsModified = true;
                db.SaveChanges();
            }
        }

        public Order GetOrderDetail(string code)
        {
            var order = db.Orders.Where(o => o.Code == code).FirstOrDefault();
            return order;
        }

        public List<OrderDetails> GetOrderDetail(int OrderId)
        {
            var items = db.OrderDetails.Where(x => x.OrderId == OrderId).ToList();
            return items;
        }
    }
}
