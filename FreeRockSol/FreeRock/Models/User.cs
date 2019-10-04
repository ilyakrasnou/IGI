using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FreeRock.Models
{
    public class User
    {
        public int ID { get; set; }
        public string Password { get; set; }
        public bool HasPrivilegy { get; set; }
        public List<Comment> Comments { get; set; }
    }
}
