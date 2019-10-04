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
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int ID { get; set; }
        public string Title { get; set; }

        public string CoverPath { get; set; }
        [DataType(DataType.Date)]
        public DateTime? ReleaseDate { get; set; }
        public List<Song> Songs { get; set; }
        public List<string> Genre;
        public List<Comment> Comments { get; set; }
        public string Description { get; set; }
        public Artist ArtistId { get; set; }

    }
}
