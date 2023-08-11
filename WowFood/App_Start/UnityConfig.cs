using System.Web.Mvc;
using Unity;
using Unity.Mvc5;
using WowFood.ServiceLayer.CustomerService;

namespace WowFood
{
    public static class UnityConfig
    {
        public static void RegisterComponents()
        {
			var container = new UnityContainer();

            container.RegisterType<ICustomerService, CustomerService>();
            
            DependencyResolver.SetResolver(new UnityDependencyResolver(container));
        }
    }
}