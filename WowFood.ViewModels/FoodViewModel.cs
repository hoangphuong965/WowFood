using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WowFood.ViewModels
{
    public class FoodViewModel
    {
        public int FoodId { get; set; }

        [Required(ErrorMessage ="Hãy nhập tên món ăn")]
        public string FoodName { get; set; }

        [Required(ErrorMessage = "Hãy nhập giá món ăn")]
        public decimal FoodPrice { get; set; }

        [Required(ErrorMessage = "Vui lòng chọn ảnh")]
        public string Photo { get; set; }
        public bool IsShow { get; set; }
    }
}
