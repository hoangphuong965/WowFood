using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WowFood.ViewModels
{
    public class LoginViewModel
    {
        [Required(ErrorMessage = "Hãy nhập email")]
        [RegularExpression(@"^([\w\.\-]+)@([\w\-]+)((\.(\w){2,3})+)$", ErrorMessage = "Định dạng email không đúng")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Hãy nhập password")]
        public string Password { get; set; }
    }
}
