using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.UI.WebControls;
using WowFood.CustomFilters;
using WowFood.Helper;
using WowFood.ServiceLayer.FoodService;
using WowFood.ViewModels;

namespace WowFood.Areas.Admin.Controllers
{
    [AdminAuthorizationFilterAttribute]
    public class FoodController : Controller
    {
        IFoodService FoodService;

        public FoodController(FoodService _foodService)
        {
            FoodService = _foodService;
        }

        public ActionResult Index()
        {
            var Foods = FoodService.GetAll();
            return View(Foods);
        }

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(FoodViewModel foodViewModel, HttpPostedFileBase photo)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    if (photo != null)
                    {
                        string path = Path.Combine(Server.MapPath("~/UploadedFiles"), Path.GetFileName(photo.FileName));
                        photo.SaveAs(path);
                        foodViewModel.Photo = photo.FileName;
                    }
                    foodViewModel.IsShow = true;
                    FoodService.Create(foodViewModel);
                    TempData["Message"] = $"Thêm món '{foodViewModel.FoodName}' thành công";
                    return RedirectToAction("Index");
                }
                catch (Exception)
                {
                    ViewBag.FileStatus = "Lỗi: không lưu được";
                }
            }
            return View(foodViewModel);
        }

        public ActionResult Delete(int id)
        {
            var food = FoodService.GetFoodById(id);
            if (food != null)
            {
                System.IO.File.Delete(Path.Combine(Server.MapPath("~/UploadedFiles/"), food.Photo));
            }
            FoodService.Delete(id);
            return Json(new { message = "success", success = true, }, JsonRequestBehavior.AllowGet);
        }

        public ActionResult Update(int id)
        {
            var food = FoodService.GetFoodById(id);
            return Json(new { message = "success", success = true, food }, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public ActionResult Update(FoodViewModel foodViewModel)
        {
            if (HttpContext.Request.Files.Count > 0)
            {
                HttpPostedFileBase imgFile = HttpContext.Request.Files[0];
                string imgName = Path.GetFileNameWithoutExtension(imgFile.FileName);
                string extension = Path.GetExtension(imgFile.FileName);
                //imgName = imgName + DateTime.Now.ToString("yyyymmssfff") + extension;
                imgName = imgName + extension;
                string imgPath = Path.Combine(Server.MapPath("~/UploadedFiles/"), imgName);
                imgFile.SaveAs(imgPath);
                System.IO.File.Delete(Path.Combine(Server.MapPath("~/UploadedFiles/"), foodViewModel.Photo));
                foodViewModel.Photo = imgName;
            }

            if (ModelState.IsValid)
            {
                FoodService.Update(foodViewModel);
                return Json(new { message = "success", success = true });
            }

            return Json(new { message = "đã xảy ra lỗi", success = false });
        }

        public ActionResult GetFoodById(int id)
        {
            var food = FoodService.GetFoodById(id);
            var FoodPrice = @Format.FormatNumber(food.FoodPrice, 0);
            return Json(new { message = "success", success = true, food, FoodPrice }, JsonRequestBehavior.AllowGet);
        }

        public ActionResult IsShow(int id)
        {
            var result = FoodService.IsShow(id);
            return Json(new { message = result.IsShow, success = true }, JsonRequestBehavior.AllowGet);
        }

    }
}