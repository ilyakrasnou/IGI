using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace FreeRock.Models
{
    public class Genre
    {
        public int ID { get; private set; }
        [Required, MaxLength(80)]
        public string Name { get; set; }

        public ICollection<GenreAlbum> GenreAlbums { get; set; }
    }
}
