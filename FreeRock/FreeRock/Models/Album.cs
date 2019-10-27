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
        public int ID { get; set; }
        [Required]
        public string Title { get; set; }
        public string CoverPath { get; set; }
        private int? _realeseDate;
        public int? ReleaseDate
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
        public bool IsVerified { get; set; }
        public virtual Artist Artist { get; set; }
        public virtual IList<Song> Songs { get; set; }
        public virtual ICollection<GenreAlbum> AlbumGenres { get; set; }
        public virtual ICollection<Comment<Album>> Comments { get; set; }
        public virtual ICollection<Like<Album>> Likes { get; set; }
        [DataType(DataType.MultilineText)]
        public string Description { get; set; }
    }
}
