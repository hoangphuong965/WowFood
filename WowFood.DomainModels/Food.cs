using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WowFood.DomainModels
{
    public class Food
    {
        public int FoodID { get; set; }
        public string FoodName { get; set; }
        public decimal FoodPrice { get; set; }
        public string Photo { get; set; }
        public bool IsShow { get; set; }
    }
}
