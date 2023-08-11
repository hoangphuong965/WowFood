using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WowFood.DomainModels;

namespace WowFood.ViewModels
{
    public class OrderViewModel
    {
        public int CustomerId { get; set; }
        [Required(ErrorMessage = "Địa chỉ không để trống")]
        public string Address { get; set; }
        public decimal TotalAmount { get; set; }
        public DateTime CreatedDate { get; set; }
        public List<OrderDetails> OrderDetails { get; set; }

    }
}
