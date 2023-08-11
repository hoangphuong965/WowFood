using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WowFood.Repositories.CustomerRepository;
using WowFood.ViewModels;
using AutoMapper;
using WowFood.DomainModels;

namespace WowFood.ServiceLayer.CustomerService
{
    public class CustomerService : ICustomerService
    {
        ICustomerRepository CustomerRepository;

        public CustomerService(CustomerRepository _customerRepository)
        {
            CustomerRepository = _customerRepository;
        }

        public bool CheckEmailService(string Email)
        {
            var customer = CustomerRepository.CheckEmailRepository(Email);
            if (customer)
            {
                return true;
            }
            return false;
        }

        public Customer GetInfo(int id)
        {
            try
            {
                var user = CustomerRepository.GetInfo(id);
                return user;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Đã xảy ra lỗi: " + ex.Message);
                return null;
            }
        }

        public IEnumerable<Customer> GetListCustomersService(int? page, string find)
        {
            IEnumerable<Customer> customers = CustomerRepository.GetListCustomersRepository(page, find);
            return customers;
        }

        public CustomerViewModel LoginService(string Email, string Password)
        {
            Customer customer = CustomerRepository.LoginRepository(Email, SHA256HashGenerator.GenerateHash(Password));
            CustomerViewModel customerViewModel = null;
            if (customer != null)
            {
                // chuyen du lieu tu viewmodel sang model
                var config = new MapperConfiguration(temp => { temp.CreateMap<Customer, CustomerViewModel>(); temp.IgnoreUnmapped(); });
                IMapper mapper = config.CreateMapper();
                customerViewModel = mapper.Map<Customer, CustomerViewModel>(customer);
            }
            return customerViewModel;

        }

        public void RegisterService(RegisterViewModel registerViewModel)
        {
            var config = new MapperConfiguration(temp => { temp.CreateMap<RegisterViewModel, Customer>(); });
            IMapper mapper = config.CreateMapper();
            Customer customer = mapper.Map<RegisterViewModel, Customer>(registerViewModel);
            customer.PasswordHash = SHA256HashGenerator.GenerateHash(registerViewModel.Password);
            CustomerRepository.RegisterRepository(customer);
        }

        public int TotalNumberOfCustomersService()
        {
            return CustomerRepository.TotalNumberOfCustomersRepository();
        }
    }
}
