using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using FreeRock.Data;
using FreeRock.Models;
using Microsoft.AspNetCore.Authorization;
using FreeRock.ViewModels;
using System.IO;

namespace FreeRock.Controllers
{
    public class ArtistsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public ArtistsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Artists
        [HttpGet("Artists/")]
        public async Task<IActionResult> Index()
        {
            if (User.IsInRole("admin"))
                return View(await _context.Artists.ToListAsync());
            else
                return View(await _context.Artists.Where(x => x.Albums.Any(a => a.IsVerified)).ToListAsync());
        }

        // GET: Artists/Details/5
        [HttpGet("Artists/{id:int:min(1)}")]
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var artist = await _context.Artists
                .FirstOrDefaultAsync(m => m.ID == id);
            if (artist == null)
            {
                return NotFound();
            }
            User user = _context.Users.FirstOrDefault(x => x.UserName == User.Identity.Name);
            var mark = artist.Likes.FirstOrDefault(x => x.User == user);
            ViewBag.CurrentUserMark = mark is null ? 0 : mark.Mark;
            return View(artist);
        }

        // GET: Artists/Create
        [HttpGet("Artists/Create")]
        public IActionResult Create()
        {
            return View();
        }

        // POST: Artists/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(ArtistViewModel artistvm)
        {
            var artist = artistvm.Artist;
            if (ModelState.IsValid)
            {
                _context.Add(artist);
                await _context.SaveChangesAsync();
                if (artistvm.PhotoImage != null)
                {
                    using (var stream = new FileStream($"wwwroot/photos/{artistvm.Artist.ID}.jpeg", FileMode.OpenOrCreate))
                    {
                        await artistvm.PhotoImage.CopyToAsync(stream);
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(artistvm);
        }

        // GET: Artists/Edit/5
        [Authorize(Roles = "admin")]
        [HttpGet("Artists/Edit/{id:int:min(1)}")]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var artist = await _context.Artists.FindAsync(id);
            if (artist == null)
            {
                return NotFound();
            }
            ArtistViewModel artistvm = new ArtistViewModel(artist); 
            return View(artistvm);
        }

        // POST: Artists/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost("Artists/Edit/{id:int:min(1)}")]
        [ValidateAntiForgeryToken]
        [Authorize(Roles ="admin")]
        public async Task<IActionResult> Edit(int id, ArtistViewModel artistvm)
        {
            var artist = artistvm.Artist;
            if (id != artist.ID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(artist);
                    await _context.SaveChangesAsync();
                    if (artistvm.PhotoImage != null)
                    {
                        using (var stream = new FileStream($"wwwroot/photos/{artistvm.Artist.ID}.jpeg", FileMode.OpenOrCreate))
                        {
                            await artistvm.PhotoImage.CopyToAsync(stream);
                        }
                    }
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ArtistExists(artist.ID))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(artistvm);
        }

        // GET: Artists/Delete/5
        [Authorize(Roles = "admin")]
        [HttpGet("Artists/Delete/{id:int:min(1)}")]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var artist = await _context.Artists
                .FirstOrDefaultAsync(m => m.ID == id);
            if (artist == null)
            {
                return NotFound();
            }

            return View(artist);
        }

        // POST: Artists/Delete/5
        [HttpPost("Artists/Delete/{id:int:min(1)}"), ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "admin")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var artist = await _context.Artists.FindAsync(id);
            _context.Artists.Remove(artist);
            await _context.SaveChangesAsync();
            if (System.IO.File.Exists($"wwwroot/photos/{id}.jpeg"))
            {
                System.IO.File.Delete($"wwwroot/photos/{id}.jpeg");
            }
            return RedirectToAction(nameof(Index));
        }

        [HttpPost("Artists/Mark/{id:int:min(1)}")]
        public async Task<object> MarkArtist(int id, sbyte mark, string userName)
        {
            var artist = _context.Artists.FirstOrDefault(x => x.ID == id);
            if (userName is null || mark == 0 || artist is null)
                return new { rating = artist.Likes.Sum(x => x.Mark), mark = 0 };
            User user = _context.Users.FirstOrDefault(x => x.UserName == userName);
            var oldMark = artist.Likes.FirstOrDefault(x => x.User == user);
            if (oldMark is null)
            {
                artist.Likes.Add(new Like<Artist> { User = user, LikeableObj = artist, Mark = mark });
            }
            else
            {
                oldMark.Mark = mark;
            }
            _context.Update(artist);
            await _context.SaveChangesAsync();
            var rating = artist.Likes.Sum(x => x.Mark);
            return new { rating, mark };
        }

        public object GetArtistAlbums(int page, int perPage, int id)
        {
            //int id = int.Parse(artistID);
            Artist artist = _context.Artists.FirstOrDefault(x => x.ID == id);
            IEnumerable<Album> albums = artist.Albums;

            if (!User.IsInRole("admin"))
            {
                albums = albums.Where(s => s.IsVerified);
            }

            albums = albums.OrderByDescending(x => x.ReleaseDate);

            var count = albums.Count();
            var lastPage = Math.Ceiling((double)count / perPage);
            var data = albums.Skip((page - 1) * perPage).Take(perPage).Select(x => new AlbumMin(x));
            return new { data, lastPage };
        }

        public object GetArtists(int page, int perPage, string searchString = null)
        {
            IQueryable<Artist> artists = _context.Artists.Include(x => x.Albums);

            if (!User.IsInRole("admin"))
            {
                artists = artists.Where(x => x.Albums.Any(a => a.IsVerified));
            }

            if (!string.IsNullOrEmpty(searchString))
            {
                artists = artists.Where(s => s.Name.Contains(searchString));
            }

            artists = artists.OrderBy(x => x.CreatedDate);

            var count = artists.Count();
            var lastPage = Math.Ceiling((double)count / perPage);
            var data = artists.Skip((page - 1) * perPage).Take(perPage).Select(x => new ArtistMin(x));
            return new { data, lastPage };
        }

        private bool ArtistExists(int id)
        {
            return _context.Artists.Any(e => e.ID == id);
        }
    }
}
