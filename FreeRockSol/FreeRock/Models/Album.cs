using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;



namespace FreeRock.Models
{
    public class Album
    {

        public int Id { get; set; }
        public string Title { get; set; }

        [DataType(DataType.Date)]
        public DateTime ReleaseDate { get; set; }
        public List<string> Genre;
        public string Description { get; set; }
        public int ArtistId { get; set; }

    }

    public class AlbumContext : DbContext
    {
        public AlbumContext(DbContextOptions<AlbumContext> options):base(options)
        {

        }
        public DbSet<Album> Albums { get; set; }
    }
}
