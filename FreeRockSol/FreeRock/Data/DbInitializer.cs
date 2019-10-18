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
            var artists = new List<Artist> {
                new Artist { Name = "Pink Floyd", Description = "The best prog rock band in the UK" },
                new Artist
                {
                    Name = "System of a Down",
                    Comments = new List<Comment<Artist>> {
                        new Comment<Artist>{Text="Wow!"},
                        new Comment<Artist>{Text="Great!"}
                    }
                }
            };
            Album[] albums = new Album[]
            {
                new Album{
                    Title ="Toxicity",
                    ReleaseDate =1999,
                },
                new Album
                {
                    Title ="Animals",
                    ReleaseDate =1977,
                    Comments = new List<Comment<Album>>
                    {
                        new Comment<Album>{Text="Fucking sheet!"}
                    }
                },
                new Album{
                    Title ="Dark side of the moon",
                    ReleaseDate =1973,
                }
            };
            var songs = new List<List<Song>>
            {
                new List<Song>
                    {
                        new Song{Name="Prison Song"},
                        new Song{Name="Needles"},
                        new Song{Name="Deer Dance"},
                        new Song{Name="Jet Pilot"},
                        new Song{Name="X"},
                        new Song{Name="Chop Suey!"},
                        new Song{Name="Bounce"},
                        new Song{Name="Forest"},
                        new Song{Name="ATWA"},
                        new Song{Name="Science"},
                        new Song{Name="Shimmy"},
                        new Song{Name="Toxicity"},
                        new Song{Name="Psycho"},
                        new Song{Name="Aerials"},
                    },
                new List<Song>
                    {
                        new Song{Name="Pig on the Wings (part 1)"},
                        new Song{Name="Dogs"},
                        new Song{Name="Pigs (Three Different Ones)"},
                        new Song{Name="Sheep"},
                        new Song{Name="Pigs on the Wing (part 2)"},
                    },
                new List<Song>
                    {
                        new Song{Name="Speak to Me" },
                        new Song{Name="Breathe"},
                        new Song{Name="On the Run"},
                        new Song{Name="Time"},
                        new Song{Name="The Great Gig in the Sky"},
                        new Song{Name="Money"},
                        new Song{Name="Us and Them"},
                        new Song{Name="Any Colour You Like"},
                        new Song{Name="Brain Damage"},
                        new Song{Name="Eclipse"},
                    }
            };
            //foreach (Song s in songs[0])
            //    s.SongArtists.Add(new ArtistSong { ArtistID = system.ID, Artist = system, Song = s });
            //foreach (Song s in songs[1])
            //    s.SongArtists.Add(new ArtistSong { Artist = pinkFloyd, Song = s });
            //foreach (Song s in songs[2])
            //    s.SongArtists.Add(new ArtistSong { Artist = pinkFloyd, Song = s });
            for (int i = 0; i < albums.Length; ++i)
                albums[i].Songs = songs[i];
            albums[0].Artist = artists[1];
            albums[1].Artist = artists[0];
            albums[2].Artist = artists[0];

            foreach (Album album in albums)
            {
                context.Albums.Add(album);
            }


            foreach (var list in songs)
                foreach (Song s in list)
                    context.Songs.Add(s);
            foreach (var a in artists)
                context.Artists.Add(a);
            context.SaveChanges();
            /*if (context.Artists.Any())
                return;
            Artist[] artists = new Artist[]
            {
                new Artist{Name="Pink Floyd", Albums= new List<Album>(), Description="The best prog rock band in the UK"},
                new Artist{Name="System of a Down", Albums= new List<Album>(), Comments= new List<Comment>()}
            };
            artists[0].Albums.Add(albums[1]);
            artists[0].Albums.Add(albums[2]);
            artists[1].Albums.Add(albums[0]);
            albums[0].ArtistID = artists[0];
            albums[1].ArtistID = */
            // TODO do similar with a\othe models

        }
    }
}
