using FreeRock.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace FreeRock.ViewModels
{
    public class AlbumViewModel: IValidatableObject
    {
        public Album Album { get; set; }
        public IFormFile CoverImage { get; set; }
        public IFormFile PhotoImage { get; set; }
        public int Mark { get; private set; }
        public IList<Genre> Genres { get; set; }


        public AlbumViewModel()
        {
            Album = new Album();
            CoverImage = null;
            PhotoImage = null;
            Genres = new List<Genre> { new Genre() };
        }

        public AlbumViewModel(Album album)
        {
            Album = album;
            CoverImage = null;
            PhotoImage = null;
            Genres = album.AlbumGenres.Select(x => x.Genre).ToList();
            if (Genres.Count == 0)
            {
                Genres.Add(new Genre());
            }
        }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            List<ValidationResult> errors = new List<ValidationResult>();
            if (CoverImage != null)
            {
                if (CoverImage.Length > 268435456)
                {
                    errors.Add(new ValidationResult("Too large file."));
                }
                if (CoverImage.ContentType != "image/jpeg")
                {
                    errors.Add(new ValidationResult("Not .jpeg or .jpg file."));
                }
            }
            if (PhotoImage != null)
            {;
                if (PhotoImage.Length > 268435456)
                {
                    errors.Add(new ValidationResult("Too large file."));
                }
                if (PhotoImage.ContentType != "image/jpeg")
                {
                    errors.Add(new ValidationResult("Not .jpeg or .jpg file."));
                }
            }
            foreach (var s in Album.Songs)
            {
                if (string.IsNullOrWhiteSpace(s.Name))
                {
                    errors.Add(new ValidationResult("Song name can't be empty"));
                }
            }
            HashSet<string> genreNames = new HashSet<string>();
            foreach (var g in Genres)
            {
                g.Name = g.Name.ToLower().Trim();
                var gName = g.Name;
                if (string.IsNullOrWhiteSpace(gName))
                {
                    errors.Add(new ValidationResult("Genre can't be empty"));
                }
                else if (genreNames.Contains(gName))
                {
                    errors.Add(new ValidationResult("Genre must be unique"));
                }
                else
                {
                    genreNames.Add(gName);
                }
            }
            return errors;
        }
    }
}
