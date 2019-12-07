using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace FreeRock.Models
{
    public class Like<T>
    {
        public int ID { get; set; }
        
        public virtual User User { get; set; }

        public virtual T LikeableObj { get; set; }
        public sbyte Mark { get; set; }
    }
}
