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
        public IEnumerable<Genre> Genres { get; set; }


        public AlbumViewModel()
        {
            Album = new Album();
            CoverImage = null;
            PhotoImage = null;
            Genres = new List<Genre>();
        }

        public AlbumViewModel(Album album)
        {
            Album = album;
            CoverImage = null;
            PhotoImage = null;
            Genres = album.AlbumGenres.Select(x => x.Genre).ToList();
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
            return errors;
        }
    }
}
