using FreeRock.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FreeRock.ViewModels
{
    public class AlbumViewModel
    {
        public Album Album { get; set; }
        public Song NewSong { get; set; }
    }
}
