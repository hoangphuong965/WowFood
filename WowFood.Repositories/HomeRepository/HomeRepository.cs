using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WowFood.DomainModels;

namespace WowFood.Repositories.HomeRepository
{
    public class HomeRepository
    {
        WowFoodDatabaseDbContext db;

        public HomeRepository(WowFoodDatabaseDbContext _wowFoodDatabaseDbContext)
        {
            db = _wowFoodDatabaseDbContext;
        }

        public int NumberOfNewOrders()
        {
            var NewOrders = db.Orders.Where(o => o.Delivered == false).Count();
            return NewOrders;
        }

        public int NumberOfOrders()
        {
            var NewOrders = db.Orders.Where(o => o.Delivered == true).Count();
            return NewOrders;
        }

        public int NumberOfCustomers()
        {
            var orders = db.Customers.Where(x => x.IsAdmin == false).Count();
            return orders;
        }

        public int NumberOfFoods()
        {
            var orders = db.Foods.Count();
            return orders;
        }
    }
}
