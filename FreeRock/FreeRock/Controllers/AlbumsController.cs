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
using Microsoft.AspNetCore.Identity;

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
        public async Task<IActionResult> Index()
        {
            /*IQueryable<Album> albums = _context.Albums;
            if (!User.IsInRole("admin"))
            {
                albums = albums.Where(x => x.IsVerified);
            }
            return View(albums);*/
            return View();
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
            User user = _context.Users.FirstOrDefault(x => x.UserName == User.Identity.Name);
            var mark = album.Likes.FirstOrDefault(x => x.User == user);
            ViewBag.CurrentUserMark = mark is null ? 0 : mark.Mark;
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
                var album = albumvm.Album;
                album.AlbumGenres = new List<GenreAlbum>();
                foreach (var g in albumvm.Genres)
                {
                    albumvm.Album.AlbumGenres.Add( new GenreAlbum { Album = album, Genre = g});
                }
                _context.Add(album);
                await _context.SaveChangesAsync();
                if (albumvm.CoverImage != null)
                {
                    using (var stream = new FileStream($"wwwroot/covers/{albumvm.Album.ID}.jpeg", FileMode.OpenOrCreate))
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
        [Authorize(Roles = "admin"), HttpGet("Albums/Edit/{id:int:min(1)}")]
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
        [Authorize(Roles = "admin")]
        public async Task<IActionResult> Edit(int id, AlbumViewModel albumvm)
        {
            if (id != albumvm.Album.ID)
            {
                return NotFound();
            }
            //return View(albumvm);
            if (ModelState.IsValid)
            {
                try
                {
                    if (albumvm.Album.AlbumGenres is null)
                    {
                        albumvm.Album.AlbumGenres = new List<GenreAlbum>();
                    }
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
                        else
                        {
                            // this is new song
                            s.Album = albumvm.Album;
                            newSongs.Add(s);
                        }
                    }
                    // work with genres
                    HashSet<int> newGenreID = new HashSet<int>();
                    IList<Genre> newGenres = new List<Genre>();
                    for (byte i = 0; i < albumvm.Genres.Count; ++i)
                    {
                        var g = albumvm.Genres[i];
                        // check if genre was in database and we don't need to add it
                        if (g.ID != 0)
                        {
                            newGenreID.Add(g.ID);
                        }
                        else
                        {
                            // this is new genre
                            albumvm.Album.AlbumGenres.Add(new GenreAlbum { Album = albumvm.Album, Genre = g });
                            newGenres.Add(g);
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

                    // remove old genres that are not in database after editing 
                    {
                        IEnumerable<Genre> oldGenres = _context.Genres
                            .Where(g => g.GenreAlbums.Any(x => x.Album == albumvm.Album));
                        foreach (var g in oldGenres)
                        {
                            if (!newGenreID.Contains(g.ID))
                            {
                                g.GenreAlbums.Remove(g.GenreAlbums.First(x => x.Album == albumvm.Album));
                                if (g.GenreAlbums.Count == 0)
                                {
                                    _context.Remove(g);
                                }
                                else
                                {
                                    _context.Update(g);
                                }

                            }
                                
                        }
                    }
                    // add new genres 
                    foreach (var g in newGenres)
                        _context.Add(g);


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
                    using (var stream = new FileStream($"wwwroot/covers/{albumvm.Album.ID}.jpeg", FileMode.OpenOrCreate))
                    {
                        await albumvm.CoverImage.CopyToAsync(stream);
                    }
                }

                if (albumvm.PhotoImage != null)
                {
                    // get new version of album in database to find artistID
                    var album = _context.Albums.FirstOrDefault(x => x.ID == id);
                    using (var stream = new FileStream($"wwwroot/photos/{album.ArtistID}.jpeg", FileMode.OpenOrCreate))
                    {
                        await albumvm.PhotoImage.CopyToAsync(stream);
                    }
                }

                return RedirectToAction(nameof(Index));
            }
            PopulateArtistsDropDownList(albumvm.Album.ArtistID);
            return View(albumvm);
        }

        // GET: Albums/Delete/5
        [HttpGet("Albums/Delete/{id:int:min(1)}")]
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
        [HttpPost("Albums/Delete/{id:int:min(1)}"), ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "admin")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var album = await _context.Albums.FindAsync(id);
            var artist = album.Artist;
            var genres = album.AlbumGenres.Select(x => x.Genre);
            _context.Albums.Remove(album);
            if (artist.Albums.Count == 0)
            {
                _context.Artists.Remove(artist);
            }
            foreach (var g in genres)
            {
                if (g.GenreAlbums.Count == 0)
                {
                    _context.Genres.Remove(g);
                }
            }
            await _context.SaveChangesAsync();
            if (System.IO.File.Exists($"wwwroot/covers/{id}.jpg"))
            {
                System.IO.File.Delete($"wwwroot/covers/{id}.jpg");
            }
            return RedirectToAction(nameof(Index));
        }

        [HttpPost("Albums/Mark/{id:int:min(1)}")]
        public async Task<object> MarkAlbum(int id, sbyte mark, string userName)
        {
            var album = _context.Albums.FirstOrDefault(x => x.ID == id);
            if (userName is null || mark == 0 || album is null)
                return new { rating = album.Likes.Sum(x => x.Mark), mark = 0 };
            User user = _context.Users.FirstOrDefault(x => x.UserName == userName);
            var oldMark = album.Likes.FirstOrDefault(x => x.User == user);
            if (oldMark is null)
            {
                album.Likes.Add(new Like<Album> { User = user, LikeableObj = album, Mark = mark });
            }
            else
            {
                oldMark.Mark = mark;
            }
            _context.Update(album);
            await _context.SaveChangesAsync();
            var rating = album.Likes.Sum(x => x.Mark);
            return new { rating, mark };
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

        public object GetAlbums(int page, int perPage, string searchString = null, string sortBy = null, string sortOrder = null)
        {
            IQueryable<Album> albums = _context.Albums.Include(x => x.Artist);

            if (!User.IsInRole("admin"))
            {
                albums = albums.Where(s => s.IsVerified);
            }

            if (!string.IsNullOrEmpty(searchString))
            {
                albums = albums.Where(s => s.Title.Contains(searchString));
            }

            if (string.IsNullOrEmpty(sortOrder))
            {
                sortBy = "CreatedDate";
            }

            if (sortOrder == "desc")
            {
                albums = albums.OrderByDescending(x => EF.Property<object>(x, sortBy));
            }
            else
            {
                albums = albums.OrderBy(x => EF.Property<object>(x, sortBy));
            }

            var count = albums.Count();
            var lastPage = Math.Ceiling((double)count / perPage);
            var data = albums.Skip((page - 1) * perPage).Take(perPage).Select(x => new AlbumMin(x));
            return new { data, lastPage };
        }
    }
}
