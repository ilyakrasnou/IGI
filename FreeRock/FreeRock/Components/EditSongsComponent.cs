using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FreeRock.Data;
using FreeRock.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace FreeRock.Components
{
    public class EditSongsViewComponent : ViewComponent
    {
        ApplicationDbContext _context;

        public EditSongsViewComponent(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IViewComponentResult> InvokeAsync(Album album)
        {
            if (album is null)
            {
                album = new Album();
            }
            if (album.Songs is null)
            {
                album.Songs = new List<Song> { new Song() };
            }
            ViewData.Model = album;
            return View(album);
        }
    }
}
