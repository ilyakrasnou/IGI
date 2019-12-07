using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using FreeRock.Data;

namespace FreeRock.Models
{
    public class Artist: ICommentable<Artist>
    {
        public int ID { get; set; }
        [Required]
        public string Name { get; set; }
        [DataType(DataType.MultilineText)]
        public string Description { get; set; }
        [NotMapped]
        public string PhotoPath => System.IO.File.Exists($"wwwroot/photos/{ID}.jpeg") ?
            $"/photos/{ID}.jpeg" : $"/photos/default.jpeg";
        public virtual ICollection<Album> Albums { get; set; }
        public virtual ICollection<Comment<Artist>> Comments { get; set; }
        public virtual ICollection<Like<Artist>> Likes { get; set; }

        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public DateTime CreatedDate { get; set; } = DateTime.Now;
    }

    public class ArtistMin
    {
        public int ID { get; set; }
        [Required]
        public string Name { get; set; }
        [DataType(DataType.MultilineText)]
        public string Description { get; set; }
        public string PhotoPath { get; set; }
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public DateTime CreatedDate { get; set; } = DateTime.Now;

        public ArtistMin(Artist artist)
        {
            ID = artist.ID;
            Name = artist.Name;
            Description = artist.Description;
            PhotoPath = artist.PhotoPath;
            CreatedDate = artist.CreatedDate;
        }
    }
}
