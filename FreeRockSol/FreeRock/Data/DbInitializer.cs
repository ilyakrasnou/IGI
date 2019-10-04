using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FreeRock.Models;

namespace FreeRock.Data
{
    public class DbInitializer
    {
        public static void Initialize(CatalogContext context)
        {
            context.Database.EnsureCreated();
            //Look for any album
            if (context.Albums.Any())
                return;
            Album[] albums = new Album[]
            {
            };
            foreach (Album album in albums)
            {
                context.Albums.Add(album);
            }
            context.SaveChanges();

            // TODO do similar with a\othe models

        }
    }
}
