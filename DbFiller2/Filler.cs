using System;
using System.Collections.Generic;
using System.Linq;

namespace DbFiller2
{
    class Filler
    {
        private readonly SongsDbContext songDb;

        private IEnumerable<Songs> Songs { get; set; }
        private IEnumerable<Albums> Albums { get; set; }
        private IEnumerable<Artists> Artists { get; set; }

        public Filler(SongsDbContext songDb)
        {
            this.songDb = songDb;
            GenerateArtists();
            GenerateAlbums();
            GenerateSongs();
        }

        private void GenerateArtists()
        {
            Artists = new List<Artists>
            {
                new Artists{Id = 1, Name = "Three Days Grace"},
                new Artists{Id = 2, Name = "Linkin Park"},
                new Artists{Id = 3, Name = "Rammstein"}
            };
        }

        private void GenerateAlbums()
        {
            Albums = new List<Albums>
            {
                new Albums{Id = 1, Name = "Life Starts Now", Year = 2009, ArtistId = 1},
                new Albums{Id = 2, Name = "Reanimation", Year = 2002, ArtistId = 2},
                new Albums{Id = 3, Name = "Mutter", Year = 2001, ArtistId = 3}
            };
        }

        private void GenerateSongs()
        {
            Songs = new List<Songs>
            {
                new Songs{Id = 1, Name = "Life Starts Now", AlbumId = 1},
                new Songs{Id = 2, Name = "Someone Who Cares", AlbumId = 1},
                new Songs{Id = 3, Name = "In The End", AlbumId = 2},
                new Songs{Id = 4, Name = "Sonne", AlbumId = 3}
            };
        }

        public void Fill()
        {
            songDb.Artists.AddRange(Artists);
            songDb.Albums.AddRange(Albums);
            songDb.Songs.AddRange(Songs);
            songDb.SaveChanges();
        }
    }
}
