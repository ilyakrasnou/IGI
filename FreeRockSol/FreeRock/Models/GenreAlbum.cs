using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace FreeRock.Models
{
    public class GenreAlbum
    {
        public int GenreID { get; set; }
        [Required]
        public Genre Genre { get; set; }

        public int AlbumID { get; set; }
        [Required]
        public Album Album { get; set; }
    }
}
