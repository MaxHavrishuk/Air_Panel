using BusinessLogicProject.Models.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogicProject.Models
{
    public class LoginModel
    {
        public string Login { get; set; }
        public string Password { get; set; }
    }

    public class RegisterModel
    {
        [Required(ErrorMessage = "Заповніть це поле")]
        public string Login { get; set; }
        [Required(ErrorMessage = "Заповніть це поле")]
        [DataType(DataType.Password)]
        public string Password { get; set; }
        [Required(ErrorMessage = "Заповніть це поле")]
        [Compare("Password", ErrorMessage = "Паролі не збігаються")]
        [DataType(DataType.Password)]
        public string PasswordConfirm { get; set; }
        [Required]
        public Sex Sex {get; set;}
        [Required(ErrorMessage = "Заповніть це поле")]
        public int Age{ get; set; }
    }
  
}
