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
    public class ArtistMinViewComponent : ViewComponent
    {
        ApplicationDbContext _context;

        public ArtistMinViewComponent(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IViewComponentResult> InvokeAsync(int? id)
        {
            Artist artist = id == null ? new Artist() : _context.Artists.FirstOrDefault(x => x.ArtistID == id);  
            return View(artist);
        }
            
    }
}
