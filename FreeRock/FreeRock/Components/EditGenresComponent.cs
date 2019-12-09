using FreeRock.Data;
using FreeRock.Models;
using FreeRock.ViewModels;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FreeRock.Components
{
    public class EditGenresViewComponent: ViewComponent
    {
        ApplicationDbContext _context;

        public EditGenresViewComponent(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IViewComponentResult> InvokeAsync(AlbumViewModel albumvm)
        {
            if (albumvm is null)
            {
                albumvm = new AlbumViewModel();
            }
            if (albumvm.Album.AlbumGenres is null)
            {
                albumvm.Album.AlbumGenres = new List<GenreAlbum> ();
            }
            if (albumvm.Genres.Count == 0)
            {
                albumvm.Genres = new List<Genre> { new Genre() };
            }
            ViewData.Model = albumvm;
            return View(albumvm);
        }
    }
}
