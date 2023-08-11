using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WowFood.DomainModels;
using WowFood.ViewModels;

namespace WowFood.ServiceLayer.CustomerService
{
    public interface ICustomerService
    {
        void RegisterService(RegisterViewModel registerViewModel);
        CustomerViewModel LoginService(string Email, string Password);
        bool CheckEmailService(string Email);
        IEnumerable<Customer> GetListCustomersService(int? page, string find);
        int TotalNumberOfCustomersService();
        Customer GetInfo(int id);
    }
}
