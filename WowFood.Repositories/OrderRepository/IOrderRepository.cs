using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WowFood.DomainModels;
using X.PagedList;

namespace WowFood.Repositories.OrderRepository
{
    public interface IOrderRepository
    {
        IPagedList<Order> GetUndeliveredOrders(int? page);
        IPagedList<Order> GetDeliveredOrders(int? page);
        void UpdateDeliveryStatus(string code, bool status);
        Order GetOrderDetail(string code);
        List<OrderDetails> GetOrderDetail(int OrderId);
    }
}
