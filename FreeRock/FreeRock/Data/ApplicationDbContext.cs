using System;
using System.Collections.Generic;
using System.Text;
using FreeRock.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace FreeRock.Data
{
    public class ApplicationDbContext : IdentityDbContext<User>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
            Database.Migrate();
        }

        public DbSet<Album> Albums { get; set; }
        public DbSet<Artist> Artists { get; set; }
        public DbSet<Song> Songs { get; set; }
        public DbSet<Genre> Genres { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<GenreAlbum>()
                .HasKey(s => new { s.GenreID, s.AlbumID });
            modelBuilder.Entity<GenreAlbum>()
                .HasOne(x => x.Album)
                .WithMany(x => x.AlbumGenres)
                .OnDelete(DeleteBehavior.Cascade);
            modelBuilder.Entity<GenreAlbum>()
                .HasOne(x => x.Genre)
                .WithMany(x => x.GenreAlbums)
                .OnDelete(DeleteBehavior.Cascade);

            // Relations with Albums
            modelBuilder.Entity<Album>()
                .HasMany(x => x.Songs)
                .WithOne(x => x.Album)
                .OnDelete(DeleteBehavior.Cascade);
            modelBuilder.Entity<Album>()
                .HasMany(a => a.Comments)
                .WithOne(x => x.CommentableObj)
                .OnDelete(DeleteBehavior.Cascade);
            modelBuilder.Entity<Album>()
                .HasMany(a => a.Likes)
                .WithOne(x => x.LikeableObj)
                .OnDelete(DeleteBehavior.Cascade);
            modelBuilder.Entity<Album>()
                .HasIndex(x => x.Title);
            modelBuilder.Entity<Album>()
                .Property(x => x.IsVerified)
                .HasDefaultValue(false);

            // Relations with Artist
            modelBuilder.Entity<Artist>()
                .HasMany(x => x.Albums)
                .WithOne(x => x.Artist)
                .OnDelete(DeleteBehavior.Cascade);
            modelBuilder.Entity<Artist>()
                .HasMany(a => a.Comments)
                .WithOne(x => x.CommentableObj)
                .OnDelete(DeleteBehavior.Cascade);
            modelBuilder.Entity<Artist>()
                .HasMany(a => a.Likes)
                .WithOne(x => x.LikeableObj)
                .OnDelete(DeleteBehavior.Cascade);
            modelBuilder.Entity<Artist>()
                .HasIndex(x => x.Name);
            //modelBuilder.Entity<Artist>()
            //    .Property(x => x.IsVerified)
            //    .HasDefaultValue(false);

            // Relations with Song
            modelBuilder.Entity<Song>()
                .HasMany(x => x.Comments)
                .WithOne(x => x.CommentableObj)
                .OnDelete(DeleteBehavior.Cascade);
            modelBuilder.Entity<Song>()
                .HasMany(x => x.Likes)
                .WithOne(x => x.LikeableObj)
                .OnDelete(DeleteBehavior.Cascade);
            modelBuilder.Entity<Song>()
                .HasIndex(x => x.Name);
            //modelBuilder.Entity<Song>()
            //    .Property(x => x.IsVerified)
            //    .HasDefaultValue(false);

            modelBuilder.Entity<Album>().ToTable("Album");
            modelBuilder.Entity<Artist>().ToTable("Artist");
            modelBuilder.Entity<Song>().ToTable("Song");
            modelBuilder.Entity<Genre>().ToTable("Genre");
        }
    }
}
