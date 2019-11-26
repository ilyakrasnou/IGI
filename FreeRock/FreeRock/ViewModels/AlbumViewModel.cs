﻿using FreeRock.Models;
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
        public IEnumerable<String> Genres { get; set; }


        public AlbumViewModel()
        {
            Album = new Album();
            CoverImage = null;
            Genres = new List<String>();
        }

        public AlbumViewModel(Album album)
        {
            Album = album;
            CoverImage = null;
            Genres = new List<string>();
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
                if (CoverImage.ContentType != "image/jpg")
                {
                    errors.Add(new ValidationResult("Not .jpg file."));
                }
            }
            return errors;
        }
    }
}
