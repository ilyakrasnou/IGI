using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace FreeRock.Models
{
    public class Comment<T>
    {
        public int ID { get; set; }
        
        public virtual User Author { get; set; }

        public DateTime Date { get; set; } = DateTime.Now;
        [DataType(DataType.MultilineText)]
        public string Text { get; set; }
        public virtual ICollection<Like<Comment<T>>> Likes { get; set; }

        public virtual T CommentableObj { get; set; }
    }
}
