using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WowFood.DomainModels
{
    public class Order
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int OrderId { get; set; }  
        public int CustomerId { get; set; }    
        public decimal TotalAmount { get; set; }
        public DateTime CreatedDate { get; set; }
        public string Code { get; set; }
        public bool Delivered { get; set; }

        [Required(ErrorMessage = "Địa chỉ không được bỏ trống")]
        public string Address { get; set; }

        public List<OrderDetails> OrderDetails { get; set; }
        public virtual Customer Customer { get; set; }
    }
}
