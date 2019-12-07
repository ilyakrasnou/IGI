using FreeRock.Models;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace FreeRock.ViewModels
{
    public class ArtistViewModel
    {
        public Artist Artist { get; set; }
        public IFormFile PhotoImage { get; set; }


        public ArtistViewModel()
        {
            Artist = new Artist();
            PhotoImage = null;
        }

        public ArtistViewModel(Artist artist)
        {
            Artist = artist;
            PhotoImage = null;
        }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            List<ValidationResult> errors = new List<ValidationResult>();
            if (PhotoImage != null)
            {
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
