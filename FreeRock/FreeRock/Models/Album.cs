using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;



namespace FreeRock.Models
{
    public class Album: ICommentable<Album>
    {
        public int ID { get; set; }
        [Required]
        public string Title { get; set; }

        [NotMapped]
        public string CoverPath => System.IO.File.Exists($"wwwroot/covers/{ID}.jpeg") ?
            $"/covers/{ID}.jpeg" : $"/covers/default.jpeg";

        private int? _realeseDate;
        public int? ReleaseDate
        {
            get => _realeseDate;
            set
            {
                if (value == null || value < System.DateTime.UtcNow.Year + 10)
                    _realeseDate = value;
                else
                    throw new FormatException();
            }
        }
        [Required]
        public bool IsVerified { get; set; }

        public int ArtistID { get; set; }
        public virtual Artist Artist { get; set; }

        public virtual IList<Song> Songs { get; set; }
        public virtual ICollection<GenreAlbum> AlbumGenres { get; set; }
        public virtual ICollection<Comment<Album>> Comments { get; set; }
        public virtual ICollection<Like<Album>> Likes { get; set; }
        //[NotMapped]
        //public int Rating => Likes is null ? 0 : Likes.Sum(x => x.Mark);

        [DataType(DataType.MultilineText)]
        public string Description { get; set; }
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public DateTime CreatedDate { get; set; } = DateTime.Now;
    }

    public class AlbumMin
    {
        public int ID { get; }
        public string Title { get; }
        public string CoverPath { get; }
        public int? ReleaseDate { get; }
        public bool IsVerified { get; }
        public int ArtistID { get; }
        public string ArtistName { get; }
        public DateTime CreatedDate { get; }

        public AlbumMin(Album album)
        {
            ID = album.ID;
            Title = album.Title;
            CoverPath = album.CoverPath;
            ReleaseDate = album.ReleaseDate;
            IsVerified = IsVerified;
            Artist artist = album.Artist;
            ArtistID = artist.ID;
            ArtistName = artist.Name;
            CreatedDate = album.CreatedDate;
        }
    }
}
