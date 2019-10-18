using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace FreeRock.Models
{
    public class Artist
    {
        public int ID { get; private set; }
        [Required, MaxLength(80)]
        public string Name { get; set; }
        [DataType(DataType.MultilineText)]
        public string Description { get; set; }
        [Display(Name="Photo")]
        public string PhotoPath { get; set; }
        public ICollection<Album> Albums { get; set; }
        public ICollection<Song> Songs { get; set; }
        public ICollection<Comment<Artist>> Comments { get; set; }
        public ICollection<Like<Artist>> Likes { get; set; }
    }
}
