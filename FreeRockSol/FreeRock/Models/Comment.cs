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
        public int ID { get; private set; }
        [Required]
        public User Author { get; }
        [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        public DateTime Date { get; }
        [DataType(DataType.MultilineText)]
        public string Text { get; set; }
        public ICollection<Like<Comment<T>>> Likes { get; set; }
        [Required]
        public T CommentableObj { get; set; }
    }
}
