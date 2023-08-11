using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WowFood.DomainModels;
using X.PagedList;

namespace WowFood.Repositories.CustomerRepository
{
    public class CustomerRepository : ICustomerRepository
    {
        WowFoodDatabaseDbContext db;

        public CustomerRepository(WowFoodDatabaseDbContext _wowFoodDatabaseDbContext)
        {
            db = _wowFoodDatabaseDbContext;
        }

        public bool CheckEmailRepository(string Email)
        {
            bool customer = db.Customers.Any(temp => temp.Email == Email);
            if (customer)
            {
                return true;
            }
            return false;
        }

        public IEnumerable<Customer> GetListCustomersRepository(int? page, string find)
        {
            var customers = db.Customers.Where(temp => temp.IsAdmin == false).OrderByDescending(temp => temp.CustomerId).ToPagedList();
            var pageSize = 10;
            if (!string.IsNullOrEmpty(find))
            {
                customers = customers.Where(temp => temp.Mobile.Contains(find)).ToPagedList();
            }
            if (page == null) page = 1;
            var pageNumber = page.HasValue ? Convert.ToInt32(page) : 1;
            customers = customers.ToPagedList(pageNumber, pageSize);
            return customers;
        }

        public Customer LoginRepository(string Email, string PasswordHash)
        {
            var customer = db.Customers.Where(temp => temp.Email == Email && temp.PasswordHash == PasswordHash).FirstOrDefault();
            return customer;
        }

        public void RegisterRepository(Customer customer)
        {
            db.Customers.Add(customer);
            db.SaveChanges();
        }

        public int TotalNumberOfCustomersRepository()
        {
            var c = db.Customers.Where(temp => temp.IsAdmin == false).Count();
            return c;
        }

        public Customer GetInfo(int id)
        {
            var user = db.Customers.Find(id);
            return user;
        }
    }
}
