using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WowFood.DomainModels;
using WowFood.ViewModels;

namespace WowFood.ServiceLayer.FoodService
{
    public interface IFoodService
    {
        void Create(FoodViewModel foodViewModel);
        List<FoodViewModel> GetAll();
        List<Food> GetAllShow();
        FoodViewModel GetFoodById(int id);
        void Delete(int id);
        void Update(FoodViewModel foodViewModel);
        Food IsShow(int id);
    }
}
