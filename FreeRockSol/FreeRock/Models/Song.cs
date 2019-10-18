using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace FreeRock.Models
{
    public class Song
    {
        public int ID { get; private set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public Album Album { get; set; }
        [Required]
        public Artist Artist { get; set; }
        [DataType(DataType.Time)]
        public DateTime Length { get; set; }
        [DataType(DataType.Url)]
        public string YouTubeUrl { get; set; }
        public ICollection<Comment<Song>> Comments { get; set; }
        public ICollection<Like<Song>> Likes { get; set; }
    }
}
