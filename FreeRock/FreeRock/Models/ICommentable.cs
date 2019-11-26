using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FreeRock.Models
{
    public interface ICommentable<T>
    {
        int ID { get; }
        ICollection<Comment<T>> Comments { get; set; }
    }
}
