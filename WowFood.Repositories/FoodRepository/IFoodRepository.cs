using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WowFood.DomainModels;

namespace WowFood.Repositories.FoodRepository
{
    public interface IFoodRepository
    {
        List<Food> GetAll();
        List<Food> GetAllShow();
        Food GetFoodById(int id);
        void Create(Food food);
        void Delete(int id);
        void Update(Food food);
        Food IsShow(int id);
    }
}
