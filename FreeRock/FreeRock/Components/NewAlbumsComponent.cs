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
    public class NewAlbumsViewComponent: ViewComponent
    {
        ApplicationDbContext _context;

        public NewAlbumsViewComponent(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IViewComponentResult> InvokeAsync() =>
            View(await _context.Albums.OrderByDescending(x => x.ReleaseDate).Take(10).ToListAsync());
    }
}
