using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FreeRock.Models
{
    public class Comment
    {
        public int ID { get; set; }
        public User Author { get; }
        public DateTime Date { get; }
        public string Text { get; set; }
    }
}
