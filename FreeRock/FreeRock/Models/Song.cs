﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace FreeRock.Models
{
    public class Song
    {
        public int SongID { get; set; }
        [Required]
        public string Name { get; set; }

        public int AlbumID { get; set; }
        public virtual Album Album { get; set; }

        [Required]
        public byte Number { get; set; }
        [DataType(DataType.Url)]
        public string YouTubeUrl { get; set; }
        public virtual ICollection<Comment<Song>> Comments { get; set; }
        public virtual ICollection<Like<Song>> Likes { get; set; }
    }
}
