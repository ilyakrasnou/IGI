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
        [Required]
        public User User { get; set; }
        [Required]
        public T LikeableObj { get; set; }
        public byte Mark { get; set; }
    }
}
