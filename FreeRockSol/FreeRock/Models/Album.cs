using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;



namespace FreeRock.Models
{
    public class Album
    {
        public int ID { get; private set; }
        [Required, MaxLength(100)]
        public string Title { get; set; }

        [Display(Name ="Cover")]
        public string CoverPath { get; set; }
        private uint? _realeseDate;
        [Display(Name ="Realese Date")]
        public uint? ReleaseDate
        {
            get => _realeseDate;
            set {
                if (value == null || value < System.DateTime.UtcNow.Year + 10)
                    _realeseDate = value;
                else
                    throw new FormatException();
            }
        }
        [Required]
        public Artist Artist { get; set; }
        [Required]
        public ICollection<Song> Songs { get; set; }
        public ICollection<GenreAlbum> AlbumGenres { get; set; }
        public ICollection<Comment<Album>> Comments { get; set; }
        public ICollection<Like<Album>> Likes { get; set; }
        [DataType(DataType.MultilineText)]
        public string Description { get; set; }
    }
}
