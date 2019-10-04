using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace FreeRock.Controllers
{
    public class PageController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public string Artist(int artistID)
        {
            return "No artist with such ID"; 
        }

        public string Album(int albumID)
        {
            return "No album with such ID";
        }

        public string Song(int songID)
        {
            return "No song with such ID";
        }
    }
}