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
            Database.Migrate();
        }

        public DbSet<Album> Albums { get; set; }
        public DbSet<Artist> Artists { get; set; }
        public DbSet<Song> Songs { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Genre> Genres { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<GenreAlbum>()
                .HasKey(s => new { s.GenreID, s.AlbumID });
            modelBuilder.Entity<GenreAlbum>()
                .HasOne(x => x.Album)
                .WithMany(x => x.AlbumGenres);
            modelBuilder.Entity<GenreAlbum>()
                .HasOne(x => x.Genre)
                .WithMany(x => x.GenreAlbums);

            // Relations with Albums
            modelBuilder.Entity<Album>()
                .HasMany(x => x.Songs)
                .WithOne(x => x.Album);
            modelBuilder.Entity<Album>()
                .HasMany(a => a.Comments)
                .WithOne(x => x.CommentableObj);
            modelBuilder.Entity<Album>()
                .HasMany(a => a.Likes)
                .WithOne(x => x.LikeableObj);
            modelBuilder.Entity<Album>()
                .HasIndex(x => x.Title);

            // Relations with Artist
            modelBuilder.Entity<Artist>()
                .HasMany(x => x.Albums)
                .WithOne(x => x.Artist);
            modelBuilder.Entity<Artist>()
                .HasMany(x => x.Songs)
                .WithOne(x => x.Artist);
            modelBuilder.Entity<Artist>()
                .HasMany(a => a.Comments)
                .WithOne(x => x.CommentableObj);
            modelBuilder.Entity<Artist>()
                .HasMany(a => a.Likes)
                .WithOne(x => x.LikeableObj);
            modelBuilder.Entity<Artist>()
                .HasIndex(x => x.Name);

            // Relations with Song
            modelBuilder.Entity<Song>()
                .HasMany(x => x.Comments)
                .WithOne(x => x.CommentableObj);
            modelBuilder.Entity<Song>()
                .HasMany(x => x.Likes)
                .WithOne(x => x.LikeableObj);
            modelBuilder.Entity<Song>()
                .HasIndex(x => x.Name);

            modelBuilder.Entity<User>()
                .HasIndex(x => x.Login)
                .IsUnique();
        }
    }
}
