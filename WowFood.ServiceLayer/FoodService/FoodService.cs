using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WowFood.DomainModels;
using WowFood.Repositories.FoodRepository;
using WowFood.ViewModels;

namespace WowFood.ServiceLayer.FoodService
{
    public class FoodService : IFoodService
    {
        IFoodRepository FoodRepository;

        public FoodService(FoodRepository _foodRepository)
        {
            FoodRepository = _foodRepository;
        }

        public void Create(FoodViewModel foodViewModel)
        {
            var config = new MapperConfiguration(temp => { temp.CreateMap<FoodViewModel, Food>(); temp.IgnoreUnmapped(); });
            IMapper mapper = config.CreateMapper();
            Food food = mapper.Map<FoodViewModel, Food>(foodViewModel);
            FoodRepository.Create(food);
        }

        public void Delete(int id)
        {
            FoodRepository.Delete(id);
        }

        public List<FoodViewModel> GetAll()
        {
            List<Food> foods = FoodRepository.GetAll();
            var config = new MapperConfiguration(temp => { temp.CreateMap<Food, FoodViewModel>(); temp.IgnoreUnmapped(); });
            IMapper mapper = config.CreateMapper();
            List<FoodViewModel> foodViewModel = mapper.Map<List<Food>, List<FoodViewModel>>(foods);
            return foodViewModel;
        }

        public List<Food> GetAllShow()
        {
            List<Food> foods = FoodRepository.GetAllShow();
            return foods;
        }

        public FoodViewModel GetFoodById(int id)
        {
            var food = FoodRepository.GetFoodById(id);
            var config = new MapperConfiguration(temp => { temp.CreateMap<Food, FoodViewModel>(); temp.IgnoreUnmapped(); });
            IMapper mapper = config.CreateMapper();
            FoodViewModel foodViewModel = mapper.Map<Food, FoodViewModel>(food);
            return foodViewModel;
        }

        public Food IsShow(int id)
        {
            var food = FoodRepository.IsShow(id);
            return food;
        }

        public void Update(FoodViewModel foodViewModel)
        {
            var cofig = new MapperConfiguration(temp => { temp.CreateMap<FoodViewModel, Food>(); temp.IgnoreUnmapped(); });
            IMapper mapper = cofig.CreateMapper();
            Food food = mapper.Map<FoodViewModel, Food>(foodViewModel);
            FoodRepository.Update(food);
        }
    }
}
