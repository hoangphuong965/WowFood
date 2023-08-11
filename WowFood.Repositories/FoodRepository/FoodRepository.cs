using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WowFood.DomainModels;

namespace WowFood.Repositories.FoodRepository
{
    public class FoodRepository : IFoodRepository
    {
        WowFoodDatabaseDbContext db;

        public FoodRepository(WowFoodDatabaseDbContext wowFoodDatabaseDbContext)
        {
            db = wowFoodDatabaseDbContext;
        }

        public void Create(Food food)
        {
            db.Foods.Add(food);
            db.SaveChanges();
        }

        public void Delete(int id)
        {
            var checkIdFood = db.Foods.Where(temp => temp.FoodID == id).FirstOrDefault();
            if (checkIdFood != null)
            {
                db.Foods.Remove(checkIdFood);
                db.SaveChanges();
            }
        }

        public List<Food> GetAll()
        {
            return db.Foods.ToList();
        }

        public List<Food> GetAllShow()
        {
            return db.Foods.Where(temp => temp.IsShow == true).ToList();
        }

        public Food GetFoodById(int id)
        {
            var checkIdFood = db.Foods.Where(temp => temp.FoodID == id).FirstOrDefault();
            return checkIdFood;
        }

        public Food IsShow(int id)
        {
            var item = db.Foods.Find(id);
            item.IsShow = !item.IsShow;
            db.Entry(item).State = System.Data.Entity.EntityState.Modified;
            db.SaveChanges();
            return item;
        }

        public void Update(Food food)
        {
            var getFood = db.Foods.Where(temp => temp.FoodID == food.FoodID).FirstOrDefault();
            if (getFood != null)
            {
                getFood.FoodName = food.FoodName;
                getFood.FoodPrice = food.FoodPrice;
                getFood.Photo = food.Photo;
                db.SaveChanges();
            }
        }
    }
}
