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
using Microsoft.AspNetCore.Http;

namespace FreeRock.Controllers
{
    public class AlbumsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public AlbumsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Albums
        [HttpGet("Albums")]
        public async Task<IActionResult> Index(string searchString)
        {
            var albums = from a in _context.Albums
                         select a;
            if (!string.IsNullOrEmpty(searchString))
            {
                albums = albums.Where(s => s.Title.Contains(searchString));
            }

            if (User.IsInRole("admin"))
                return View(await albums.ToListAsync());
            else
                return View(await albums.Where(x => x.IsVerified).ToListAsync());
        }

        // GET: Albums/Details/5
        [HttpGet("Albums/{id:int:min(1)?}")]
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var album = await _context.Albums
                .FirstOrDefaultAsync(m => m.ID == id);

            if (album == null)
            {
                return NotFound();
            }

            return View(album);
        }

   
        [HttpPost("Albums/{id:int:min(1)}")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Details(int id, string NewCommentary, string userName)
        {
            
            var album = await _context.Albums
                .FirstOrDefaultAsync(m => m.ID == id);
            
            //AddCommentary(project, NewCommentary, UserName);
            //_context.Projects.Update(project);
            //await _context.SaveChangesAsync();
            return View(album);
        }


        // GET: Albums/Create
        [HttpGet]
        public IActionResult Create()
        {
            PopulateArtistsDropDownList();
            //return View(_context.Albums.First());
            return View(new AlbumViewModel());
        }

        /*[HttpGet]
        public IList<Song> GetSongs()
        {
            var x = _context.Albums.First().Songs.Select(s => new Song{ Name = s.Name, YouTubeUrl = s.YouTubeUrl }).ToList();
            return x;
        }*/

        // POST: Albums/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost("Albums/Create")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(AlbumViewModel albumvm)
        {
            if (ModelState.IsValid)
            {
                _context.Add(albumvm.Album);
                await _context.SaveChangesAsync();
                if (albumvm.CoverImage != null)
                {
                    using (var stream = new FileStream($"wwwroot/covers/{albumvm.Album.ID}.jpg", FileMode.OpenOrCreate))
                    {
                        await albumvm.CoverImage.CopyToAsync(stream);
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            PopulateArtistsDropDownList(albumvm.Album.ArtistID);
            return View(albumvm);
        }

        // GET: Albums/Edit/5
        [Authorize(Roles ="admin"), HttpGet("Albums/Edit/{id:int:min(1)}")]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var album = await _context.Albums.FindAsync(id);
            if (album == null)
            {
                return NotFound();
            }
            album.Songs = album.Songs.OrderBy(s => s.Number).ToList();
            PopulateArtistsDropDownList(album.ArtistID);
            return View(new AlbumViewModel(album));
        }

        // POST: Albums/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost("Albums/Edit/{id:int:min(1)}")]
        [ValidateAntiForgeryToken]
        [Authorize(Roles ="admin")]
        public async Task<IActionResult> Edit(int id, AlbumViewModel albumvm)
        {
            if (id != albumvm.Album.ID)
            {
                return NotFound();
            }
            return View(albumvm);
            if (ModelState.IsValid)
            {
                try
                {
                    HashSet<int> newID = new HashSet<int>();
                    IList<Song> newSongs = new List<Song>();
                    for (byte i = 0; i < albumvm.Album.Songs.Count; ++i)
                    {
                        var s = albumvm.Album.Songs[i];
                        s.Number = i;
                        // check if song was in album and we need to update it
                        if (s.ID != 0)
                        {
                            newID.Add(s.ID);
                            _context.Update(s);
                        }
                        // this is new song
                        else
                        {
                            s.Album = albumvm.Album;
                            newSongs.Add(s);
                        }
                    }
                    _context.Update(albumvm.Album);
                    // remove old songs that are not in album after editing 
                    {
                        IEnumerable<Song> oldSongs = _context.Songs.Where(s => s.Album == albumvm.Album);
                        foreach (var s in oldSongs)
                        {
                            if (!newID.Contains(s.ID))
                                _context.Remove(s);
                        }
                    }
                    // add new songs 
                    foreach (var s in newSongs)
                       _context.Add(s);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!AlbumExists(albumvm.Album.ID))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }

                if (albumvm.CoverImage != null)
                {
                    using (var stream = new FileStream($"wwwroot/covers/{albumvm.Album.ID}.jpg", FileMode.OpenOrCreate))
                    {
                        await albumvm.CoverImage.CopyToAsync(stream);
                    }
                }

                return RedirectToAction(nameof(Index));
            }
            PopulateArtistsDropDownList(albumvm.Album.ArtistID);
            return View(albumvm);
        }

        // GET: Albums/Delete/5
        [HttpGet("Delete/{id:int:min(1)}")]
        [Authorize(Roles = "admin")]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var album = await _context.Albums
                .FirstOrDefaultAsync(m => m.ID == id);
            if (album == null)
            {
                return NotFound();
            }

            return View(album);
        }

        // POST: Albums/Delete/5
        [HttpPost("Delete/{id:int:min(1)}"), ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize(Roles ="admin")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var album = await _context.Albums.FindAsync(id);
            _context.Albums.Remove(album);
            await _context.SaveChangesAsync();
            if (System.IO.File.Exists($"wwwroot/covers/{id}.jpg"))
            {
                System.IO.File.Delete($"wwwroot/covers/{id}.jpg");
            }
            return RedirectToAction(nameof(Index));
        }

        private bool AlbumExists(int id)
        {
            return _context.Albums.Any(e => e.ID == id);
        }

        private void PopulateArtistsDropDownList(object selectedArtist = null)
        {
            var artistsQuery = _context.Artists.OrderBy(a => a.Name);
            ViewBag.ArtistID = new SelectList(artistsQuery.AsNoTracking(), "ID", "Name", selectedArtist);
        }
    }
}
