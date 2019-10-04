﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FreeRock.Models
{
    public class Song
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public int AlbumId { get; set; }
        public string YouTubeUrl { get; set; }
        public List<Comment> Comments { get; set; }
    }
}
