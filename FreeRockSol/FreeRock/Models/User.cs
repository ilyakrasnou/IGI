using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace FreeRock.Models
{
    public class User
    {
        public int ID { get; private set; }
        public string Login { get; set; }
        [DataType(DataType.Password)]
        public string Password { get; set; }
        [Display(Name ="E-mail"), DataType(DataType.EmailAddress)]
        public string Email { get; set; }
        public bool HasPrivilegy { get; set; }
       // public List<Comment> Comments { get; set; }
    }
}
