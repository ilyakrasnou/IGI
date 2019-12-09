using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FreeRock.Data;
using FreeRock.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace FreeRock.Controllers
{
    public class GenresController : Controller
    {
        ApplicationDbContext _context;

        public GenresController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Genres
        public ActionResult Index()
        {
            return View();
        }

        // GET: Genres/Details/5
        [HttpGet("Genres/{id:int:min(1)}")]
        public ActionResult Details(int id)
        {
            var genre = _context.Genres.FirstOrDefault(x => x.ID == id);
            if (genre is null)
                return NotFound();
            return View(genre);
        }

        public object GetGenres() => _context.Genres
            .Select(x => new { id = x.ID, name = x.Name })
            .ToList();

        public object GetAlbumsByGenre(int page, int perPage, int id)
        {
            var res = _context.Genres.FirstOrDefault(x => x.ID == id);
            if (res is null)
                return new { data = new List<AlbumMin>(), lastPage=0};
            var albums = res.GenreAlbums.Select(x => x.Album);
            var count = albums.Count();
            var lastPage = Math.Ceiling((double)count / perPage);
            var data = albums.Skip((page - 1) * perPage).Take(perPage).Select(x => new AlbumMin(x));
            return new { data, lastPage };
        }
    }
}