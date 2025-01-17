﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace FreeRock.Models
{
    public class Genre
    {
        public int ID { get; set; }
        [Required]
        public string Name { get; set; }

        public virtual ICollection<GenreAlbum> GenreAlbums { get; set; }
    }
}
