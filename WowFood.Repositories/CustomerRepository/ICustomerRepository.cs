using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WowFood.DomainModels;

namespace WowFood.Repositories.CustomerRepository
{
    public interface ICustomerRepository
    {
        void RegisterRepository(Customer customer);
        Customer LoginRepository(string Email, string Password);
        bool CheckEmailRepository(string Email);
        IEnumerable<Customer> GetListCustomersRepository(int? page, string find);
        int TotalNumberOfCustomersRepository();
        Customer GetInfo(int id);
    }
}
