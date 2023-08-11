using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WowFood.ViewModels
{
    public class RegisterViewModel
    {
        [Required(ErrorMessage = "Vui lòng nhập Email")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Vui lòng nhập Password")]
        public string Password { get; set; }

        [Required(ErrorMessage = "Vui lòng nhập UserName vd: John, Phuong")]
        public string UserName { get; set; }

        [Required(ErrorMessage = "Hãy nhập số điện thoại")]
        public string Mobile { get; set; }
    }
}
