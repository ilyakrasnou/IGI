using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using FreeRock.Models;

namespace FreeRock.Data
{
    public class CatalogContext: DbContext
    {
        public CatalogContext(DbContextOptions<CatalogContext> options) : base(options)
        {
        }

        public DbSet<Album> Albums { get; set; }
        public DbSet<Artist> Artists { get; set; }
        public DbSet<Song> Songs { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Comment> Comments { get; set; }
        public DbSet<Genre> Genres { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Album>().ToTable("Album");
            modelBuilder.Entity<Artist>().ToTable("Artist");
            modelBuilder.Entity<Song>().ToTable("Song");
            modelBuilder.Entity<User>().ToTable("User");
            modelBuilder.Entity<Comment>().ToTable("Comment");
            modelBuilder.Entity<Genre>().ToTable("Genre");
        }
    }
}
