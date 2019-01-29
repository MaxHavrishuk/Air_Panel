using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogicProject.Models
{
  public  class User
    {
        public int Id { get; set; }
        
        [DisplayName("Логін")]
        public string Name { get; set; }
        [DisplayName("Пароль")]
        public string PassWord { get; set; }
    
        [DisplayName("Вік")]
        public int Age { get; set; }
   
        [DisplayName("Стать")]
        public string Sex { get; set; }
    }

}
